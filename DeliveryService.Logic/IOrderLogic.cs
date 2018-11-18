using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryService.Logic
{
    public interface IOrderLogic
    {
		void Create(Order order);

		IEnumerable<Order> Get();
    }
}
