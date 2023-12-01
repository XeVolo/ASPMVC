using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SystemyBazDanychP1.Models
{
	public class OpinionModel
	{
		public int Id { get; set; }
		public string ClientId { get; set; }
		public int SaleAnnouncementId { get; set; }
		[Required]
		public string Description { get; set; }

		[ForeignKey("ClientId")]
		public virtual ApplicationUser User { get; set; }
		public virtual SaleAnnouncementModel SaleAnnouncement { get; set; }

	}
}