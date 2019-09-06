using Contracts;
using System;
using System.Collections.Generic;
using HamiltonPageScrapper;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace WebRunner
{
	class Program
	{
		private static IList<IScrappable> _scrappers;
		private static WebScrapperContext _dbContext;

		static void Main(string[] args)
		{
			_dbContext = CreateDbContext();
			_scrappers = GetAllScrappers();

			foreach(var scrapper in _scrappers)
			{
				scrapper.Initialize();
				scrapper.Execute();
				scrapper.CleanUp();
			}
		}

		private static IList<IScrappable> GetAllScrappers()
		{
			List<IScrappable> scrappers = new List<IScrappable>();

			scrappers.Add(new HamiltonScrapper(_dbContext));

			return scrappers;
		}

		private static WebScrapperContext CreateDbContext()
		{
			return new WebScrapperContextFactory().CreateDbContext(null);
		}
	}
}
