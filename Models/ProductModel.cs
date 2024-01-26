using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
	public class ProductModel
	{
		public int Id { get; set; }
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
		public int CategoryId { get; set; }
		[Required]
        [Range(0.01, 10000000000)]
        public double Price { get; set; }
		public bool IsDeleted { get; set; }
		public virtual CategoryModel Category { get; set; }


	}
}