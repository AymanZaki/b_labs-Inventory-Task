using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IProductRatingDal
    {
        Task<ProductRating> GetProductRating(int productId);
        ProductRating UpdateRating(ProductRating entity);
        ProductRating AddProductRating(ProductRating entity);
    }
}
