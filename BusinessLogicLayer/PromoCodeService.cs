using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class PromoCodeService : IPromoCodeService
    {
        private readonly IPromoCodeRepository PromoCoderepository;

        public PromoCodeService(IPromoCodeRepository PromoCoderepository)
        {
           this.PromoCoderepository = PromoCoderepository;
        }

        public async Task<List<PromoCodeModel>> GetAllAsync()
        {
            return await PromoCoderepository.GetAllAsync();
        }

        public async Task<PromoCodeModel> GetByIdAsync(int id)
        {
            return await PromoCoderepository.GetByIdAsync(id);
        }

        public async Task CreateAsync(PromoCodeModel promo)
        {
            await PromoCoderepository.AddAsync(promo);
        }

        public async Task UpdateAsync(PromoCodeModel promo)
        {
            await PromoCoderepository.UpdateAsync(promo);
        }

        public async Task DeleteAsync(int id)
        {
            var promo = await PromoCoderepository.GetByIdAsync(id);
            if (promo == null)
                return;

            await PromoCoderepository.DeleteAsync(promo);
        }

        public async Task<PromoCodeModel> ValidateCodeAsync(string code)
        {
            var promo = await PromoCoderepository.GetByCodeAsync(code);

            if (promo == null)
                return null;

            if (!promo.IsActive)
                return null;

            if (promo.ExpirationDate <= DateTime.UtcNow)
                return null;

            return promo;
        }
    }
}
