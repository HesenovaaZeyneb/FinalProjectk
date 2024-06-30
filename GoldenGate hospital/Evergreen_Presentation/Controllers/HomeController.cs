using System.Diagnostics;
using Evergreen_Presentation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Evergreen_Presentation.Controllers
{
	public class HomeController : Controller
	{
		
		public IActionResult Index()
		{
			return View();
		}

		
	}
}
