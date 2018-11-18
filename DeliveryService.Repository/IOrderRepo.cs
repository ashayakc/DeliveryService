using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryService.Repository
{
    public interface IOrderRepo
    {
		IEnumerable<Order> GetByCustomerId(Guid customerId);

		IEnumerable<Order> GetAll();

		void Create(Order order);
    }
}
