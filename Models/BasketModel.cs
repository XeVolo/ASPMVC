using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASPMVC.Models
{
	public class BasketModel
	{
		public int Id { get; set; }
		public string ClientId { get; set; }

		[ForeignKey("ClientId")]
		public virtual ApplicationUser User { get; set; }
		public virtual ICollection<BasketConnectorModel> OrderProducts { get; set; }
		

	}
}