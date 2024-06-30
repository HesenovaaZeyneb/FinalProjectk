using System.IO;
using Evergreen_Application.Abstractions;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EverGreenHospital.Areas.Manage.Controllers

{   [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class DepartmentController : Controller
	{
		
		private readonly IDepartmentService _service;
		private readonly IWebHostEnvironment _environment;

		public DepartmentController(IDepartmentService service, IWebHostEnvironment environment)
        {
			this._service = service;
			this._environment = environment;
		}
        public IActionResult AllDeparment()
		{
			return View(_service.GetAll());
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(DepartmentVm departmentVm)
		{
			string path = _environment.WebRootPath + @"\Upload\Icon\";
			string filename = departmentVm.ImgFile.FileName;
			using(FileStream stream=new FileStream(path + filename, FileMode.Create))
			{
				departmentVm.ImgFile.CopyTo(stream);
			}
			departmentVm.ImgUrl= filename;

			_service.Create(departmentVm);
			return RedirectToAction(nameof(AllDeparment));
		}
		[HttpGet]
		public IActionResult Update(int id)
		{
			return View(_service.Get(id));
		}
		[HttpPost]
		public IActionResult Update(DepartmentVm departmentVm)
		{
            string path = _environment.WebRootPath + @"\Upload\Icon\";
            string filename = departmentVm.ImgFile.FileName;
			if(_service.Get(departmentVm.Id) != null)
			{
				FileInfo fileInfo = new FileInfo(filename+path);
			   if(fileInfo.Exists)
			   {
				fileInfo.Delete();
			   }

			}
			
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))
            {
                departmentVm.ImgFile.CopyTo(stream);
				departmentVm.ImgUrl=filename;
            _service.Update(departmentVm);
            }
			
			return RedirectToAction(nameof(AllDeparment));

		}
		public IActionResult Delete(int id)
		{
			if (id == 0) { return View(); }
			_service.Delete(id);
			return RedirectToAction(nameof(AllDeparment));
		}
	}
}
