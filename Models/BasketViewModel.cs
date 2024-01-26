using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
	public class BasketViewModel
	{
		public int ProductId { get; set; }

		public string Name { get; set; }
		public double Price { get; set; }
		public int Quantity { get; set; }
	}
}