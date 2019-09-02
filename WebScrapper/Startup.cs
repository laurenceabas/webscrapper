using Database;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;

[assembly: FunctionsStartup(typeof(WebScrapper.Startup))]
namespace WebScrapper
{
	public class Startup : FunctionsStartup
	{
		public override void Configure(IFunctionsHostBuilder builder)
		{
			string SqlConnection = Environment.GetEnvironmentVariable("SqlConnectionString");

			builder.Services.AddDbContext<WebScrapperContext>(options => options.UseSqlServer(SqlConnection));
			
		}
	}
}
