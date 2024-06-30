using Evergreen_Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace EverGreenHospital.Controllers
{
	public class QuestionController : Controller
	{
		private readonly IQuestionService _service;

		public QuestionController(IQuestionService service)
		{
			_service = service;
		}

		public IActionResult Index()
		{
			return View(_service.GetAll());
		}
	}
}
