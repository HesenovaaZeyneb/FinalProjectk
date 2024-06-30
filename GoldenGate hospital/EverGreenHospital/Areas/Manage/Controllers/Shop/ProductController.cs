using Evergreen_Application.Abstractions.ShopServices;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EverGreenHospital.Areas.Manage.Controllers.Shop
{
	[Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
	{
		AppDbContext _context;
		IProductService _service;
		IWebHostEnvironment _environment;

		

		public ProductController(AppDbContext context, IProductService service,IWebHostEnvironment environment)
		{
			_context = context;
			_service = service;
			_environment = environment;
		}

		public IActionResult AllProduct()
		{
			return View(_service.GetAll());
		}
		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Categories = _context.Categories.ToList();
			return View();
		}
		[HttpPost]
		public IActionResult Create(ProductVm productVm)
		{
			if (!productVm.ImgFile.ContentType.Contains("image/"))
			{
				return View();
			}
			string path = _environment.WebRootPath + @"\Upload\Product\";
			string filename=productVm.ImgFile.FileName;
			using(FileStream stream=new FileStream(path+filename,FileMode.Create))
			{
				productVm.ImgFile.CopyTo(stream);
			}
			productVm.ImgUrl=filename;
			_service.Create(productVm);
			return RedirectToAction(nameof(AllProduct));
		}
		[HttpGet]
		public IActionResult Update(int id)
		{
			ViewBag.Categories = _context.Categories.ToList();
			return View(_service.Get(id));
		}
		[HttpPost]
		public IActionResult Update(ProductVm productVm)
		{
			if (!productVm.ImgFile.ContentType.Contains("image/"))
			{
				return View();
			}
			string path = _environment.WebRootPath + @"\Upload\Product\";
			string filename = productVm.ImgFile.FileName;
			
			if(_service.Get(productVm.Id)!=null) 
			{
				 FileInfo fileInfo = new FileInfo(path+filename);
			    if(fileInfo.Exists)
			    {
				   fileInfo.Delete();
			    }
				
			}
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                productVm.ImgFile.CopyTo(stream);
				productVm.ImgUrl = filename;
            _service.Update(productVm);
            }
           
            return RedirectToAction(nameof(AllProduct));
			
			
		}
		public IActionResult Delete(int id)
		{
			_service.Delete(id);
			return RedirectToAction(nameof(AllProduct));
		}
		public IActionResult Detail(int id)
		{
			return View(_service.Get(id));
		}

	}
}
