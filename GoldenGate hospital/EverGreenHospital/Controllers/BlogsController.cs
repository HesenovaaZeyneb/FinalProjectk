using Evergreen_Application.Abstractions;
using Evergreen_Domain;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Mvc;

namespace EverGreenHospital.Controllers
{
	public class BlogsController : Controller
	{ IBlogService _service;
		private readonly AppDbContext _context;

		public BlogsController(IBlogService service,AppDbContext context)
		{
			_service = service;
			_context = context;
		}
		public IActionResult Blogs()
		{
			return View(_service.Getall());
		}
		public IActionResult Details(int id)
		{
			var blog=_service.Get( id);
			ViewBag.Realedd=_context.Blogs.Where(x => x.Date == blog.Date && x.Id != blog.Id).ToList();
			return View(blog);
		}
	}
}
