using Evergreen_Application.Abstractions;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EverGreenHospital.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        IBlogService _service;
        IWebHostEnvironment _environment;

        public BlogController(IBlogService service, IWebHostEnvironment environment )
        {
            _service = service;
            _environment = environment;
        }

        public IActionResult AllBlogs()
        {
            return View(_service.Getall());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Create(BlogVm blogVm)
        {
			if (!blogVm.ImgFile.ContentType.Contains("image/"))
			{
				return View();
			}
			string path=_environment.WebRootPath+ @"\Upload\Blog\";
            string filename= blogVm.ImgFile.FileName;
            using(FileStream stream =new FileStream(path+filename,FileMode.Create))
            {
               blogVm.ImgFile.CopyTo(stream);
            }          
            blogVm.ImgUrl=filename;
            _service.Create(blogVm); 
            return RedirectToAction(nameof(AllBlogs));

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
           
            return View( _service.Get(id));
        }
        [HttpPost]
        public IActionResult Update(BlogVm blogVm)
        {
            string path = _environment.WebRootPath + @"\Upload\Blog\";
            string filename = Guid.NewGuid() + blogVm.ImgFile.FileName;
            if (!ModelState.IsValid) { return View(); }
            if (_service.Get(blogVm.Id) != null)
            {
                FileInfo fileinfo = new FileInfo(path + filename);
                if (fileinfo.Exists)
                {
                    fileinfo.Delete();
                }
            }
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                blogVm.ImgFile.CopyTo(stream);
               
                blogVm.ImgUrl = filename; 
                _service.Update(blogVm);
            }
           
            return RedirectToAction(nameof(AllBlogs));
        }
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(AllBlogs));
        }
        public IActionResult Detail(int id)
        {
            return View(_service.Get(id));
        }
    }
}
