using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebProject1.Models;

namespace WebProject1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        Context c1 = new Context();

        public IActionResult HomePage(int page)
        {
            if (page < 1) { page = 1; }

            var tumpaylasimlar = c1.Posts.Include(x => x.User).Include(x => x.City).Skip(9 * (page - 1)).Take(9).ToList();
            tumpaylasimlar.Sort((x, y) => -x.Date.CompareTo(y.Date));

                
            var model = new HomePageViewModel
            {
                Posts = tumpaylasimlar,
                CurrentPage = page,
                TotalItemCount = c1.Posts.Count(),
                ItemPerPage = 9
            };
            return View(model);
        }

        public IActionResult HomePage2(int page)
        {
            if (page < 1) { page = 1; }

            var tumpaylasimlar = c1.Posts.Include(x => x.User).Include(x => x.City).Skip(9 * (page - 1)).Take(9).ToList();
            tumpaylasimlar.Sort((x, y) => -x.Date.CompareTo(y.Date));

            var model = new HomePageViewModel
            {
                Posts = tumpaylasimlar,
                CurrentPage = page,
                TotalItemCount = c1.Posts.Count(),
                ItemPerPage = 9
            };
            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
