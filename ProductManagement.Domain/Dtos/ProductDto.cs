﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Domain.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? ProductCode { get; set; }
        public string? ProductReference { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public int IdCategory { get; set; }
        public string CategoryDescription { get; set; } = string.Empty;
    }
}