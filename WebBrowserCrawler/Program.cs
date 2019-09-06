using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCrawler
{
	class Program
	{

		const string TARGET_URL = "https://www.vividseats.com/theatre/hamilton-tickets/hamilton-9-3-2883527.html#";

		[STAThread]
		static void Main(string[] args)
		{
			//RunCrawler(new Uri(TARGET_URL));

			Application.Run(new InternalBrowser());
		}

		//private static void RunCrawler(Uri url)
		//{
		//	var thread = new Thread(() =>
		//	{
		//		var browser = new WebBrowser();
		//		browser.DocumentCompleted += Browser_DocumentCompleted;
		//		browser.Navigate(url);

		//		Application.Run();
		//	});

		//	thread.SetApartmentState(ApartmentState.STA);
		//	thread.Start();
		//}

		//private static void Browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		//{
		//	var browser = (WebBrowser)sender;

		//	Console.WriteLine(browser.DocumentText);
		//	Application.ExitThread();
		//}
	}
}
