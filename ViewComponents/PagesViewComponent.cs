using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebProject1.Models;

namespace WebProject1.ViewComponents
{
    public class PagesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int TotalItemCount, int CurrentPage, int ItemPerPage, int Track)
        {
            var aList = new List<int>();
            aList.Add(TotalItemCount);
            aList.Add(CurrentPage);
            aList.Add(ItemPerPage);
            aList.Add(Track);
            return View(aList);
        }
    }

}
