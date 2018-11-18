using DeliveryService.Logic;
using DeliveryService.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryService.Test
{
	[TestClass]
    public class OrderIntegrationTest
    {
		private readonly Mock<IPriceCalculator> _priceCalculator;
		private readonly Mock<IOrderRepo> _orderRepo;
		private readonly Mock<ICustomerRepo> _customerRepo;
		public OrderIntegrationTest()
		{
			_priceCalculator = new Mock<IPriceCalculator>();
			_orderRepo = new Mock<IOrderRepo>();
			_customerRepo = new Mock<ICustomerRepo>();
		}

		[TestMethod]
		public void PricingCalculator_WhenNewCustomerOrder_ReturnsAppropriatePrice()
		{
			var pricingCalc = new PriceCalculator(new List<IOrderRule>
			{
				new DistanceRule(),
				new FloorRule(),
				new WeekendRule(),
				new NewCustomerRule(_orderRepo.Object),
				new GoldenCustomerRule(_orderRepo.Object, _customerRepo.Object),
				new CouponRule(_customerRepo.Object)
			});

			var order = new Models.Order
			{
				CustomerId = new Guid(),
				DeliveryDate = DateTime.Today,
				Distance = 5,
				FloorNumber = 5
			};

			pricingCalc.Calculate(order);

			Assert.AreEqual(849.15, order.Price);
		}
    }
}
