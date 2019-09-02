using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
	public class Ticket
	{
		public long Id { get; set; }

		public string EventName { get; set; }

		public string Location { get; set; }

		public Nullable<decimal> Price { get; set; }

		public string Section { get; set; }

		public string SeatNumber { get; set; }

		public DateTime EventDate { get; set; }

		public string Url { get; set; }

		public string Status { get; set; }

		public bool IsActive { get; set; }

		public DateTime DateCreated { get; set; }

		public Nullable<DateTime> DateLastModified { get; set; }

		[ForeignKey("InnerLinkId")]
		public InnerLink Referrer { get; set; }
	}
}
