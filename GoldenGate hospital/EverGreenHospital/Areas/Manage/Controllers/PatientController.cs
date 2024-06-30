using Evergreen_Application.Abstractions;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EverGreenHospital.Areas.Manage.Controllers
{
	[Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class PatientController : Controller
	{
		IWebHostEnvironment _environment;
		IPatientService _service;
		AppDbContext _context;

		public PatientController(IWebHostEnvironment environment, IPatientService service, AppDbContext context)
		{
			_environment = environment;
			_service = service;
			_context = context;
		}

		public IActionResult AllPatient()
		{
			return View(_service.GetAll());
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(PatientVm patientVm)
		{

			string path = _environment.WebRootPath + @"\Upload\Patient\";
			string filename=Guid.NewGuid()+patientVm.ImgFile.FileName;
			using(FileStream stream=new FileStream(path+filename,FileMode.Create))
			{
				patientVm.ImgFile.CopyTo(stream);
			}
			patientVm.ImgUrl = filename;
			_service.Create(patientVm);
			return RedirectToAction(nameof(AllPatient));
		}
		[HttpGet]
		public IActionResult Update(int id)
		{
			return View(_service.Get(id));
		}
		[HttpPost]
		public IActionResult Update(PatientVm patientVm)
		{
			string path = _environment.WebRootPath + @"\Upload\Patient\";
			string filename = Guid.NewGuid() + patientVm.ImgFile.FileName;
			if (_service.Get(patientVm.Id) != null)
			{
             FileInfo fileInfo = new FileInfo(path+filename);
				if(fileInfo.Exists)
				{
					fileInfo.Delete();
				}
			}
			
			using (FileStream stream = new FileStream(path + filename, FileMode.Create))
			{
				patientVm.ImgFile.CopyTo(stream);
				patientVm.ImgUrl= filename;
				_service.Update(patientVm);
				return Ok();
			}
			
			return RedirectToAction(nameof(AllPatient));
		}
		public IActionResult Detail(int id)
		{
			return View(_service.Get(id));
		}
		public IActionResult Delete(int id)
		{
			_service.Delete(id);
			return RedirectToAction(nameof(AllPatient));
		}
	}
}
