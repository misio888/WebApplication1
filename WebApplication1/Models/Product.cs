using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; } 
        [Required,MaxLength(100)]
        public string? Name { get; set; }
        [Required,MaxLength(50)]
        public string? Category { get; set; }
        [Required,Precision(16,2)]
        public decimal Price { get; set; }
        [Required,MaxLength(500)]
        public string? Description { get; set; }
        [Required,MaxLength(100)]
        public string? ImageFileName { get; set; }
        [Required]
        public DateTime CratedAt { get; set; }
    }
}
