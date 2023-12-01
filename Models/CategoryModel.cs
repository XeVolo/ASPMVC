using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemyBazDanychP1.Models
{
	public class CategoryModel
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
	}
}