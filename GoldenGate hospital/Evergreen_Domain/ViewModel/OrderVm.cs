using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Domain.OrderandBasket;

namespace Evergreen_Domain.ViewModel
{
	public class OrderVm
	{
		public string Adress {  get; set; }
		
		public List<BasketItem>? BasketItems { get; set; }
	}
}
