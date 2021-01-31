using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebProject1.Migrations;
using WebProject1.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Cryptography.X509Certificates;

namespace WebProject1.Controllers
{
    public class UserController : Controller
    {

        Context c = new Context();

        [HttpGet]
        public IActionResult LoginView()
        {
            var model = new LoginViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginView(LoginViewModel u)
        {
            var bilgiler = c.Users.FirstOrDefault(x => x.Email == u.Email && x.Password == u.Password);

            if (bilgiler != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, bilgiler.Email),
                    new Claim(ClaimTypes.NameIdentifier, bilgiler.Id.ToString()),
                    new Claim(ClaimTypes.Name, bilgiler.Name),
                    new Claim(ClaimTypes.Surname, bilgiler.Surname),
                };

                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("HomePage", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("HomePage", "Home");
        }

        public IActionResult UserProfil(int id, int page)
        {
            
            var idx = id;
            if (id < 0)
            {
                //Current User bilgileir lazım
               var claim_id = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
               idx = Int32.Parse(claim_id);
            }
            
            var degerler = c.Posts.Include(x => x.User).Include(x => x.City).ToList();
            var degerler2 = new List<Post>();

            foreach (var i in degerler)
            {
                if (i.UserId == idx)
                {
                    degerler2.Add(i);
                }
            }
            degerler2.Sort((x, y) => -x.Date.CompareTo(y.Date));

            if (page < 1)
            {
                page = 1;
            }

            var degerler3 = degerler2.Skip(9 * (page - 1)).Take(9).ToList();
           
            var model = new HomePageViewModel
            {
                Posts = degerler3,
                CurrentPage = page,
                TotalItemCount = degerler2.Count(),
                ItemPerPage = 9
            };
            

            bool isAuthenticated = User.Identity.IsAuthenticated;
            var tuple_p = new Tuple<HomePageViewModel, bool, int>(model, isAuthenticated, idx);
            return View(tuple_p);
        }


        public IActionResult AboutUs()
        {
            var yazarlar = c.Users.ToList();
            return View(yazarlar);
        }

        [HttpGet]
        public IActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewUser(User u)
        {
            c.Users.Add(u);
            c.SaveChanges();
            return RedirectToAction("HomePage", "Home");
        }


        public IActionResult DeleteUser(int id)
        {
            var kullanici = c.Users.Find(id);
            c.Users.Remove(kullanici);
            c.SaveChanges();
            return RedirectToAction("LogOut", "User");
        }

    }
}
