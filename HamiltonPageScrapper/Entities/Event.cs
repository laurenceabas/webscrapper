using System;
using System.Collections.Generic;
using System.Text;

namespace HamiltonPageScrapper.Entities
{
	public class Event
	{
		public string Name { get; set; }

		public DateTime StartDateTime { get; set; }

		public IList<Price> TicketPrices { get; set; }

		public IList<Seat> OfferedSeats { get; set; }
	}
}
