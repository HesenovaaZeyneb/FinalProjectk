using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;

namespace Evergreen_Application.Abstractions.ShopServices
{
	public  interface IProductService
	{
		List<Product> GetAll();
		void Create(ProductVm productVm);
		void Update(ProductVm newproduct);
		void Delete(int id);
		ProductVm Get(int id);
	}
}
