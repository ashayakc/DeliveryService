using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryService.Logic
{
    public interface IOrderRule
    {
		void CalculatePrice(Order order);
    }
}
