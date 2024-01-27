using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
	public class FilePathsModel
	{
		public int Id { get; set; }
		public string Path { get; set; }
		public int SaleAnnouncementId { get; set; }

		[ForeignKey("SaleAnnouncementId")]
		public virtual SaleAnnouncementModel SaleAnnouncementModel { get; set;}
	}
}