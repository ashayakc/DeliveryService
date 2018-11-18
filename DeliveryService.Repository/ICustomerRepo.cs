using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryService.Repository
{
    public interface ICustomerRepo
    {
		Customer GetById(Guid id);

		IEnumerable<Customer> GetAll();
    }
}
