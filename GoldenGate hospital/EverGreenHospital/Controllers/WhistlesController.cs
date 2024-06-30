using Evergreen_Domain;
using Evergreen_Domain.OrderandBasket;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EverGreenHospital.Controllers
{
    public class WhistlesController : Controller
    {
        AppDbContext _context;

        UserManager<User> _userManager;

        public WhistlesController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {
            List<WhistlesItemVm> whislesItem = new List<WhistlesItemVm>();
            if (User.Identity.IsAuthenticated)
            {
                User user = await _userManager.FindByNameAsync(User.Identity.Name);
                List<WhistlesItem> userWhisles = await _context.WhistlesItems
                    .Where(b => b.UserId == user.Id)
                    .Include(b => b.Product)
                    .ToListAsync();
                foreach (var item in userWhisles)
                {
                    whislesItem.Add(new WhistlesItemVm()
                    {
                        Id = item.Id,
                        Price = item.Price,
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
                if (Request.Cookies["Whistles"] != null)
                {
                    basketCookies = JsonConvert.DeserializeObject<List<CookieItemVm>>(Request.Cookies["Whistles"]);
                    foreach (var item in basketCookies)
                    {
                        Evergreen_Domain.Product product = _context.Products.FirstOrDefault(p => p.Id == item.Id);

                        if (product == null)
                        {

                            continue;
                        }

                        whislesItem.Add(new WhistlesItemVm()
                        {
                            Id = item.Id,
                            Name = product.Name,
                            Price = product.Price,
                            ImgUrl = product.ImgUrl,
                            ProductId = product.Id,
                            Product = product,
                        
                        });
                    }

                }
            }

            return View(whislesItem);

        }
        public async Task< IActionResult> AddWhisles(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            if (User.Identity.IsAuthenticated)
            {
                User user = await _userManager.FindByNameAsync(User.Identity.Name);
                WhistlesItem existItem = await _context.WhistlesItems
                    .FirstOrDefaultAsync(p => p.UserId == user.Id && p.ProductId == product.Id );

                WhistlesItem whistlesitem = new WhistlesItem()
                    {
                        User = user,
                        Product = product,                    
                        Price = product.Price,

                    };
                    _context.WhistlesItems.Add(whistlesitem);

                
                await _context.SaveChangesAsync();


            }
            else
            {
                List<CookieItemVm> whistles;
                if (Request.Cookies["Whistles"] == null)
                {
                    CookieItemVm whistlesCookieVm = new CookieItemVm()
                    {
                        Id = id,
                     
                    };
                    whistles = new List<CookieItemVm>();
                    whistles.Add(whistlesCookieVm);
                }
                else
                {
                    whistles = JsonConvert.DeserializeObject<List<CookieItemVm>>(Request.Cookies["Whistles"]);
                    var existBasket = whistles.FirstOrDefault(p => p.Id == id);
                   
                  
                        CookieItemVm whistlesCookieVm = new CookieItemVm()
                        {
                            Id = id,
                        
                        };
                    whistles.Add(whistlesCookieVm);
                   
                }
                var json = JsonConvert.SerializeObject(whistles);
                Response.Cookies.Append("Whistles", json);
            }

            return RedirectToAction(nameof(Index), "Whistles");
            
        }


		public async Task<IActionResult> RemoveWhistles(int productId)
		{
			//User user = await _userManager.FindByNameAsync(User.Identity.Name);

			var myItem = await _context.WhistlesItems.FirstOrDefaultAsync(c => c.ProductId == productId);

			if (myItem != null)
			{
				_context.WhistlesItems.Remove(myItem);
				await _context.SaveChangesAsync();
				return Json(new { success = true, message = "Item removed successfully!" });
			}

			return RedirectToAction(nameof(Index), "Basket");


		}
	}
}
