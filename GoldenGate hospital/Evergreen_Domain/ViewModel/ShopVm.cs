using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evergreen_Domain.ViewModel
{
    public class ShopVm
    {
        public int? CategoryId { get; set; }
        public int? Order {  get; set; }
        public string? Search { get; set; } 
        public List<Product> products;
        public List<Category> categories;
    }
}
