using DeliveryService.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryService.Logic
{
    public interface IPriceCalculator
    {
		double Calculate(Order order);
    }
}
