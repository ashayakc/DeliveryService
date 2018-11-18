using System;

namespace DeliveryService.Models
{
    public class Customer
    {
		public Guid Id { get; set; }

		public string Name { get; set; }

		public int Rating { get; set; }

		public bool IsCouponHolder { get; set; }
	}
}
