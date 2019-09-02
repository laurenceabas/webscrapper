using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
	public class WebScrapperContextFactory : IDesignTimeDbContextFactory<WebScrapperContext>
	{
		public WebScrapperContext CreateDbContext(string[] args)
		{
			var optionBuilder = new DbContextOptionsBuilder<WebScrapperContext>();
			optionBuilder.UseSqlServer("Server=localhost; Database=HtmlWebScrapper; Trusted_Connection=true;");

			return new WebScrapperContext(optionBuilder.Options);
		}
	}
}
