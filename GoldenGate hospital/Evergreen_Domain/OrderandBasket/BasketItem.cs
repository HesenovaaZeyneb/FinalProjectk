using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evergreen_Domain.OrderandBasket
{
	public class BasketItem:BaseEntity
	{
		public int ProductId { get; set; }
		public Product Product { get; set; }
		public string UserId { get; set; }
		public User? User { get; set; }
		public int Count { get; set; }
		public int? OrderId { get; set; }
		public Order? Order { get; set; }
		
		public double Price { get; set; }
	
	}
}
