using System;
using System.Collections.Generic;
using System.Text;
using DeliveryService.Models;

namespace DeliveryService.Logic
{
	public class PriceCalculator : IPriceCalculator
	{
		private readonly IEnumerable<IOrderRule> _rules;
		public PriceCalculator(IEnumerable<IOrderRule> rules)
		{
			_rules = rules;
		}

		public double Calculate(Order order)
		{
			foreach(var rule in _rules)
			{
				rule.CalculatePrice(order);
			}
			return order.Price;
		}
	}
}
