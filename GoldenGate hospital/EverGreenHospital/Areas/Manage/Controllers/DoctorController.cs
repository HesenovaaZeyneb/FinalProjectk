using Evergreen_Application.Abstractions;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EverGreenHospital.Areas.Manage.Controllers
{
	[Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class DoctorController : Controller
	{
		IDoctorsService _service;
		IWebHostEnvironment _environment;
         AppDbContext _context;
		IUserService _userService;
		private readonly UserManager<User> _userManager;
		public DoctorController(AppDbContext context, IDoctorsService service, IWebHostEnvironment environment, IUserService userService, UserManager<User> userManager)
		{
			_environment = environment;
			_service = service;
			_context = context;
			_userService = userService;
			_userManager = userManager;
		}
		public IActionResult AllDoctors()
		{
			return View(_service.GetAll());
		}
		[HttpGet]
		public IActionResult Create()
		{
			var users = _userService.GetAllUsers();
			
			foreach (var item in users)
			{
				ViewBag.Doctors= _context.Users.Where(x=>x.DoctorId=="Doctor").ToList();
			}



              ViewBag.Departments=_context.Departments.ToList();
			

			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(DoctorsVm doctorVm)
		{
			
			if (!doctorVm.ImgFile.ContentType.Contains("image/"))
			{
				return View();
			}
			User user = await _userManager.FindByIdAsync(doctorVm.UserId);
			doctorVm.FullName = user.Name+" "+user.Surname;
			doctorVm.UserId = user.Id;
			var path = _environment.WebRootPath + @"\Upload\Doctor\";
			var filename= Guid.NewGuid()+doctorVm.ImgFile.FileName;
		    using (FileStream stream = new FileStream(path + filename, FileMode.Create))
			{
				doctorVm.ImgFile.CopyTo(stream);
			}
		    doctorVm.ImgUrl=filename;
			
           _service.Create(doctorVm);
			return RedirectToAction(nameof(AllDoctors));
		}
		[HttpGet]
		public IActionResult Update(int id)
		{
			
		 
			ViewBag.Departments = _context.Departments.ToList();
			return View(_service.Get(id));

		}
		[HttpPost]
		public IActionResult Update(DoctorsVm doctorsVm)
		{
			string path = _environment.WebRootPath + @"\Upload\Doctor\";
			string filename = Guid.NewGuid() + doctorsVm.ImgFile.FileName;
			if (!ModelState.IsValid) { return View(); }
			if (_service.Get(doctorsVm.Id) != null)
			{
				FileInfo fileinfo = new FileInfo(path + filename);
				if (fileinfo.Exists)
				{
					fileinfo.Delete();
				}
			}
			using (FileStream stream = new FileStream(path + filename, FileMode.Create))
			{
				doctorsVm.ImgFile.CopyTo(stream);
				doctorsVm.ImgUrl = filename;
			    _service.Update(doctorsVm);
			}
		  
			return RedirectToAction(nameof(AllDoctors));
		}
		public IActionResult Delete(int id)
		{
			_service.Delete(id);
			return RedirectToAction(nameof(AllDoctors));
		}
		public IActionResult Detail(int id)
		{
			return View(_service.Get(id));
		}
	} 
}
