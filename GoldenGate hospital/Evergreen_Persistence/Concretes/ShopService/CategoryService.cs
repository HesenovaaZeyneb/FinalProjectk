using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Application.Abstractions.ShopServices;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;

namespace Evergreen_Persistence.Concretes.ShopService
{
	public class CategoryService : ICategoryService
	{
		AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public void Create(CategoryVm categoryvm)
		{
			Category category = new Category()
			{
				Name = categoryvm.Name,
		


			};
			_context.Categories.Add(category);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var category = _context.Categories.FirstOrDefault(c => c.Id == id);
			_context.Categories.Remove(category);
			
			_context.SaveChanges();
		}

		public CategoryVm Get(int id)
		{
			var category= _context.Categories.FirstOrDefault(x=>x.Id == id);
			CategoryVm categoryVm = new CategoryVm()
			{
				Name = category.Name,
				
				Id=category.Id,
			};
			return categoryVm;
		}

		public List<Category> GetAll()
		{
			return _context.Categories.ToList();
		}

		public void Update(CategoryVm newcategory)
		{
			var oldcategory = _context.Categories.FirstOrDefault(x=>x.Id==newcategory.Id);
			oldcategory.Name= newcategory.Name;
			
			oldcategory.Id= newcategory.Id;
			_context.SaveChanges();
		}
	}
}
