using BusinessLogicLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AbsoluteCinema.Controllers
{
    public class PromoCodeController : Controller
    {
        private readonly IPromoCodeService promoCodeService;

        public PromoCodeController(IPromoCodeService promoCodeService)
        {
            this.promoCodeService = promoCodeService;
        }

        public async Task<IActionResult> Index()
        {
            var promos = await promoCodeService.GetAllAsync();
            return View(promos);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PromoCodeModel promo)
        {
            if (!ModelState.IsValid)
                return View(promo);

            await promoCodeService.CreateAsync(promo);

            return RedirectToAction(nameof(Index));
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
