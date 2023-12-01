using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SystemyBazDanychP1.Models
{
	public class BasketConnectorModel
	{
		public int Id { get; set; }	
		public int OrderProductId { get; set; }
		public int BasketId { get; set; }

		public virtual OrderProduct Product { get; set; }
		public virtual BasketModel Basket { get; set; }
	}
}