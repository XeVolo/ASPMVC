using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SystemyBazDanychP1.Models
{
	public class SpecialOfferModel
	{
		public int Id { get; set; }
		public int SaleAnnouncementId { get; set; }
		public int PromotionValue { get; set; }
		[DataType(DataType.Date)]
		public DateTime ExpirationDate { get; set; }
		public virtual SaleAnnouncementModel SaleAnnouncement { get; set; }
	}
}