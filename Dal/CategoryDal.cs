using Dal.Interfaces;
using Entites;
using Entites.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public class CategoryDal : ICategoryDal
    {
        private readonly IInventoryContext _dbContext;
        public CategoryDal(IInventoryContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> GetCategoryByKey(string categoryKey)
        {
            var category = await _dbContext.Categories
                .FirstOrDefaultAsync(cat => cat.CategoryKey.Equals(categoryKey));

            return category;
        }
    }
}
