using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SeleniumApp
{
	class Program
	{
		static void Main(string[] args)
		{
			IWebDriver driver = new ChromeDriver();

			driver.Manage().Window.Maximize();

			driver.Navigate().GoToUrl("https://hamilton.shnsf.com");

			// Item name
			IWebElement itemNameElement = driver.FindElement(By.XPath("//div//*[@class=\"results-box standard-search-results\"]//*[@class=\"item-description result-box-item-details\"]//*[@class=\"item-name\"]"));

			// Date 
			IWebElement dateElement = driver.FindElement(By.XPath("//div//*[@class=\"results-box standard-search-results\"]//*[@class=\"item-description result-box-item-details\"]//*[@class=\"item-start-date\"]//*[@class=\"start-date\"]"));

			// for the Buy button element
			IWebElement buyElement = driver.FindElement(By.XPath("//div//*[@class=\"results-box standard-search-results\"]//*[contains(@class, \"item-link result-box-item-details last-column limited\")]//*[@class=\"btn btn-primary\"]"));

			buyElement.Click();

			#region PRICE TABLE

			IWebElement priceListElement = driver.FindElement(By.XPath("//div//*[@id=\"pricing_list\"]//*[@class=\"legend-ul\"]"));
			IList<IWebElement> liElements = priceListElement.FindElements(By.XPath("//*[@class=\"legend-li\"]//*[@class=\"price-zone-option\"]"));

			foreach (var li in liElements)
			{
				Debug.WriteLine($"Seat Name: {li.FindElement(By.ClassName("zone-label")).Text}; " +
					$"Color: {li.FindElement(By.ClassName("price-zone-color")).GetAttribute("style")}" +
					$"Price: {li.FindElement(By.ClassName("price-zone-price")).Text}");

				//System.Diagnostics.Debug.WriteLine(li.Text);

				//System.Diagnostics.Debug.WriteLine(li.FindElements(By.ClassName("price-zone-color"))[0].GetAttribute("style"));

				//System.Diagnostics.Debug.WriteLine(li.FindElement(By.XPath("//*[@class=\"price-zone-info\"]//*[@class=\"zone-label\"]")).Text);
				//System.Diagnostics.Debug.WriteLine(li.FindElement(By.XPath("//*[@class=\"price-zone-info\"]//*[@class=\"item-box-detail-data price-zone-price\"]")).GetAttribute("style"));
				//System.Diagnostics.Debug.WriteLine(li.FindElement(By.XPath("//*[@class=\"price-zone-info\"]//*[@class=\"item-box-detail-data price-zone-price\"]")).Text);
			}


			#endregion

			#region SECTION

			// TEXT
			IWebElement sectionTextElement = driver.FindElement(By.XPath("//div//*[@id=\"mapControlsAndLegend\"]//*[@id=\"screen_flip_section\"]//*[@class=\"item-box-item\"]//label//select//option"));

			// VALUE
			IWebElement sectionValueElement = driver.FindElement(By.XPath("//div//*[@id=\"mapControlsAndLegend\"]//*[@id=\"screen_flip_section\"]//*[@class=\"item-box-item\"]//label//select//option"));

			// LINK
			IWebElement sectionLinkElement = driver.FindElement(By.XPath("//div//*[@id=\"mapControlsAndLegend\"]//*[@id=\"screen_flip_section\"]//*[@class=\"item-box-item\"]//label//select"));

			// DROPDOWN BOX
			SelectElement select = new SelectElement(sectionLinkElement);

			#endregion

			#region SEATS

			// ID
			IList<IWebElement> seatIdElement = driver.FindElements(By.XPath("//div//*[@id=\"seatGroup\"]//*[contains(@style, \"fill\")]")).ToList();

			foreach(var seatGroup in seatIdElement)
			{
				System.Diagnostics.Debug.WriteLine(seatGroup.GetAttribute("style"));
				System.Diagnostics.Debug.WriteLine(seatGroup.GetAttribute("id"));

				var seats = seatGroup.FindElements(By.TagName("circle"));
				foreach(var seat in seats)
				{
					Debug.WriteLine($"Seat: {seat.GetAttribute("data-tsdesc")}; Status: {seat.GetAttribute("data-status")}; Stroke: {seat.GetAttribute("stroke")}" );
				}
			}

			#endregion


			driver.Close();
		}

		//pri	vate static IList<IScrappable> GetAllScrappers()
		//{

		//}
	}
}
