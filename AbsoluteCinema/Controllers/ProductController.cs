using AbsoluteCinema.Models;
using BusinessLogicLayer;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace AbsoluteCinema.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService productService;
        private readonly ICartService cartService;
        private readonly ICategoryService categoryService;
        private readonly AbsoluteCinemaDbContext _context;
        private readonly ITmdbService tmdbService;

        public ProductController(IProductService productService, ICartService cartService, ICategoryService categoryService, AbsoluteCinemaDbContext _context, ITmdbService tmdbService)
        {
            this.cartService = cartService;
            this.productService = productService;
            this.categoryService = categoryService;
            this._context = _context;
            this.tmdbService = tmdbService;
        }
        public async Task<IActionResult> Index(string category)
        {
            var products = await productService.GetProductModelsAsync();

            if (!string.IsNullOrEmpty(category) && category != "All") 
            {
                products = products.Where(p => p.Category.CategoryName == category);
            }
            ViewBag.CurrentCategory = category ?? "All";

            return View(products);
        }
        public async Task<IActionResult> Products()
        {
            var products = await productService.GetAllAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            var viewModel = new ProductViewModel
            {
                Categories = await _context.Categories.ToListAsync()
            };

            return View(viewModel);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _context.Categories.ToListAsync();
                return View(model);
            }

            var product = new ProductModel
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                QuantityAvailable = model.QuantityAvailable,
                ImagePath = model.ImagePath,
                CategoryId = model.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Products");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
      .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            var viewModel = new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                QuantityAvailable = product.QuantityAvailable,
                ImagePath = product.ImagePath,
                CategoryId = product.CategoryId,
                Categories = await _context.Categories.ToListAsync()
            };

            return View(viewModel);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _context.Categories.ToListAsync();
                return View(model);
            }

            var product = await _context.Products.FindAsync(model.ProductId);

            if (product == null)
                return NotFound();

            // Map ViewModel → Entity
            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.QuantityAvailable = model.QuantityAvailable;
            product.ImagePath = model.ImagePath;
            product.CategoryId = model.CategoryId;

            await _context.SaveChangesAsync();

            return RedirectToAction("Products");
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteAsync(id);

            return RedirectToAction("Products");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return RedirectToAction("Login", "Account");

            int userId = int.Parse(userIdClaim.Value);

            await cartService.AddToCartAsync(userId, productId);
           

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var relatedProducts = await productService.GetRelatedProductsAsync(product.CategoryId, product.ProductId);

            var category = await _context.Categories.FindAsync(product.CategoryId);
            var tmdbMovie = await tmdbService.GetMoviesByGenreAsync(category.TmdbId);

            var viewModel = new ProductDetailsViewModel
            {
                Product = product,
                RelatedProducts = relatedProducts,
                Movies = tmdbMovie.Take(5).ToList()
            };

            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTicketToCart(string movieTitle, string posterPath, string theaterName, int showTime, string seatNumber)
        { 
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
           
            var ticketCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryName == "Tickets");

            var ticketData = new
            {
                Theater = theaterName,
                Time = DateTime.Today.AddHours(showTime),
                Seat = seatNumber
            };
            
            
            var ticket = new ProductModel
            {
                Name = $"Ticket for {movieTitle}",
                Description = JsonSerializer.Serialize(ticketData),
                Price = 12.99m,
                QuantityAvailable = 100,
                CategoryId = ticketCategory.CategoryId,
                ImagePath = $"https://image.tmdb.org/t/p/w200{posterPath}"

            };

            _context.Products.Add(ticket);
            await _context.SaveChangesAsync();
            await cartService.AddToCartAsync(userId, ticket.ProductId);

            return RedirectToAction("Index", "Cart");

        }


    }
}
