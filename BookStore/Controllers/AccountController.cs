using BookStore.Data;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private IBookService bookService;
        private IUserService userService;
        BookStoreDbContext dbContext;
        private List<BookInCart> books = new List<BookInCart>();

        public IActionResult Index()
        {
            return View();
        }

        public AccountController(BookStoreDbContext dbContext, IUserService userService, IBookService bookService)
        {
            this.dbContext = dbContext;
            this.userService = userService;
            this.bookService = bookService;

        }

        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl= returnUrl ;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel, string returnUrl)
        {
            var user = userService.ValidUser(userLoginModel.UserName, userLoginModel.Password);
            if (user != null)
            {
                List<Claim> claims = new List<Claim>();

                
                var role = dbContext.Roles.FirstOrDefault(x => x.Id == user.RoleId);
                
                claims.Add(new Claim(ClaimTypes.Name, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Role,role.Name));
                
                

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }




                return Redirect("/");
                

            }

            ModelState.AddModelError("hata", "Kullanıcı adı veya şifre hatalı");
            return View();
        }

        public async Task<IActionResult> LogOut()
        {

            Cart cart = GetCart();
  
            if(cart != null) {
                /*foreach (var item in cart.Books)
                {
                    cart.RemoveAllBook(item.Book);
                   
                    if (cart.Books.Count() == 0)
                    {
                        break;
                    }
                }*/
                cart = null;
                SaveCart(cart);

            }
            await HttpContext.SignOutAsync();
            return Redirect("/");


        }

        Cart GetCart()
        {
            var data = HttpContext.Session.GetString("Sepetim");
            if (data == null)
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<Cart>(data);
            }
        }
        void SaveCart(Cart cart)
        {

            HttpContext.Session.SetString("Sepetim", JsonConvert.SerializeObject(cart));
        }
    }
}