using System.Security.Claims;
using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverGreenHospital.Controllers
{
    [Authorize]
    [Authorize(Roles = "Patient")]
    public class PatientUserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public PatientUserController(UserManager<User> userManager, AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");

            }
            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserVm uservm = new UserVm()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Adress = user.Adress,
                BirthDay = (DateTime)user.BirthDay

            };
            ViewBag.Appointment = await _context.Appointments.Include(x=>x.Department).ThenInclude(x=>x.Doctors).ToListAsync();
            return View(uservm);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileUpdate(UserVm uservm)
        {

            User user = await _userManager.FindByNameAsync(User.Identity.Name);

            user.Name = uservm.Name;
            user.Surname = uservm.Surname;
            user.PhoneNumber = uservm.PhoneNumber;
            user.Email = uservm.Email;
            user.Adress = uservm.Adress;
            user.BirthDay = uservm.BirthDay;




            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            //User user = new User()
            //{
            //    //Id=userId,
            //    Name = uservm.Name,
            //    Surname = uservm.Surname,
            //    PhoneNumber = uservm.PhoneNumber,
            //    Email = uservm.Email,

            //};

            await _context.SaveChangesAsync();
            return RedirectToAction("Profile");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> OnlineAppointment()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");

            }
            //User user = await _userManager.FindByNameAsync(User.Identity.Name);
            //UserVm uservm = new UserVm()
            //{
            //    Id = user.Id,
            //    Name = user.Name,
            //    Surname = user.Surname,
            //    PhoneNumber = user.PhoneNumber,
            //    Email = user.Email,
            //    Adress= user.Adress,

            //};
            ViewBag.Departments = await _context.Departments.ToListAsync();
            ViewBag.Doctors = await _context.Doctors.ToListAsync();
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> OnlineAppointment(AppointmentVm appointmentVm)
        {
            Appointment appointment = new Appointment()
            {

                Comment = appointmentVm.Comment,
                DepartmentId = appointmentVm.DepartmentId,
                DoctorId = appointmentVm.DoctorId,
                Phone = appointmentVm.Phone,
                Email = appointmentVm.Email,
                VisitDate = appointmentVm.VisitDate,
            };
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Profile), "PatientUser");
        }

    }
}
