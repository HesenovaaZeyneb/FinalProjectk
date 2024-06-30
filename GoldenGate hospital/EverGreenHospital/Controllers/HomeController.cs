using System.Diagnostics;
using Evergreen_Application.Abstractions;
using Evergreen_Domain;
using Evergreen_Domain.OrderandBasket;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverGreenHospital.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;
        IDoctorsService _service;
        IBlogService _blogService;
        UserManager<User> _userManager;
		public HomeController(IDoctorsService service, AppDbContext context, IBlogService blogService, UserManager<User> userManager)
		{
			_service = service;
			_context = context;
			_blogService = blogService;
			_userManager = userManager;
		}

		public IActionResult Index()
        {
            ViewBag.Departments = _context.Departments.ToList();
            ViewBag.Doctors = _service.GetAll();
            ViewBag.Blogs= _blogService.Getall();
            ViewBag.Contact=_context.Contacts.ToList();
            return View();
        }
        public IActionResult AboutUs()
        {
            ViewBag.Departments= _context.Departments.ToList();
            ViewBag.Doctors = _service.GetAll();
            return View();
        }
        [HttpGet]
        public IActionResult MyError()
        {
            return View();
        }
       
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Faq()
        {
            return View();
        }

    }
}
