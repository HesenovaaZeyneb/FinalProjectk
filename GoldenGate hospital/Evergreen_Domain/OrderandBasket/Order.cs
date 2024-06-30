using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evergreen_Domain.OrderandBasket
{
	public class Order:BaseEntity
	{
		public string UserId { get; set; }
		public User User { get; set; }
		public List<BasketItem> BasketItems { get; set; }
		public bool? Status { get; set; }
		public string Address { get; set; }
		public DateTime CreateDate { get; set; }
		public double TotalPrice { get; set; }
		public string Code { get; set; }
	}
}
