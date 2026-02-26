using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public class PromoCodeRepository : IPromoCodeRepository
    {
        private readonly AbsoluteCinemaDbContext promoCodeDbContext;

        public PromoCodeRepository(AbsoluteCinemaDbContext promoCodeDbContext)
        {
            this.promoCodeDbContext = promoCodeDbContext;
        }

        public async Task<List<PromoCodeModel>> GetAllAsync()
        {
            return await promoCodeDbContext.PromoCodes.ToListAsync();
        }

        public async Task<PromoCodeModel> GetByIdAsync(int id)
        {
            return await promoCodeDbContext.PromoCodes.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<PromoCodeModel> GetByCodeAsync(string code)
        {
            return await promoCodeDbContext.PromoCodes
                .FirstOrDefaultAsync(p => p.Code == code);
        }

        public async Task AddAsync(PromoCodeModel promo)
        {
            promoCodeDbContext.PromoCodes.Add(promo);
            await promoCodeDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PromoCodeModel promo)
        {
            promoCodeDbContext.PromoCodes.Update(promo);
            await promoCodeDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(PromoCodeModel promo)
        {
            promoCodeDbContext.PromoCodes.Remove(promo);
            await promoCodeDbContext.SaveChangesAsync();
        }
    }
}
