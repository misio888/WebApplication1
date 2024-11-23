using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.Admin.Products
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ApplicationDbContext context;

        [BindProperty]
        public ProductCre ProductCre { get; set; } = new ProductCre();

        public Product Product { get; set; } = new Product();

        public string errorMessage = "";
        public string successMessage = "";
        public EditModel(IWebHostEnvironment environment, ApplicationDbContext context) { 
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Index");
                return;
            }

            var product = context.Products.Find(id);
            if (product == null)
            {
                Response.Redirect("/Index");
                return;
            }

            ProductCre.Name = product.Name;
            ProductCre.Category = product.Category;
            ProductCre.Price = product.Price;
            ProductCre.Description = product.Description;

            Product = product;
        }

        public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Index");
                return;
            }

            if (!ModelState.IsValid) {
                errorMessage = "Dokoñcz wprowadzaæ dane :<";
                return;
            }
            var product = context.Products.Find(id);
            if (product == null)
            {
                Response.Redirect("/Index");
                return;
            }

            string? newFileName = product.ImageFileName;
            if (ProductCre.ImageFile != null) {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(ProductCre.ImageFile.FileName);

                string imageFullPath = environment.WebRootPath + "/products/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath)) {
                    ProductCre.ImageFile.CopyTo(stream);
                }

                string oldImageFullPath = environment.WebRootPath + "/products/" + product.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }

            product.Name = ProductCre.Name;
            product.Category = ProductCre.Category;
            product.Price = ProductCre.Price;
            product.Description = ProductCre.Description ?? "";
            product.ImageFileName = newFileName;

            context.SaveChanges();

            Product = product;

            successMessage = "Zaktualizowano dane !";
        }
    }
}
