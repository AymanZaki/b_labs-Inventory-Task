using Dal.Interfaces;
using Entites;
using Entites.Entities;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;
using ProductStatusEnum = Models.Enums.ProductStatus;

namespace Dal
{
    public class ProductDal : IProductDal
    {
        private readonly IInventoryContext _dbContext;
        public ProductDal(IInventoryContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Product AddProduct(Product product)
        {
            _dbContext.Products.Add(product);
            var result = _dbContext.SaveChangesAsync().Result;
            return product;
        }

        public async Task<(int, IEnumerable<Product>)> GetAllProducts(GetAllProductDTO dto, int? languageId = null)
        {
            var query = _dbContext.Products
                .Include(p => p.ProductLanguageDetails)
                .Include(p => p.Category)
                    .ThenInclude(c => c.CategoryDetails)
                .Include(p => p.ProductRating)
                .Where(x => x.ProductStatusId == dto.ProductStatus);

            if (languageId.HasValue)
            {
                query = query.Where(x => x.ProductLanguageDetails.Any(pd => pd.LanguageId == languageId.Value) &&
                                        x.Category.CategoryDetails.Any(c => c.LanguageId == languageId.Value));
            }
            if(!string.IsNullOrWhiteSpace(dto.CategoryKey))
            {
                query = query.Where(x => x.Category.CategoryKey.Equals(dto.CategoryKey));
            }
                
            int totalCount = await query.CountAsync();
                
            var products = await query.Skip((dto.Page - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();
            
            return (totalCount, products);
        }

        public async Task<Product> GetProductByKey(string productKey, int? languageId = null)
        {
            var query = _dbContext.Products
                .Include(p => p.ProductLanguageDetails)
                .Include(p => p.Category)
                    .ThenInclude(c => c.CategoryDetails)
                .Include(p => p.ProductRating)
                .Where(x => x.ProductKey.Equals(productKey));

            if (languageId.HasValue)
            {
                query = query.Where(x => x.ProductLanguageDetails.Any(pd => pd.LanguageId == languageId.Value) &&
                                        x.Category.CategoryDetails.Any(c => c.LanguageId == languageId.Value));
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Product> UpdateProduct(Product product)
        { 
            _dbContext.Products.Update(product);
            var isUpdated = (await _dbContext.SaveChangesAsync()) > 0;
            if (!isUpdated)
                return null;
            return product;
        }

        public async Task<(int, IEnumerable<Product>)> GetProducts(SearchProductDTO dto, int? languageId = null)
        {
            var query = _dbContext.Products
                .Include(p => p.ProductLanguageDetails)
                .Include(p => p.Category)
                    .ThenInclude(c => c.CategoryDetails)
                .Include(p => p.ProductRating)
                .Where(x => x.ProductStatusId == (int)ProductStatusEnum.Active
                );

            if(!string.IsNullOrWhiteSpace(dto.SearchKeyword))
            {
                query = query.Where(x => x.ProductLanguageDetails
                                            .Any(pd => (!languageId.HasValue || pd.LanguageId == languageId.Value) &&
                                                        pd.ProductName.ToLower().Contains(dto.SearchKeyword.ToLower()) &&
                                       x.Category.CategoryDetails
                                            .Any(c => (!languageId.HasValue || c.LanguageId == languageId.Value) &&
                                                        c.CateogryName.ToLower().Contains(dto.SearchKeyword)))
                                   );
            }

            int totalCount = await query.CountAsync();

            var products = await query.Skip((dto.Page - 1) * dto.PageSize)
                .Take(dto.PageSize)
                .ToListAsync();

            return (totalCount, products);
        }
    }
}
