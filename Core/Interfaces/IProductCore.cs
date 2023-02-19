using Models.DTOs;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductCore
    {
        Task<ResultModel<PageModel<ProductDTO>>> GetAllProducts(GetAllProductDTO dto, string language);
        Task<ResultModel<ProductDTO>> GetProductByKey(string productKey, string language);
        ResultModel<ProductDTO> AddProduct(AddProductDTO dto);
        ResultModel<ProductDTO> UpdateProduct(UpdateProductDTO dto);
        ResultModel<bool> DeleteProduct(string productKey);
        ResultModel<bool> RateProduct(string productKey, int rate);
        Task<ResultModel<PageModel<ProductDTO>>> Search(SearchProductDTO searchDTO, string language);
    }
}
