using Evergreen_Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EverGreenHospital.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentService _service;
        public DepartmentController(IDepartmentService service)
        {
            _service = service;
        }
        public IActionResult Departments()
        {
            return View(_service.GetAll());
        }
    }
}
