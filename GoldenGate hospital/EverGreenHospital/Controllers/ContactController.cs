using Evergreen_Domain;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EverGreenHospital.Controllers
{
	public class ContactController : Controller
	{
		private readonly AppDbContext _dbContext;

		public ContactController(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IActionResult Index()
		{
			if (!User.Identity.IsAuthenticated)
			{
				return RedirectToAction("Login", "Account");

			}
			return View();
		}
		[Authorize]
		[HttpPost]	
		public async Task< IActionResult> Contact(ContactVm contactVm)
		{

			if (contactVm == null) { return NotFound(); }
			Contact contact = new Contact()
			{
			
				UserName = contactVm.UserName,
				Email = contactVm.Email,
				Subject = contactVm.Subject,
				Comment = contactVm.Comment,

			};
			await _dbContext.Contacts.AddAsync(contact);
			await _dbContext.SaveChangesAsync();

			return RedirectToAction(nameof(Index),"Contact");
		}
	}
}
