using AutoMapper;
using Core.Interfaces;
using Dal;
using Dal.Interfaces;
using Entites.Entities;
using Models;
using Models.DTOs;
using System.Net;
using System.Runtime.CompilerServices;
using LanguageEnum = Models.Enums.Language;
using ProductStatusEnum = Models.Enums.ProductStatus;

namespace Core
{
    public class ProductCore : IProductCore
    {
        private readonly IProductDal _productDal;
        private readonly IProductRatingDal _productRatingDal;
        private readonly IMapper _mapper;
        private readonly ICategoryCore _categoryCore;
        private readonly IUserSearchHistoryDal _searchHistoryDal;

        public ProductCore(IProductDal productDal, IProductRatingDal productRatingDal, IMapper mapper,
            ICategoryCore categoryCore, IUserSearchHistoryDal searchHistoryDal)
        {
            _productDal = productDal;
            _mapper = mapper;
            _categoryCore = categoryCore;
            _productRatingDal = productRatingDal;
            _searchHistoryDal = searchHistoryDal;
        }

        public async Task<ResultModel<PageModel<ProductDTO>>> GetAllProducts(GetAllProductDTO dto, string language)
        {
            var (totalCount, products) = await _productDal.GetAllProducts(dto, GetLanguageId(language));
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            var pageModel = new PageModel<ProductDTO>
            {
                PageNumber = dto.Page,
                TotalCount = totalCount,
                PageSize = dto.PageSize,
                Results = productsDto,
                PagesCount = (int)Math.Ceiling(totalCount / (decimal)dto.PageSize)
            };

            return new ResultModel<PageModel<ProductDTO>>
            {
                Data = pageModel,
                Message = "Succeeded",
                StatusCode = HttpStatusCode.OK,
            };
        }

        public async Task<ResultModel<ProductDTO>> GetProductByKey(string productKey, string language)
        {
            var product = await _productDal.GetProductByKey(productKey, GetLanguageId(language));
            var productDto = _mapper.Map<ProductDTO>(product);
            if(product == null)
            {
                return new ResultModel<ProductDTO>
                {
                    Data = productDto,
                    Message = "Succeeded",
                    StatusCode = HttpStatusCode.NotFound, 
                };
            }

            return new ResultModel<ProductDTO>
            {
                Data = productDto,
                Message = "Succeeded",
                StatusCode = HttpStatusCode.OK,
            };
        }

        public ResultModel<ProductDTO> AddProduct(AddProductDTO dto)
        {
            var categoryDto = _categoryCore.GetCategoryByKey(dto.CategoryKey).Result;
            if(categoryDto.Data == null)
            {
                return new ResultModel<ProductDTO>
                {
                    Data = null,
                    Message = categoryDto.Message,
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
            var entity = _mapper.Map<Product>(dto);
            entity.ProductStatusId = (int)ProductStatusEnum.Active;
            entity.ProductKey = Guid.NewGuid().ToString();
            entity.CategoryId = categoryDto.Data.CategoryId;

            var product = _productDal.AddProduct(entity);
            var productDto = _mapper.Map<ProductDTO>(product);

            return new ResultModel<ProductDTO>
            {
                Data = productDto,
                Message = "Succeeded",
                StatusCode = HttpStatusCode.OK,
            };
        }

        public ResultModel<bool> DeleteProduct(string productKey)
        {
            var product = _productDal.GetProductByKey(productKey).Result;
            product.ProductStatusId = (int)ProductStatusEnum.Deleted;
            product = _productDal.UpdateProduct(product).Result;
            
            if(product == null)
            {
                return new ResultModel<bool>
                {
                    Data = false,
                    Message = "Failed to delete product",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
            return new ResultModel<bool>
            {
                Data = true,
                Message = "Succeeded",
                StatusCode = HttpStatusCode.OK,
            };

        }

        public ResultModel<bool> RateProduct(string productKey, int rate)
        {
            var product = _productDal.GetProductByKey(productKey).Result;
            if(product == null)
            {
                return new ResultModel<bool>
                {
                    Data = false,
                    Message = "Product not found",
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
            var productRating = _productRatingDal.GetProductRating(product.ProductId).Result;
            if(productRating == null)
            {
                var rating = _productRatingDal.AddProductRating(new ProductRating
                {
                    ProductId = product.ProductId,
                    ProductRatings = rate,
                    ProductRatingCount = 1,
                });
            }
            else
            {
                productRating.ProductRatingCount++;
                productRating.ProductRatings += rate;
                _productRatingDal.UpdateRating(productRating);
            }

            return new ResultModel<bool>
            {
                Data = true,
                Message = "Updated Successfully",
                StatusCode = HttpStatusCode.OK,
            };
        }

        public async Task<ResultModel<PageModel<ProductDTO>>> Search(SearchProductDTO searchDTO, string language)
        {
            var (totalCount, products) = await _productDal.GetProducts(searchDTO, GetLanguageId(language));
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            var pageModel = new PageModel<ProductDTO>
            {
                PageNumber = searchDTO.Page,
                TotalCount = totalCount,
                PageSize = searchDTO.PageSize,
                Results = productsDto,
                PagesCount = (int)Math.Ceiling(totalCount / (decimal)searchDTO.PageSize)
            };
            if (searchDTO.UserId.HasValue)
            {
                _searchHistoryDal.AddSearchHistory(new UserSearchHistory
                {
                    SearchKeyword = searchDTO.SearchKeyword,
                    UserId = searchDTO.UserId.Value,
                });
            }
            return new ResultModel<PageModel<ProductDTO>>
            {
                Data = pageModel,
                Message = "Succeeded",
                StatusCode = HttpStatusCode.OK,
            };
        }
        public ResultModel<ProductDTO> UpdateProduct(UpdateProductDTO dto)
        {
            var product = _productDal.GetProductByKey(dto.ProductKey).Result;
            if (product == null)
            {
                return new ResultModel<ProductDTO>
                {
                    Data = null,
                    Message = "Product not found",
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
            product.Price = dto.Price;
            product.ImageUrl = dto.ImageUrl;
            foreach(var languageDetails in dto.ProductLanguageDetails)
            {
                var productDetails = product.ProductLanguageDetails.FirstOrDefault(x => x.LanguageId == languageDetails.LanguageId);
                productDetails.ProductName = languageDetails.ProductName;
                productDetails.ProductUrl = languageDetails.ProductUrl;
            }
            product = _productDal.UpdateProduct(product).Result;
            if(product == null)
            {
                return new ResultModel<ProductDTO>
                {
                    Data = null,
                    Message = "Failed to update product",
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }
            var productDto = _mapper.Map<ProductDTO>(product);
            return new ResultModel<ProductDTO>
            {
                Data = productDto,
                Message = "Succeeded",
                StatusCode = HttpStatusCode.OK,
            };
        }

        private int? GetLanguageId(string language)
        {
            if(string.IsNullOrEmpty(language))
            {
                return null;
            }
            if (language.ToLower().Equals("ar"))
                return (int)LanguageEnum.Arabic;
            return (int)LanguageEnum.English;
        }

    }
}
