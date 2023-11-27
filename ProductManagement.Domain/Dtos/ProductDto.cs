using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Domain.Dtos
{
    public class ProductDto
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
        public string CategoryDescription { get; set; } = string.Empty;
    }
}