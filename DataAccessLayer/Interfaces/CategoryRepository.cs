using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    using DataAccessLayer.Data;
    using DataAccessLayer.Entities;
    using Microsoft.EntityFrameworkCore;

    public class CategoryRepository : ICategoryRepository
    {
        private readonly AbsoluteCinemaDbContext CategoryDbContext;

        public CategoryRepository(AbsoluteCinemaDbContext CategoryDbContext)
        {
            this.CategoryDbContext = CategoryDbContext;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            return await CategoryDbContext.Categories.ToListAsync();
        }

        public async Task<CategoryModel?> GetByIdAsync(int id)
        {
            return await CategoryDbContext.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task AddAsync(CategoryModel category)
        {
            CategoryDbContext.Categories.Add(category);
            await CategoryDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CategoryModel category)
        {
            CategoryDbContext.Categories.Update(category);
            await CategoryDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await CategoryDbContext.Categories.FindAsync(id);
            if (category != null)
            {
                CategoryDbContext.Categories.Remove(category);
                await CategoryDbContext.SaveChangesAsync();
            }
        }
    }
}
