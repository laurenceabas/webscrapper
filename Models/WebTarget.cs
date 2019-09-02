using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
	public class WebTarget
	{
		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Url { get; set; }

		public string Description { get; set; }

		public string EventName { get; set; }

		public IList<InnerLink> InnerLinks { get; set; }
	}
}
