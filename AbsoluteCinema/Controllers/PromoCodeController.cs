using AbsoluteCinema.Models;
using BusinessLogicLayer;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.Controllers
{
    public class PromoCodeController : Controller
    {
        private readonly IPromoCodeService promoCodeService;
        private readonly AbsoluteCinemaDbContext _context;

        public PromoCodeController(IPromoCodeService promoCodeService, AbsoluteCinemaDbContext _context)
        {
            this.promoCodeService = promoCodeService;
            this._context = _context;
        }

        public async Task<IActionResult> Index()
        {
            var promos = await promoCodeService.GetAllAsync();
            return View(promos);
        }

        public IActionResult Create()
        {
            return View(new PromoCodeViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PromoCodeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var promo = new PromoCodeModel
            {
                Code = model.Code,
                DiscountPercentage = model.DiscountPercentage,
                IsActive = model.IsActive,
                ExpirationDate = model.ExpirationDate
            };

            _context.PromoCodes.Add(promo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    
        public async Task<IActionResult> Edit(int id)
        {
            var promo = await promoCodeService.GetByIdAsync(id);
            if (promo == null)
                return NotFound();

            return View(promo);
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PromoCodeModel promo)
        {
            if (!ModelState.IsValid)
                return View(promo);

            await promoCodeService.UpdateAsync(promo);

            return RedirectToAction(nameof(Index));
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await promoCodeService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
