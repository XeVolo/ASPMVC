using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
	public class AnnouncementViewModel
	{
		public string SellerId { get; set; }
		[Required]
		[MaxLength(50)]
		public string Title { get; set; }
		public string Description { get; set; }
		[Required]
		[Range(1, 10000000000)]
		public int Quantity { get; set; }
		[Required]
		public string Status { get; set; }
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
		[Required]
		[Range(0.01, 10000000000)]
		public double Price { get; set; }

		public int CategoryId { get; set; }
	}
}