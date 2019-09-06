using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
	public class InnerLink
	{
		public long Id { get; set; }

		[ForeignKey("WebTargetId")]
		public WebTarget ParentLink { get; set; }

		public string Url { get; set; }

		public string Locator { get; set; }

		public bool IsXPath { get; set; }

		public bool IsCssClass { get; set; }

		public bool IsTagName { get; set; }

		public string AttributeName { get; set; }

		public string ElementName { get; set; }
	}
}
