using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evergreen_Domain.OrderandBasket
{
    public class WhistlesItem:BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string UserId { get; set; }
        public User? User { get; set; }

        public double Price { get; set; }
    }
}
