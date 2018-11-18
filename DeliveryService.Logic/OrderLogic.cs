using System;
using System.Collections.Generic;
using DeliveryService.Models;
using DeliveryService.Repository;

namespace DeliveryService.Logic
{
	public class OrderLogic : IOrderLogic
	{
		private readonly IOrderRepo _orderRepo;
		private readonly IPriceCalculator _priceCalculator;
		public OrderLogic(IOrderRepo orderRepo, IPriceCalculator priceCalculator)
		{
			_orderRepo = orderRepo;
			_priceCalculator = priceCalculator;
		}

		public void Create(Order order)
		{
			_priceCalculator.Calculate(order);
			_orderRepo.Create(order);
		}

		public IEnumerable<Order> Get()
		{
			return _orderRepo.GetAll();
		}
	}
}
