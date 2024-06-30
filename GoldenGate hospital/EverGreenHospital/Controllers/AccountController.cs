using System.Data;

using Evergreen_Application.Abstractions;
using Evergreen_Domain;
using Evergreen_Domain.Helper;
using Evergreen_Domain.OrderandBasket;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EverGreenHospital.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMailService _mailservice;
        AppDbContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IMailService mailservice, AppDbContext context)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._roleManager = roleManager;
            this._mailservice = mailservice;
            _context = context;
        }
        public async Task<IActionResult> CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRole)))
            {
                var roleName = item.ToString();
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = roleName
                    });
                }
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }
            User user = new User()
            {
                Name = registerVm.Name,
                Surname = registerVm.Surname,
                UserName=registerVm.Username,
                Email = registerVm.Email,     
                DoctorId = registerVm.Role
            };
            var result= await _userManager.CreateAsync(user,registerVm.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View(registerVm);
            }
            var role = registerVm.Role?? "Patient";

            if (await _roleManager.RoleExistsAsync(role))
            {
                await _userManager.AddToRoleAsync(user, role);
            }

            await _signInManager.SignInAsync(user, isPersistent: false);


            return RedirectToAction(nameof(Login));


        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> Login(LoginVm loginVm,string? ReturnUrl=null)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            User user;
           if(loginVm.EmailorUsername.Contains("@"))
            {
                user=await _userManager.FindByEmailAsync(loginVm.EmailorUsername);
            }
           else
            {
                user=await _userManager.FindByNameAsync(loginVm.EmailorUsername);
            }
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Username Or Password!");
            }
            var result=await  _signInManager.CheckPasswordSignInAsync(user,loginVm.Password,true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Please Try Again Later!");
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid Username Or Password!");
                return View();
            }
            await _signInManager.SignInAsync(user, loginVm.RemembeMe);
            var admin = await _userManager.FindByNameAsync("Admin");
            if (admin.Id == user.Id)
            {
                return RedirectToAction("Index", "Dashboard", new { Area = "Manage" });

            }
            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }
            else return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordVm forgotPasswordVm)
		{
			if (!ModelState.IsValid)
			{
				return View(forgotPasswordVm);
			}
			var user = await _userManager.FindByEmailAsync(forgotPasswordVm.Email);
			if (user == null)
			{
				return View("Error", "Home");
			}
			string token = await _userManager.GeneratePasswordResetTokenAsync(user);
			string link = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = token }, HttpContext.Request.Scheme)!;
			await _mailservice.SendEmailAsync(new MailRequest()
			{
				Subject = "Reset Password",
				ToEmail = user.Email,
				Body = $"<a href='{link}'>Reset Password</a>"
			});
			Console.WriteLine(link);
			return RedirectToAction("Login", "Account");
		}
		[HttpGet]
        public async Task<IActionResult> ResetPassword(string UserId,string token)
        {
            if(string.IsNullOrWhiteSpace(UserId)|| string.IsNullOrWhiteSpace(token)) { return BadRequest();  }    
            var user= await _userManager.FindByIdAsync(UserId);
            if(user is null) { return View(user); };
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVm resetPasswordVm,string UserId, string token)
        {
            if (string.IsNullOrWhiteSpace(UserId) || string.IsNullOrWhiteSpace(token)) { return BadRequest(); }
            var user = await _userManager.FindByIdAsync(UserId);
            if (user is null) { return View("MyError", "Home"); }
            var identityuser = await _userManager.ResetPasswordAsync(user, token, resetPasswordVm.ComfirmPassword);
            return RedirectToAction(nameof(Login));
        }
       [Authorize]
       public async Task<IActionResult> MyAccount()
        {

            User user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return RedirectToAction(nameof(Index), "Home");
            }
            List<Order> userOrders = await _context.Orders.Where(o => o.UserId == user.Id).Include(o => o.BasketItems).ToListAsync();
            MyAccountVm accountVm = new MyAccountVm()
            {
                Orders = userOrders
            };




            return View(accountVm);
        }


    }
}
