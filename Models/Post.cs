using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject1.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public virtual User User { get; set; }
        public virtual City City { get; set; }

        public int UserId { get; set; }
        public int CityId { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }


        [DataType(DataType.Date)]
        public DateTime Date { get; set; }


        public bool IsDeleted { get; set; }

      
    }
}
