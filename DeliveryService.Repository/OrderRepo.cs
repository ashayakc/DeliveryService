using System;
using System.Collections.Generic;
using System.Text;
using DeliveryService.Models;
using System.Linq;

namespace DeliveryService.Repository
{
	public class OrderRepo : IOrderRepo
	{
		private readonly DeliveryServiceContext _context;
		public OrderRepo(DeliveryServiceContext context)
		{
			_context = context;
		}

		public void Create(Order order)
		{
			_context.Orders.Add(order);
			_context.SaveChanges();
		}

		public IEnumerable<Order> GetAll()
		{
			return _context.Orders;
		}

		public IEnumerable<Order> GetByCustomerId(Guid customerId)
		{
			return _context.Orders.Where(x => x.CustomerId == customerId);
		}
	}
}
