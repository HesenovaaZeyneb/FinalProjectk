using Evergreen_Application.Abstractions;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EverGreenHospital.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class FaqController : Controller
    {
        private readonly IQuestionService _service;
        private readonly AppDbContext _context;
		public FaqController(IQuestionService service, AppDbContext context)
		{
			_service = service;
			_context = context;
		}

		public IActionResult AllQuestion()
        {
            return View(_context.Questions.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(QuestionVm questionVm)
        {
            if (questionVm == null)
            {
                return NotFound();
            }
            _service.Create(questionVm);
            return RedirectToAction(nameof(AllQuestion));

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            return View(_service.Get(id));
        }
        [HttpPost]  
        public IActionResult Update(QuestionVm questionVm)
        {
            _service.Update(questionVm);

            return RedirectToAction(nameof(AllQuestion));
        }
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(AllQuestion));
        }
    }
}
