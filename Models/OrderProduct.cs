using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SystemyBazDanychP1.Models
{
	public class OrderProduct
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public int OrderId { get; set; }
		[Required]
        [Range(0.01, 10000000000)]
        public double Price { get; set; }
        [Required]
        [Range(1, 10000000000)]
        public int Quantity { get; set; }
		[Required]
        [Range(0.01, 10000000000)]
        public double TotalPrice { get; set; }

		public virtual ProductModel Product { get; set; }
		public virtual OrderModel Order { get; set;}
		public virtual ICollection<BasketConnectorModel> BasketConnectors { get; set;}

	}
}