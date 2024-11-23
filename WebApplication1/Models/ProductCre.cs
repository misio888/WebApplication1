using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ProductCre
    { 
        [Required, MaxLength(100)]
        public string? Name { get; set; }
        [Required, MaxLength(50)]
        public string? Category { get; set; }
        [Required, Precision(16, 2)]
        public decimal Price { get; set; }
        [Required,MaxLength(500)]
        public string? Description { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}

