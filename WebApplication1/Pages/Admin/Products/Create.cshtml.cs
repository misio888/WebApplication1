using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.Admin.Products
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment? environment;
        private readonly ApplicationDbContext? context;
        [BindProperty]
        public ProductCre ProductCre { get; set; } = new ProductCre();

        public CreateModel(IWebHostEnvironment environment, ApplicationDbContext context) { 
            this.environment = environment;
            this.context = context; 
        }
        public void OnGet()
        {

        }

        public string errorMessage = "";
        public string successMessage = "";

        public void OnPost() {

            if (ProductCre.ImageFile == null) { 
                ModelState.AddModelError("ProductCre.ImageFile", "Daj zdjêcie jakiegoœ fajnego mercedesa :D");
            }

            if (!ModelState.IsValid) {
                errorMessage = "Dokoñcz formularz :<";
                return;
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(ProductCre.ImageFile!.FileName);
           
            string imageFullPath = environment?.WebRootPath + "/products/" + newFileName;
            using (var steam = System.IO.File.Create(imageFullPath)) {
                ProductCre.ImageFile.CopyTo(steam);
            }

            Product product = new Product()
            {
                Name = ProductCre.Name,
                Category = ProductCre.Category,
                Price = ProductCre.Price,
                Description = ProductCre.Description,
                ImageFileName = newFileName,
                CratedAt = DateTime.Now,
            };

            context?.Products.Add(product);
            context?.SaveChanges();

            ProductCre.Name = "";
            ProductCre.Description = "";
            ProductCre.Category = "";
            ProductCre.Price = 0;
            ProductCre.ImageFile = null;

            ModelState.Clear();

            successMessage = "Produkt dodany !";

            Response.Redirect("/Admin/Products/Index");
        }

    }
}
