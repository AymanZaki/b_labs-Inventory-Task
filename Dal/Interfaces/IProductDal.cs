using Entites.Entities;
using Models.DTOs;

namespace Dal.Interfaces
{
    public interface IProductDal
    {
        Task<(int, IEnumerable<Product>)> GetAllProducts(GetAllProductDTO dto, int? languageId = null);
        Task<Product> GetProductByKey(string productKey, int? languageId = null);
        Product AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<(int, IEnumerable<Product>)> GetProducts(SearchProductDTO dto, int? languageId = null);
    }
}
