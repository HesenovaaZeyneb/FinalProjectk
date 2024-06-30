using Evergreen_Application.Abstractions;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EverGreenHospital.Controllers
{
    public class AllDoctorsController : Controller
    {
        IDoctorsService _service;

        public AllDoctorsController(IDoctorsService service)
        {
            _service = service;
        }

        public IActionResult Doctors()
        {
            return View(_service.GetAll());
        }

    }
}
