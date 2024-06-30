using Evergreen_Application.Abstractions.ShopServices;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EverGreenHospital.Areas.Manage.Controllers.Shop
{
	[Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
	{
		ICategoryService _service;
		AppDbContext _context;
		IWebHostEnvironment _enviroment;
        public CategoryController(ICategoryService service, AppDbContext context, IWebHostEnvironment enviroment)
        {
            _service = service;
            _context = context;
            _enviroment = enviroment;
        }

        public IActionResult AllCategory()
		{
			return View(_service.GetAll());
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(CategoryVm categoryVm)
		{
            //if (!categoryVm.ImgFile.ContentType.Contains("image/"))
            //{
            //    return View();
            //}
            //string path = _enviroment.WebRootPath + @"\Upload\Category\";
            //string filename = categoryVm.ImgFile.FileName;
            //using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            //{
            //    categoryVm.ImgFile.CopyTo(stream);
                
            //}
            //categoryVm.ImgUrl = filename;
                _service.Create(categoryVm);
           
			return RedirectToAction(nameof(AllCategory));
		}
		[HttpGet]
		public IActionResult Update(int id)
		{
			
			return View(_service.Get(id));
		}
		[HttpPost]
		public IActionResult Update(CategoryVm category) 
		{
            //if (!category.ImgFile.ContentType.Contains("image/"))
            //{
            //    return View();
            //}
            //string path = _enviroment.WebRootPath + @"\Upload\Product\";
            //string filename = category.ImgFile.FileName;

            //if (_service.Get(category.Id) != null)
            //{
            //    FileInfo fileInfo = new FileInfo(path + filename);
            //    if (fileInfo.Exists)
            //    {
            //        fileInfo.Delete();
            //    }

            //}
            //using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            //{
            //    category.ImgFile.CopyTo(stream);
            //    category.ImgUrl = filename;
            //   
            //}
            _service.Update(category);
            return RedirectToAction(nameof(AllCategory));
		}
		public IActionResult Delete(int id)
		{
			
			_service.Delete(id);
			
			return RedirectToAction(nameof(AllCategory));
		}

	}
}
