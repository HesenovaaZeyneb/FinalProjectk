using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverGreenHospital.Areas.Manage.Controllers
{
	[Area("Manage")]
	[Authorize(Roles = "Admin")]
	public class AppoinmentController : Controller
	{
		private readonly AppDbContext _context;

		public AppoinmentController(AppDbContext context)
		{
			_context = context;
		}

		public  IActionResult Index()
		{
			ViewBag.Appointment =  _context.Appointments.Include(x => x.Department).ThenInclude(x => x.Doctors).ToList();
			return View();
		}
	}
}
