using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using WebProject1.Models;

namespace WebProject1.Controllers
{
    public class PostController : Controller
    {
        Context c2 = new Context();

        public IActionResult DisplayPost(int id)
        {
            var u = c2.Users.Find(c2.Posts.Find(id).UserId);
            var tmp = c2.Posts.Find(id);

            var tumu = c2.Posts.ToList();
            List<Post> tumu2 = new List<Post>();
            foreach(var i in tumu)
            {
                if (i.UserId == tmp.UserId && i.Id != tmp.Id)
                {
                    tumu2.Add(i);
                }
                
                if (tumu2.Count == 3)
                {
                    break;
                }
            }

            var tmp_tuple = new Tuple<Post, User, List<Post>>(tmp, u, tumu2);
            
            //ViewBag.msg = str;
            return View(tmp_tuple);

            //var tumpaylasımlar = c2.Posts.Include(x => x.User).Include(x => x.City).ToList();
        }


        public IActionResult DeletePost(int idx)
        {

            var id = idx;
            var paylasim = c2.Posts.Find(id);
            id = paylasim.UserId;   
            c2.Posts.Remove(paylasim);
            c2.SaveChanges();
            return RedirectToAction("UserProfil", new RouteValueDictionary(
                        new { controller = "User", action = "UserProfil", Id = id }));
        }
            
       [HttpGet]
        public IActionResult NewPost()
        {

            return View();
        }


        [HttpPost]
        public IActionResult NewPost(FormData formData)
        {
            int tmpID = -1;
            foreach(var x in c2.Cities.ToList())
            {
                if (x.Name.Equals(formData.Il))
                {
                    tmpID = x.Id;
                    break;
                }
            }
            int id = Int32.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var newpost = new Post
            {
                UserId = id,
                CityId = tmpID,
                Title = formData.Baslik,
                Content = formData.Content.Substring(3, formData.Content.Length-7),
                Date = DateTime.UtcNow,
                IsDeleted = true,
            };
            c2.Posts.Add(newpost);
            c2.SaveChanges();

            return RedirectToAction("UserProfil", new RouteValueDictionary(
                        new { controller = "User", action = "UserProfil", Id = id }));
        }

    }
}
