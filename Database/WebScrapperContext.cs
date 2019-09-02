using Microsoft.EntityFrameworkCore;
using Models;

namespace Database
{

	public class WebScrapperContext : DbContext
	{
		public WebScrapperContext(DbContextOptions<WebScrapperContext> options) : base (options)
		{

		}

		public DbSet<WebTarget> WebTargets { get; set; }

		public DbSet<InnerLink> InnerLinks { get; set; }

		public DbSet<Ticket> Tickets { get; set; }
	}
}
