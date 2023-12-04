﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Domain.Dtos.CRUD
{
    public class ProductCreationDto
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
        public string? ProductCode { get; set; }
        public string? ProductReference { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}