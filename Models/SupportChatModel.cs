using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;

namespace SystemyBazDanychP1.Models
{
	public class SupportChatModel
	{
		public int Id { get; set; }
		public string ClientId { get; set; }
		public string AdminId { get; set; }

		public string Conversation { get; set; }
		public string ApplicationUser_Id { get; set; }

        [ForeignKey("ClientId")]
		public virtual ApplicationUser User { get; set; }

		[ForeignKey("AdminId")]
		public virtual ApplicationUser Admin { get; set; }

	}
}