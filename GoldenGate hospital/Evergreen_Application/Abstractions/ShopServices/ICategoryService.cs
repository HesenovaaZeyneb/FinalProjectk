using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Domain.ViewModel;
using Evergreen_Domain;

namespace Evergreen_Application.Abstractions.ShopServices
{
	public interface ICategoryService
	{
		List<Category> GetAll();
		void Create(CategoryVm categoryvm);
		void Update(CategoryVm newcategory);
		void Delete(int id);
		CategoryVm Get(int id);
	}
}
