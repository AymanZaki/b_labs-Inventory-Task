using AutoMapper;
using Entites.Entities;
using Models.DTOs;
using System.ComponentModel;

namespace b_labs_Inventory_API.Mapper
{
    public class MappingProfile : Profile
    {
        IConfiguration configuration;
        public MappingProfile(IConfiguration configuration)
        {
            this.configuration = configuration;

            #region Map Entity To Dto
            MapProductEntityToProductDTO();
            MapCategoryEntityToCategoryDTO();
            MapProductStatusEntityToProductStatusDTO();
            MapProductLanguageDetailsEntityToProductLanguageDetailsDTO();
            MapCategoryLanguageDetailsEntityToCategoryLanguageDetailsDTO();
            #endregion

            #region Map DTO to Entity
            MapAddProductDTOToProductEntity();
            MapProductLanguageDetailsDTOToProductLanguageDetailsEntity();
            #endregion
        }


        #region Map Entity To Dto
        private void MapProductEntityToProductDTO()
        {
            CreateMap<Product, ProductDTO>()
                .AfterMap((src, dest) =>
                {
                    if (src.ProductRating != null)
                    {
                        dest.Rating = Math.Round((double)src.ProductRating.ProductRatings / src.ProductRating.ProductRatingCount, 2);
                    }
                });
        }
        private void MapCategoryEntityToCategoryDTO()
        {
            CreateMap<Category, CategoryDTO>();
        }
        private void MapProductStatusEntityToProductStatusDTO()
        {
            CreateMap<ProductStatus, ProductStatusDTO>();
        }
        private void MapProductLanguageDetailsEntityToProductLanguageDetailsDTO()
        {
            CreateMap<ProductLanguageDetails, ProductLanguageDetailsDTO>();
        }
        private void MapCategoryLanguageDetailsEntityToCategoryLanguageDetailsDTO()
        {
            CreateMap<CategoryLanguageDetails, CategoryLanguageDetailsDTO>();
        }
        #endregion


        #region Map DTO to Entity
        private void MapAddProductDTOToProductEntity()
        {
            CreateMap<AddProductDTO, Product>()
                ;
        }
        private void MapProductLanguageDetailsDTOToProductLanguageDetailsEntity()
        {
            CreateMap<ProductLanguageDetailsDTO, ProductLanguageDetails>()
                ;
        }
        #endregion
    }
}
