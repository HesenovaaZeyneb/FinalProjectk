using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Azure;
using Evergreen_Domain;
using Evergreen_Domain.OrderandBasket;
using Evergreen_Domain.ViewModel;
using Evergreen_Persistence.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Org.BouncyCastle.Bcpg;

namespace Evergreen_Persistence.Concretes.Setting
{
    public class LayoutServicee 
    {
        AppDbContext _context;
        IHttpContextAccessor _http;
        UserManager<User> _userManager;

        public LayoutServicee(AppDbContext context, IHttpContextAccessor http, UserManager<User> userManager)
        {
            _context = context;
            _http = http;
            _userManager = userManager;
        }

        public async Task<Dictionary<string, string>> GetSettings()
        {
            Dictionary<string, string> settings = _context.Settings.ToDictionary(s=>s.Key,s=>s.Value);
            return settings;
        }
        public async Task<List<BasketItemVm>> GetBasketItems()
        {
            List<BasketItemVm> basketItemVm = new List<BasketItemVm>();
            if (_http.HttpContext.User.Identity.IsAuthenticated)
            {
                User user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);
                if (user is null) { throw new Exception(" user not found "); };
                List<BasketItem> userBasket = await _context.BasketItems
                    .Where(b => b.UserId == user.Id && b.OrderId == null)
                    .Include(b => b.Product)                
                    .ToListAsync();
                foreach (var item in userBasket)
                {
                    basketItemVm.Add(new BasketItemVm()
                    {
                        Id = item.Id,
                        Price = item.Price,
                        Count = item.Count,
                        Product = item.Product,
                        ProductId = item.ProductId,
                        ImgUrl = item.Product.ImgUrl,
                        Name = item.Product.Name
                    });
                }
            }
            else
            {
                var jsonBasket = _http.HttpContext.Request.Cookies["Basket"];
                if (jsonBasket != null)
                {
                    List<CookieItemVm> basketCookie = JsonConvert.DeserializeObject<List<CookieItemVm>>(jsonBasket);

                    foreach (var item in basketCookie)
                    {
                        Product product = _context.Products.FirstOrDefault(p => p.Id == item.Id);

                        if (product == null)
                        {

                            continue;
                        }

                        basketItemVm.Add(new BasketItemVm()
                        {
                            Id = product.Id,
                            Name = product.Name,
                            Price = product.Price,
                            ImgUrl = product.ImgUrl,
                            Product = product,
                            ProductId = product.Id,
                            Count = item.Count
                        });
                    }
                }
            }
            return basketItemVm;
        }


		public async Task<List<WhistlesItemVm>> GetWhistlesItems()
		{
			List<WhistlesItemVm> whistles = new List<WhistlesItemVm>();
			if (_http.HttpContext.User.Identity.IsAuthenticated)
			{
				User user = await _userManager.FindByNameAsync(_http.HttpContext.User.Identity.Name);
				if (user is null) { throw new Exception(" user not found "); };
				List<WhistlesItem> userwhistles = await _context.WhistlesItems
					.Where(b => b.UserId == user.Id)
					.Include(b => b.Product)
					.ToListAsync();
				foreach (var item in userwhistles)
				{
					whistles.Add(new WhistlesItemVm()
					{
						Id = item.Id,
						Price = item.Price,
						Product = item.Product,
						ProductId = item.ProductId,
						ImgUrl = item.Product.ImgUrl,
						Name = item.Product.Name
					});
				}
			}
			else
			{
				var jsonBasket = _http.HttpContext.Request.Cookies["Whistles"];
				if (jsonBasket != null)
				{
					List<CookieItemVm> whistlesCookie = JsonConvert.DeserializeObject<List<CookieItemVm>>(jsonBasket);

					foreach (var item in whistlesCookie)
					{
						Product product = _context.Products.FirstOrDefault(p => p.Id == item.Id);

						if (product == null)
						{

							continue;
						}

						whistles.Add(new WhistlesItemVm()
						{
							Id = product.Id,
							Name = product.Name,
							Price = product.Price,
							ImgUrl = product.ImgUrl,
							Product = product,
							ProductId = product.Id,
						});
					}
				}
			}
			return whistles;
		}


	}
}
