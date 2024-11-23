using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Services;

namespace WebApplication1.Pages.Admin.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IWebHostEnvironment enviroment;
        private readonly ApplicationDbContext context;
        public DeleteModel(IWebHostEnvironment environment, ApplicationDbContext context) { 
            this.enviroment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            if (id == null) {
                Response.Redirect("/Index");
                return;
            }

            var product = context.Products.Find(id);
            if (product == null)
            {
                Response.Redirect("/Index");
                return;
            }

            string imageFullPath = enviroment.WebRootPath + "/products/" + product.ImageFileName;
            System.IO.File.Delete(imageFullPath);

            context.Products.Remove(product);
            context.SaveChanges();

            Response.Redirect("/Index");
        }
    }
}
