using Evergreen_Application.Abstractions.ShopServices;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverGreenHospital.Controllers
{
	public class ShopController : Controller
	{
		IProductService _service;
		AppDbContext _context;

		public ShopController(IProductService service,AppDbContext context)
		{
			_service = service;
			_context = context;
		}

		public  IActionResult Shops(string? search,int? order,int? categoryId)
		{
			IQueryable<Product> query= _context.Products.AsQueryable();
			switch (order)
			{
				case 1:
					query=query.OrderBy(c=>c.Price); break;
					case 2:
					query=query.OrderByDescending(c=>c.Id); break;
					case 3:
					query=query.OrderBy(query=>query.Name); break;
			}

			if(!String.IsNullOrEmpty(search))
			{
				query=query.Where(p=>p.Name.ToLower().Contains(search.ToLower()));
			}
			if(categoryId!=null)
			{
				query=query.Where(p=>p.CategoryId==categoryId);
			}
			ShopVm shopVm = new ShopVm()
			{
				categories=_context.Categories.Include(x=>x.Products).ToList(),
				products=query.ToList(),
				CategoryId=categoryId,
				Order=order,
				Search=search,
				
			};
			
			return View(shopVm);
		}
		public IActionResult Detail(int id)
		{
			var product=_service.Get(id);
			ViewBag.Realed = _context.Products.Where(x => x.CategoryId == product.CategoryId && x.Id != product.Id).ToList();
			return View(product);
		}
	}
}
