using DeliveryService.Logic;
using DeliveryService.Models;
using DeliveryService.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryService.Test
{
	[TestClass]
    public class OrderLogicTest
    {
		private readonly Mock<IOrderRepo> _orderRepo;
		private readonly Mock<IPriceCalculator> _pricingCalculator;
		public OrderLogicTest()
		{
			_orderRepo = new Mock<IOrderRepo>();
			_pricingCalculator = new Mock<IPriceCalculator>();
		}

        [TestMethod]
        public void OrderLogic_Get_ShouldReturnAllOrders_WhenRequested()
        {
			var orderLogicIns = new OrderLogic(_orderRepo.Object, _pricingCalculator.Object);
			_orderRepo.Setup(x => x.GetAll()).Returns(GetOrders);
			var result = orderLogicIns.Get();

			Assert.IsNotNull(result);
			Assert.AreEqual(2, result.Count());
        }

		private IEnumerable<Order> GetOrders()
		{
			return new List<Order>
			{
				new Order
				{
					CustomerId = new Guid(),
					DeliveryDate = DateTime.Today,
					Distance = 1,
					FloorNumber = 2,
					Price = 1499.8
				},
				new Order
				{
					CustomerId = new Guid(),
					DeliveryDate = DateTime.Today,
					Distance = 12,
					FloorNumber = 2,
					Price = 1699.8
				}
			};
		}
    }
}
