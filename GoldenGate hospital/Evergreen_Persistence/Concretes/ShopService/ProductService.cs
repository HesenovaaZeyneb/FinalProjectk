using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evergreen_Application.Abstractions.ShopServices;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.EntityFrameworkCore;

namespace Evergreen_Persistence.Concretes.ShopService
{
	public class ProductService : IProductService
	{
		AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public void Create(ProductVm productVm)
		{
			Product product = new Product()
			{
				Id = productVm.Id,
				Name = productVm.Name,
				Description = productVm.Description,
				ImgUrl = productVm.ImgUrl,
				Price = productVm.Price,
				Category = productVm.Category,
				CategoryId = productVm.CategoryId,
			};
			_context.Products.Add(product);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var product = _context.Products.FirstOrDefault(x => x.Id == id);
			_context.Products.Remove(product);
			_context.SaveChanges();
		}

		public ProductVm Get(int id)
		{
			var product = _context.Products.Include(x=>x.Category).FirstOrDefault(x=> x.Id == id);
			ProductVm productvm = new ProductVm()
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				ImgUrl = product.ImgUrl,
				Price = product.Price,
				Category = product.Category,
				CategoryId = product.CategoryId,

			};
			return productvm;
		}

		public List<Product> GetAll()
		{
			return _context.Products.Include(x=>x.Category).ToList();
		}

		public void Update(ProductVm newproduct)
		{
			var oldproduct = _context.Products.Include(x=>x.Category).FirstOrDefault(x=>x.Id == newproduct.Id);
			oldproduct.Id = newproduct.Id;
			oldproduct.Name= newproduct.Name;
			oldproduct.Description= newproduct.Description;
			oldproduct.Category= newproduct.Category;
			oldproduct.Price= newproduct.Price;
			oldproduct.CategoryId= newproduct.CategoryId;
			_context.SaveChanges();
		}
	}
}
