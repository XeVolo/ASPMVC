using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
	public class CategoryModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? ParentCategoryId { get; set; }
		public int Left { get; set; }
		public int Right { get; set; }

		[ForeignKey("ParentCategoryId")]
		public virtual CategoryModel ParentCategory { get; set; }

		public virtual ICollection<CategoryModel> Subcategories { get; set; }
	}
}