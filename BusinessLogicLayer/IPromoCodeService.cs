using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public interface IPromoCodeService
    {
        Task<List<PromoCodeModel>> GetAllAsync();
        Task<PromoCodeModel> GetByIdAsync(int id);
        Task CreateAsync(PromoCodeModel promo);
        Task UpdateAsync(PromoCodeModel promo);
        Task DeleteAsync(int id);
        Task<PromoCodeModel> ValidateCodeAsync(string code);
    }
}
