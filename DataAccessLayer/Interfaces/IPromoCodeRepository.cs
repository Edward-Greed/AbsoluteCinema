using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IPromoCodeRepository
    {
        Task<List<PromoCodeModel>> GetAllAsync();
        Task<PromoCodeModel> GetByIdAsync(int id);
        Task<PromoCodeModel> GetByCodeAsync(string code);
        Task AddAsync(PromoCodeModel promo);
        Task UpdateAsync(PromoCodeModel promo);
        Task DeleteAsync(PromoCodeModel promo);
    }
}
