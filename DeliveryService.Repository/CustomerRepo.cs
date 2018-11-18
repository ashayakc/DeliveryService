using System;
using System.Collections.Generic;
using System.Text;
using DeliveryService.Models;
using System.Linq;

namespace DeliveryService.Repository
{
	public class CustomerRepo : ICustomerRepo
	{
		private readonly DeliveryServiceContext _deliveryServiceContext;
		public CustomerRepo(DeliveryServiceContext deliveryServiceContext)
		{
			_deliveryServiceContext = deliveryServiceContext;
		}

		public IEnumerable<Customer> GetAll()
		{
			return _deliveryServiceContext.Customers;
		}

		public Customer GetById(Guid id)
		{
			return _deliveryServiceContext.Customers.FirstOrDefault(x => x.Id == id);
		}
	}
}
