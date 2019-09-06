using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Contracts;
using Database;
using HamiltonPageScrapper.Entities;
using Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Utilities;

namespace HamiltonPageScrapper
{
	public class HamiltonScrapper : IScrappable
	{
		private string _name;
		private Uri _url;
		private IWebDriver _webDriver;
		private WebTarget _target;

		private readonly WebScrapperContext _dbContext;

		public string Name { get {return _name; } }

		public Uri Url { get { return _url; } }

		public IWebDriver Driver => _webDriver;

		public HamiltonScrapper(WebScrapperContext context)
		{
			_dbContext = context;

			this._url = new Uri(_dbContext.WebTargets.Where(x => x.ConstantName.Equals(GlobalContstants.NAME)).FirstOrDefault().Url);
		}

		// get all the configuration from the database
		public void Initialize()
		{
			// don't proceed if web driver is not present
			if (_webDriver == null)
			{
				var driverOption = new ChromeOptions();
				driverOption.AddArguments(new List<string>() { "no-sandbox", "headless", "disable-gpu", "hide-scrollbars", "log-level=3", "start-maximize" });

				_webDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), driverOption);
				//_webDriver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
			}

			_target = _dbContext.WebTargets.Where(x => x.ConstantName.Equals(GlobalContstants.NAME)).FirstOrDefault();
			_target.InnerLinks = _dbContext.InnerLinks.Where(x => x.ParentLink.Id == _target.Id).ToList();
		}

		/// <summary>
		/// Everything here moving forward is about web page navigation and scrapping.
		/// </summary>
		public void Execute()
		{
			// navigate to URL;
			_webDriver.Navigate().GoToUrl(this.Url.ToString());

			var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));

			var elementModel = _target.InnerLinks;
			Event _event = new Event();
 
			// event propeties
			var nameLocator = elementModel.Where(x => x.ElementName.Equals(GlobalContstants.ITEM_NAME) && x.IsXPath).FirstOrDefault().Locator;
			_event.Name = _webDriver.FindElement(By.XPath(nameLocator)).Text;

			var dateLocator = elementModel.Where(x => x.ElementName.Equals(GlobalContstants.EVENT_DATE) && x.IsXPath).FirstOrDefault().Locator;
			_event.StartDateTime = DateTime.Parse(_webDriver.FindElement(By.XPath(dateLocator)).Text);

			// getting the buy button
			var buyButton = _webDriver.FindElement(By.XPath(elementModel.Where(x => x.ElementName.Equals(GlobalContstants.BUY_BUTTON) && x.IsXPath).FirstOrDefault().Locator));
			buyButton.Click();

			// we need to wait until all seats are loaded
			wait.Until((x) =>
			{
				return ((IJavaScriptExecutor)_webDriver).ExecuteScript("return document.readyState").Equals("complete");
			});

			// now getting the prices
			var priceTable = _webDriver.FindElement(By.XPath(elementModel.Where(x => x.ElementName.Equals(GlobalContstants.PRICING_TABLE) && x.IsXPath).FirstOrDefault().Locator));
			var ticketPrices = priceTable.FindElements(By.XPath(elementModel.Where(x => x.ElementName.Equals(GlobalContstants.PRICES) && x.IsXPath).FirstOrDefault().Locator));

			_event.TicketPrices = GetPriceFromTable(ticketPrices, elementModel);

			// getting all seats irregardless of status
			var seatGroups = _webDriver.FindElements(By.XPath(elementModel.Where(x => x.ElementName.Equals(GlobalContstants.SEAT_GROUP) && x.IsXPath).FirstOrDefault().Locator));
			foreach(var group in seatGroups)
			{
				var seats = group.FindElements(By.TagName(elementModel.Where(x => x.ElementName.Equals(GlobalContstants.SEATS) && x.IsTagName).FirstOrDefault().Locator));
				var newSeats = GetSeatFromSeatGroup(seats, elementModel, _event.TicketPrices);

				if (_event.OfferedSeats == null)
					_event.OfferedSeats = newSeats;
				else
					((List<Seat>)_event.OfferedSeats).AddRange(newSeats);
			}

			// we are now ready to save the scrape data to database
			SaveScrapeData(_event);

			_webDriver.Close();
		}

		public void CleanUp()
		{
			if (_webDriver != null)
				_webDriver.Dispose();
		}

		private IList<Price> GetPriceFromTable(ICollection<IWebElement> pricesElement, IList<InnerLink> elementModel)
		{
			List<Price> prices = new List<Price>();

			foreach(var el in pricesElement)
			{
				var priceText = el.Text;
				var priceName = elementModel.Where(x => x.ElementName.Equals(GlobalContstants.SEAT_NAME) && x.IsCssClass).FirstOrDefault();
				var priceStorke = elementModel.Where(x => x.ElementName.Equals(GlobalContstants.PRICE_COLOR) && x.IsCssClass).FirstOrDefault();
				var amount = elementModel.Where(x => x.ElementName.Equals(GlobalContstants.TICKET_PRICE) && x.IsCssClass).FirstOrDefault();

				var rgbToStroke =  Helpers.ConvertRgbToHex(el.FindElement(By.ClassName(priceStorke.Locator)).GetAttribute(priceStorke.AttributeName));

				var price = new Price()
				{
					Name = Helpers.SanitizeString(el.FindElement(By.ClassName(priceName.Locator)).GetAttribute(GlobalContstants.INNER_TEXT)),
					Stroke = rgbToStroke.ToLower(),
					Amount = Helpers.SanitizeString(el.FindElement(By.ClassName(amount.Locator)).GetAttribute(GlobalContstants.INNER_TEXT))
				};

				prices.Add(price);
			}

			return prices;
		}

		private IList<Seat> GetSeatFromSeatGroup(IList<IWebElement> seatsElement, IList<InnerLink> elementModel, IList<Price> ticketPrices)
		{
			List<Seat> seats = new List<Seat>();

			foreach(var seat in seatsElement)
			{
				var newSeat = new Seat()
				{
					Name = seat.GetAttribute(elementModel.Where(x => x.ElementName.Equals(GlobalContstants.SEAT_DESCRIPTION) && x.IsCssClass).FirstOrDefault().AttributeName),
					Status = seat.GetAttribute(elementModel.Where(x => x.ElementName.Equals(GlobalContstants.SEAT_STATUS) && x.IsCssClass).FirstOrDefault().AttributeName),
				};

				Debug.WriteLine(newSeat.Name);

				var stroke = seat.GetAttribute(elementModel.Where(x => x.ElementName.Equals(GlobalContstants.SEAT_COLOR) && x.IsCssClass).FirstOrDefault().AttributeName);
				if (stroke == null)
					newSeat.SeatPrice = new Price() { Amount = "0.00" };
				else
					newSeat.SeatPrice = ticketPrices.Where(x => x.Stroke.ToLower().Equals(stroke.ToLower())).FirstOrDefault();

				seats.Add(newSeat);
			}

			return seats;
		}

		private async void SaveScrapeData(HamiltonPageScrapper.Entities.Event eventName)
		{
			foreach(var seat in eventName.OfferedSeats)
			{
				_dbContext.Tickets.Add(new Ticket()
				{
					EventName = eventName.Name,
					EventDate = eventName.StartDateTime,
					DateCreated = DateTime.UtcNow,
					SeatNumber = seat.Name,
					Status = seat.Status,
					Price = decimal.Parse(seat.SeatPrice.Amount.Replace("$", string.Empty))
				});
			}

			_dbContext.SaveChanges();
		}
	}
}
