using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EverGreenHospital.Areas.Manage.Controllers
{
	public class DashboardController : Controller
	{
		[Area("Manage")]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
		{
			return View();
		}
	}
}
