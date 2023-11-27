﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public virtual List<Product> Products { get; set; }

    }
}