﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Dtos.CRUD
{
    public class CategoryWriteDto
    {
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
}