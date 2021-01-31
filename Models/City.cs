﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebProject1.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        public int Plate { get; set; }
        public string Name { get; set; }
        

    }
}