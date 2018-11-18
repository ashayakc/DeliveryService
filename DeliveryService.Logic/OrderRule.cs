using System;
using System.Collections.Generic;
using System.Text;
using DeliveryService.Models;
using DeliveryService.Repository;
using System.Linq;

namespace DeliveryService.Logic
{
	public class DistanceRule : IOrderRule
	{
		public void CalculatePrice(Order order)
		{
			if (order.Distance <= 10)
			{
				order.Price = 1098.9;
				return;
			}

			if (order.Distance > 10 && order.Distance <= 50)
			{
				order.Price = 1248.75;
				return;
			}				

			order.Price = ((order.Distance - 50) * 0.25) + 1248.75;
		}
	}

	public class FloorRule : IOrderRule
	{
		public void CalculatePrice(Order order)
		{
			if (order.FloorNumber <= 0)
			{
				order.Price = 999;
				return;
			}

			if (order.FloorNumber > 0 && order.FloorNumber <= 5)
			{
				order.Price = 1048.95;
				return;
			}

			order.Price = ((order.FloorNumber - 5) * 4) + 1048.95;
		}
	}

	public class WeekendRule : IOrderRule
	{
		public void CalculatePrice(Order order)
		{
			if (order.DeliveryDate.DayOfWeek == DayOfWeek.Sunday)
				order.Price = 1498.5;
		}
	}

	public class NewCustomerRule : IOrderRule
	{
		private readonly IOrderRepo _orderRepo;
		public NewCustomerRule(IOrderRepo orderRepo)
		{
			_orderRepo = orderRepo;
		}

		public void CalculatePrice(Order order)
		{
			var ordersFromDb = _orderRepo.GetByCustomerId(order.CustomerId);
			if (!ordersFromDb.Any())
				order.Price = 849.15;
		}
	}

	public class GoldenCustomerRule : IOrderRule
	{
		private readonly IOrderRepo _orderRepo;
		private readonly ICustomerRepo _customerRepo;
		public GoldenCustomerRule(IOrderRepo orderRepo, ICustomerRepo customerRepo)
		{
			_orderRepo = orderRepo;
			_customerRepo = customerRepo;
		}

		public void CalculatePrice(Order order)
		{
			var ordersFromDb = _orderRepo.GetByCustomerId(order.CustomerId);
			if (ordersFromDb.Any() && IsGoldenCustomer(order.CustomerId))
			{
				order.Price = 899.1;
			}
		}

		private bool IsGoldenCustomer(Guid customerId)
		{
			var customer = _customerRepo.GetById(customerId);
			if (customer != null && customer.Rating == 5)
				return true;
			return false;
		}
	}

	public class CouponRule : IOrderRule
	{
		private readonly ICustomerRepo _customerRepo;
		public CouponRule(ICustomerRepo customerRepo)
		{
			_customerRepo = customerRepo;
		}

		public void CalculatePrice(Order order)
		{
			var customer = _customerRepo.GetById(order.CustomerId);
			if(customer != null && customer.IsCouponHolder)
			{
				order.Price = 799.2;
			}
		}
	}
}
