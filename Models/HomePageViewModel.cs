using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject1.Models
{
    public class HomePageViewModel
    {
        public List<Post> Posts { get; set; }
        public int TotalItemCount { get; set; }
        public int CurrentPage { get; set; }
        public int ItemPerPage { get; set; }
    }
}
