using ASPMVC.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
	public class SaleAnnouncementModel
	{
		public int Id { get; set; }
		public string SellerId { get; set; }
		[Required]
		[MaxLength(50)]
		public string Title { get; set; }
		public string Description { get; set; }
		[Required]
		[Range(0, 10000000000)]
        public int Quantity { get; set; }
		public int ProductId { get; set; }
		[Required]
		public SaleAnnouncementState State { get; set; }
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }


		[ForeignKey("SellerId")]
		public virtual ApplicationUser User { get; set; }
		public virtual ProductModel Product { get; set; }
		public virtual ICollection<OpinionModel> Opinion { get; set; }
		public virtual ICollection<FilePathsModel> FilePaths { get; set; }

	

    }
}