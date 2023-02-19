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
    public class ProductRatingDal : IProductRatingDal
    {
        private IInventoryContext _dbContext;
        public ProductRatingDal(IInventoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductRating AddProductRating(ProductRating entity)
        {
            _dbContext.ProductRatings.Add(entity);
            var result = _dbContext.SaveChangesAsync().Result;
            return entity;
        }

        public async Task<ProductRating> GetProductRating(int productId)
        {
            var rating = await _dbContext.ProductRatings.FirstOrDefaultAsync(x => x.ProductId == productId);
            return rating;
        }

        public ProductRating UpdateRating(ProductRating entity)
        {
            _dbContext.ProductRatings.Update(entity);
            var isUpdated = (_dbContext.SaveChangesAsync().Result > 0);
            return entity;
        }
    }
}
