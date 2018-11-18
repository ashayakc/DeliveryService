using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DeliveryService.Models;
using DeliveryService.Logic;

namespace DeliveryService.Controllers
{
    public class OrdersController : Controller
    {
		public IOrderLogic _orderLogic { get; set; }
		public OrdersController(IOrderLogic orderLogic)
		{
			_orderLogic = orderLogic;
		}

        public IActionResult Index()
        {
            return View();
        }

		[HttpPost, Route("v1/orders")]
		public JsonResult Create([FromBody] Order order)
		{
			_orderLogic.Create(order);
			return Json("Order created successfully");
		}

		[HttpGet, Route("v1/orders")]
		public IEnumerable<Order> Get()
		{
			return _orderLogic.Get();
		}
	}
}