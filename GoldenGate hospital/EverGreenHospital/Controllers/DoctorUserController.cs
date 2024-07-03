using Evergreen_Domain.ViewModel;
using Evergreen_Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Evergreen_Persistence.DAL;
using Microsoft.EntityFrameworkCore;

namespace EverGreenHospital.Controllers
{
	[Authorize]
	[Authorize(Roles = "Doctor")]

	public class DoctorUserController : Controller
	{
		private readonly UserManager<User> _usermanager;
		private readonly AppDbContext _context;

		public DoctorUserController(UserManager<User> usermanager, AppDbContext context)
		{
			_usermanager = usermanager;
			_context = context;
		}

		public async Task< IActionResult> Dashboard()
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");

			}
			User user = await _usermanager.FindByNameAsync(User.Identity.Name);
			

			return View(user);
		}
		public async Task<IActionResult> Profile(string id)
		{
			Doctor doctor= _context.Doctors.Include(x=>x.Department).FirstOrDefault(x=>x.UserId==id);
			if (doctor == null) { TempData["ErrorMessageProfile"] = "Please add doctor "; return RedirectToAction("Dashboard"); };
			if (doctor==null) return NotFound();
			ViewBag.Realed = _context.Doctors.Where(x => x.DepartmentId ==doctor.DepartmentId&& x.Id!=doctor.Id).ToList();
            return View(doctor);
        }

		public async Task< IActionResult> AppoinmentList(string id)
        {
			Doctor doctor = _context.Doctors.Include(x => x.Department).FirstOrDefault(x => x.UserId == id);
            ViewBag.Appointments = await  _context.Appointments.Include(x => x.Department).ThenInclude(x => x.Doctors).Where(x=>x.DoctorId== doctor.Id).ToListAsync();
			return View(doctor);
		}
		public async Task<IActionResult> DeleteAppointment(int id)
		{
			var oldappointment = await _context.Appointments.Include(x => x.Department).ThenInclude(x => x.Doctors).FirstOrDefaultAsync(x => x.Id == id);
			_context.Appointments.Remove(oldappointment);
			await _context.SaveChangesAsync(true);
			return RedirectToAction(nameof(Dashboard), "DoctorUser");
		}


	}
}
