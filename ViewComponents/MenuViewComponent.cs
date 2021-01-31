using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebProject1.Models;

namespace WebProject1.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new MenuModal();
           
            bool isAuthenticated = User.Identity.IsAuthenticated;
            

            if (isAuthenticated == true)
            {
                model.IsLoggedIn = true;
                var user = User as ClaimsPrincipal;
                var name = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var surname = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname).Value;
                model.NameSurname = $"{name} {surname}"; 
            }
            else
            {
                model.IsLoggedIn = false;
            }
            return View(model);
        } 
       
    }
}
