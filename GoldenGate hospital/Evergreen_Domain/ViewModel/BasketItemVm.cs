using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evergreen_Domain.ViewModel
{
    public class BasketItemVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count {  get; set; }
        public double Price { get; set; }
        public string? ImgUrl { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        
    }
}
