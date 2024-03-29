using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Database;

namespace WebScrapper
{
    public class HtmlEventsScrapper
	{
		private readonly WebScrapperContext _context;

		public HtmlEventsScrapper(WebScrapperContext context)
		{
			_context = context;
		}

        [FunctionName("HtmlEventsScrapper")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {


			return new OkResult();
        }
    }
}
