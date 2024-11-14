
using Amazon.Models;
using Amazon.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Amazon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContex context;

        public HomeController(ApplicationDBContex context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {         
            var totalCount = await context.Products.CountAsync();

            var products = await context.Products
                .Skip((page - 1) * pageSize)  
                .Take(pageSize)             
                .ToListAsync();
            
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var viewModel = new ProductListViewModel
            {
                Products = products,
                CurrentPage = page,
                TotalPages = totalPages,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(viewModel);
        }

        //public IActionResult Index()
        //{
        //    var products = context.Products.ToList();
        //    return View(products);
        //}
        // GET: /Products/Create
        public IActionResult Create()
        {
            var categories = context.Categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName,
            }).ToList();

            ViewBag.Categories = new SelectList(categories, "Value", "Text");

            return View(new ProductDto());

        }

        // POST: /Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductDto productDto)
        {
            
         
            var category = context.Categories.FirstOrDefault(c => c.CategoryId == productDto.CategoryId);
            if (category == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid Category selected.");
                ViewBag.Categories = new SelectList(context.Categories, "CategoryId", "CategoryName");
                return View(productDto);
            }

           
            productDto.CategoryName = category.CategoryName;

            // Create the new product
            var product = new Product()
            {
                ProductName = productDto.ProductName,
                CategoryId = productDto.CategoryId,
                
                CategoryName = productDto.CategoryName  
            };
      
            context.Products.Add(product);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);

            if (product == null)
            {
                return RedirectToAction("Index", "Products");
            }

            // Populate the category dropdown
            var categories = context.Categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName,
            }).ToList();

            ViewBag.Categories = new SelectList(categories, "Value", "Text");
            ViewBag.ProductId = product.ProductId;
            ViewBag.SelectedCategoryId = product.CategoryId; // Pass the selected category to the view

            // Send existing product data to the view
            var productDto = new ProductDto
            {
                ProducId= product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
            };

            return View(productDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductDto productDto)
        {
            

            var existingProduct = context.Products.FirstOrDefault(p => p.ProductId == productDto.ProducId);

            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update the product
            existingProduct.ProductName = productDto.ProductName;
            existingProduct.CategoryId = productDto.CategoryId;

            // Save the changes
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }




        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index)); // Redirect to Product List after delete
        }
        private bool ProductExists(int id)
        {
            return context.Products.Any(e => e.ProductId == id);
        }

    }
}



