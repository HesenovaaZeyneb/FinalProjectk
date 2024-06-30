using Evergreen_Domain.Helper;
using Evergreen_Domain.ViewModel;
using Evergreen_Domain;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Evergreen_Domain.OrderandBasket;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Stripe;
using static System.Net.WebRequestMethods;


namespace EverGreenHospital.Controllers
{
	[AutoValidateAntiforgeryToken]
	public class BasketController : Controller
	{
		AppDbContext _context;

		UserManager<User> _userManager;

		public BasketController(AppDbContext context, UserManager<User> userManager)
		{
			_context = context;
			_userManager = userManager;

		}

		public async Task<IActionResult> Index()
		{
			List<BasketItemVm> basketItems = new List<BasketItemVm>();
			if (User.Identity.IsAuthenticated)
			{
				User user = await _userManager.FindByNameAsync(User.Identity.Name);
				List<BasketItem> userBasket = await _context.BasketItems
					.Where(b => b.UserId == user.Id && b.OrderId == null)
					.Include(b => b.Product)
					.ToListAsync();
				foreach (var item in userBasket)
				{
					basketItems.Add(new BasketItemVm()
					{
						Id = item.Id,
						Price = item.Price,
						Count = item.Count,
						ProductId = item.ProductId,
						Product = item.Product,
						ImgUrl = item.Product.ImgUrl,
						Name = item.Product.Name
					});
				}

			}
			else
			{
                List<CookieItemVm> basketCookies = new List<CookieItemVm>();
                if (Request.Cookies["Basket"] != null)
                {
                    basketCookies = JsonConvert.DeserializeObject<List<CookieItemVm>>(Request.Cookies["Basket"]);
                    foreach (var item in basketCookies)
                    {
                        Evergreen_Domain.Product product = _context.Products.FirstOrDefault(p => p.Id == item.Id);

                        if (product == null)
                        {

                            continue;
                        }

                        basketItems.Add(new BasketItemVm()
                        {
                            Id = item.Id,
                            Name = product.Name,
                            Price = product.Price,
                            ImgUrl = product.ImgUrl,
                            ProductId = product.Id,
							Product = product,
                            Count = item.Count
                        });
                    }

                }
            }
			
			return View(basketItems);

		}
		//Addbasket
		public async Task<IActionResult> AddBasket(int id)
		{
			var product = _context.Products.FirstOrDefault(p => p.Id == id);
			if (product == null) return NotFound();

			if (User.Identity.IsAuthenticated)
			{
				User user = await _userManager.FindByNameAsync(User.Identity.Name);
				BasketItem existItem = await _context.BasketItems
					.FirstOrDefaultAsync(p => p.UserId == user.Id && p.ProductId == product.Id && p.OrderId == null);
				if (existItem != null)
				{
					existItem.Count++;
				}
				else
				{
					BasketItem basketItem = new BasketItem()
					{
						User = user,
						Product = product,
						Count = 1,
						Price = product.Price,

					};
					_context.BasketItems.Add(basketItem);

				}
				await _context.SaveChangesAsync();


			}
			else
			{
				List<CookieItemVm> basket;
				if (Request.Cookies["Basket"] == null)
				{
					CookieItemVm basketCookieVm = new CookieItemVm()
					{
						Id = id,
						Count = 1
					};
					basket = new List<CookieItemVm>();
					basket.Add(basketCookieVm);
				}
				else
				{
					basket = JsonConvert.DeserializeObject<List<CookieItemVm>>(Request.Cookies["Basket"]);
					var existBasket = basket.FirstOrDefault(p => p.Id == id);
					if (existBasket != null)
					{
						existBasket.Count += 1;
					}
					else
					{
						CookieItemVm basketCookieVm = new CookieItemVm()
						{
							Id = id,
							Count = 1
						};
						basket.Add(basketCookieVm);
					}
				}
				var json = JsonConvert.SerializeObject(basket);
				Response.Cookies.Append("Basket", json);
			}

			return RedirectToAction(nameof(Index), "Basket");


		}
	
		public async Task<IActionResult> RemoveBasket(int productId)
		{
			//User user = await _userManager.FindByNameAsync(User.Identity.Name);

			var myItem = await _context.BasketItems.FirstOrDefaultAsync(c => c.ProductId == productId);
			
			if (myItem != null)
			{
				_context.BasketItems.Remove(myItem);
				await _context.SaveChangesAsync();
				return Json(new { success = true, message = "Item removed successfully!" });
			}

			return RedirectToAction(nameof(Index), "Basket");


		}
		public IActionResult GetBasket()
		{
			var basketcookie = Request.Cookies["Basket"];
			return Json(basketcookie);
		}
		[Authorize]
		public async Task<IActionResult> Checkout()
		{
			User user = await _userManager.FindByNameAsync(User.Identity.Name);
			
			ViewBag.BasketItems= await _context.BasketItems
            .Where(b => b.UserId == user.Id && b.OrderId == null).Include(b => b.Product).ToListAsync();
            return View();
		}
		[HttpPost]
		[Authorize]
		public async Task<IActionResult> Checkout(OrderVm orderVm, string StripeEmail, string StripeToken)
		{
			User user = await _userManager.FindByNameAsync(User.Identity.Name);
			List<BasketItem> basketItems= await _context.BasketItems
            .Where(b => b.UserId == user.Id && b.OrderId == null).Include(b => b.Product).ToListAsync();
            if (!ModelState.IsValid)
			{
                
                ViewBag.BasketItems= basketItems;
                ModelState.AddModelError("Adress", "please fill in the address");
				return View();

			}
            //var basketitem = await _context.BasketItems.Where(b => b.UserId == user.Id && b.OrderId == null).Include(b => b.Product).ToListAsync();
            ViewBag.BasketItems = await _context.BasketItems
            .Where(b => b.UserId == user.Id && b.OrderId == null).Include(b => b.Product).ToListAsync();
            double TotalPrice = 0;
			for (int i = 0; i < basketItems.Count; i++)
			{
				TotalPrice += basketItems[i].Price * basketItems[i].Count;
			}
			Order order = new Order()
			{
				
				BasketItems = basketItems,
				Status = null,
				Address = orderVm.Adress,
				User = user,
				CreateDate = DateTime.Now,
				TotalPrice = TotalPrice,
				Code = CreateRandomCode.GenerateCode(5)
			};
			//Stripe payment
			var optionCust = new CustomerCreateOptions
			{
				Email = StripeEmail,
				Name = user.Name + " " + user.Surname,
				Phone = "+994 50 66"
			};
			var serviceCust = new CustomerService();
			Customer customer = serviceCust.Create(optionCust);

			TotalPrice = TotalPrice * 100;
			var optionsCharge = new ChargeCreateOptions
			{

				Amount = (long)TotalPrice,
				Currency = "USD",
				Description = "Product Selling amount",
				Source = StripeToken,
				ReceiptEmail = StripeEmail


			};
			var serviceCharge = new ChargeService();
			Charge charge = serviceCharge.Create(optionsCharge);
			if (charge.Status != "succeeded")
			{
				ViewBag.BasketItems = basketItems;
				ModelState.AddModelError("Address", "there is problem in payment");
				return View();
			}

			await _context.Orders.AddAsync(order);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index), "Home");
		}
	}
}
