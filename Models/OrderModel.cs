using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SystemyBazDanychP1.Models
{
	public class OrderModel
	{
		public int Id { get; set; }
		[DataType(DataType.Date)]
		public DateTime DateTime { get; set; }
		[Required]
        [Range(0, 10000000000)]
        public double TotalPrice { get; set; }
		public string ClientId { get; set; }
		[Required]
		public string Status { get; set; }
		[ForeignKey("ClientId")]
		public virtual ApplicationUser User { get; set; }
		public virtual ICollection<OrderProduct> OrderProducts { get; set; }

	}
}