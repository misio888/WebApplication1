using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext context;

        public IndexModel(ILogger<IndexModel> logger,ApplicationDbContext context)
        {
            _logger = logger;
            this.context = context;
        }
        public List<Product> Products { get; set; } = new List<Product>();

        public void OnGet()
        {
            Products = context.Products.ToList();
        }
    }
}
