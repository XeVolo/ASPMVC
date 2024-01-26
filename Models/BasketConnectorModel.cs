using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
	public class BasketConnectorModel
	{
		public int Id { get; set; }	
		public int ProductId { get; set; }
		public int BasketId { get; set; }

		public virtual ProductModel Product { get; set; }
		public virtual BasketModel Basket { get; set; }
	}
}