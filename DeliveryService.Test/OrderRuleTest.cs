using DeliveryService.Logic;
using DeliveryService.Models;
using DeliveryService.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeliveryService.Test
{
	[TestClass]
    public class OrderRuleTest
    {
		private readonly Mock<IOrderRepo> _orderRepo;
		private readonly Mock<ICustomerRepo> _customerRepo;
		public OrderRuleTest()
		{
			_orderRepo = new Mock<IOrderRepo>();
			_customerRepo = new Mock<ICustomerRepo>();
		}

		[TestMethod]
		public void DistanceRule_CalculatePrice_WhenDistanceIsLessThan10_ShoulldReturn1092()
		{
			var rule = new DistanceRule();
			var order = new Models.Order
			{
				CustomerId = Guid.NewGuid(),
				Distance = 5
			};
			rule.CalculatePrice(order);

			Assert.AreEqual(1098.9, order.Price);
		}

		[TestMethod]
		public void DistanceRule_CalculatePrice_WhenDistanceIsBetween10And50_ShoulldReturn1248()
		{
			var rule = new DistanceRule();
			var order = new Models.Order
			{
				CustomerId = Guid.NewGuid(),
				Distance = 20
			};
			rule.CalculatePrice(order);

			Assert.AreEqual(1248.75, order.Price);
		}

		[TestMethod]
		public void DistanceRule_CalculatePrice_WhenDistanceIsGreaterThan50_ShoulldReturnCalculatedValue()
		{
			var rule = new DistanceRule();
			var order = new Models.Order
			{
				CustomerId = Guid.NewGuid(),
				Distance = 51
			};
			rule.CalculatePrice(order);

			Assert.AreEqual(1249, order.Price);
		}

		[TestMethod]
		public void FloorRule_CalculatePrice_WhenGroundFloor_ShoulldReturn999()
		{
			var rule = new FloorRule();
			var order = new Models.Order
			{
				CustomerId = Guid.NewGuid(),
				FloorNumber = 0
			};
			rule.CalculatePrice(order);

			Assert.AreEqual(999, order.Price);
		}


		[TestMethod]
		public void FloorRule_CalculatePrice_WhenFloorBetween0To5_ShoulldReturnAppropriateValue()
		{
			var rule = new FloorRule();
			var order = new Models.Order
			{
				CustomerId = Guid.NewGuid(),
				FloorNumber = 2
			};
			rule.CalculatePrice(order);

			Assert.AreEqual(1048.95, order.Price);
		}

		[TestMethod]
		public void FloorRule_CalculatePrice_WhenFloorGreaterThan5_ShoulldReturnAppropriateValue()
		{
			var rule = new FloorRule();
			var order = new Models.Order
			{
				CustomerId = Guid.NewGuid(),
				FloorNumber = 6
			};
			rule.CalculatePrice(order);

			Assert.AreEqual(1052.95, order.Price);
		}

		[TestMethod]
		public void WeekendRule_CalculatePrice_WhenDeliveryDateIsWeekend_ShoulldReturnAppropriateValue()
		{
			var rule = new WeekendRule();
			var order = new Models.Order
			{
				CustomerId = Guid.NewGuid(),
				FloorNumber = 6,
				DeliveryDate = new DateTime(2018, 11, 18)
			};
			rule.CalculatePrice(order);

			Assert.AreEqual(1498.5, order.Price);
		}

		[TestMethod]
		public void NewCustomerRule_CalculatePrice_WhenOrderIsFromNewCustomer_ShoulldReturnAppropriateValue()
		{
			var rule = new NewCustomerRule(_orderRepo.Object);
			_orderRepo.Setup(x => x.GetByCustomerId(It.IsAny<Guid>())).Returns(GetOrders().Where(x => x.Id == Guid.NewGuid()));
			var order = new Models.Order
			{
				CustomerId = Guid.NewGuid(),
				FloorNumber = 6,
				DeliveryDate = new DateTime(2018, 11, 18)
			};
			rule.CalculatePrice(order);

			Assert.AreEqual(849.15, order.Price);
		}

		[TestMethod]
		public void GoldenCustomerRule_CalculatePrice_WhenOrderIsFromGoldenCustomer_ShoulldReturnAppropriateValue()
		{
			var rule = new GoldenCustomerRule(_orderRepo.Object, _customerRepo.Object);
			_orderRepo.Setup(x => x.GetByCustomerId(It.IsAny<Guid>())).Returns(GetOrders);
			_customerRepo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Customer
			{
				Id = Guid.Parse("b4300366-25e6-406f-adf2-d97cc7c75dcb"),
				Name = "Ashay",
				Rating = 5
			});
			var order = new Order
			{
				CustomerId = Guid.Parse("b4300366-25e6-406f-adf2-d97cc7c75dcb"),
				FloorNumber = 6,
				DeliveryDate = new DateTime(2018, 11, 18)
			};
			rule.CalculatePrice(order);

			Assert.AreEqual(899.1, order.Price);
		}

		[TestMethod]
		public void CouponRule_CalculatePrice_WhenOrderIsFromCouponHolderCustomer_ShoulldReturnAppropriateValue()
		{
			var rule = new CouponRule(_customerRepo.Object);
			_customerRepo.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(new Customer
			{
				Id = Guid.Parse("b4300366-25e6-406f-adf2-d97cc7c75dcb"),
				Name = "Ashay",
				Rating = 5,
				IsCouponHolder = true
			});
			var order = new Order
			{
				CustomerId = Guid.Parse("b4300366-25e6-406f-adf2-d97cc7c75dcb"),
				FloorNumber = 6,
				DeliveryDate = new DateTime(2018, 11, 18)
			};
			rule.CalculatePrice(order);

			Assert.AreEqual(799.2, order.Price);
		}

		private IEnumerable<Order> GetOrders()
		{
			return new List<Order>
			{
				new Order
				{
					CustomerId = Guid.Parse("b4300366-25e6-406f-adf2-d97cc7c75dcb"),
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
