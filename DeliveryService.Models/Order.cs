using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryService.Models
{
    public class Order
    {
		public Guid Id { get; set; }

		public Guid CustomerId { get; set; }

		public int FloorNumber { get; set; }

		public int Distance { get; set; }

		public DateTime DeliveryDate { get; set; }

		public double Price { get; set; }
	}
}
