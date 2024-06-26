﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ASPMVC.Models.Enums;

namespace ASPMVC.Models
{
	public class OrderModel
	{
		public int Id { get; set; }
		[DataType(DataType.Date)]
		public DateTime DateTime { get; set; }
		[Required]
        [Range(0, 10000000000)]
        public double TotalPrice { get; set; }
		public string ClientId { get; set; }
		[Required]
		public OrderState State { get; set; }

		public int PaymentMethodId { get; set; }

		public int DeliveryMethodId { get; set; }

		[ForeignKey("PaymentMethodId")]
		public virtual PaymentMethodsModel PaymentMethod { get; set; }

		[ForeignKey("DeliveryMethodId")]
		public virtual DeliveryMethodsModel DeliveryMethod { get; set; }

		[ForeignKey("ClientId")]
		public virtual ApplicationUser User { get; set; }
		public virtual ICollection<OrderProduct> OrderProducts { get; set; }

	}
}