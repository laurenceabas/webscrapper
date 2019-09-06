using System;
using System.Collections.Generic;
using System.Text;

namespace HamiltonPageScrapper.Entities
{
	public class Seat
	{
		public string Name { get; set; }

		public string GroupId { get; set; }

		public string Status { get; set; }

		public Price SeatPrice { get; set; }
	}
}
