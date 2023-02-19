using AutoMapper;
using Core.Interfaces;
using Dal.Interfaces;
using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class CategoryCore : ICategoryCore
    {
        private readonly IMapper _mapper;
        private readonly ICategoryDal _categoryDal;

        public CategoryCore(IMapper mapper, ICategoryDal categoryDal)
        {
            _mapper = mapper;
            _categoryDal = categoryDal;
        }

        public async Task<ResultModel<CategoryDTO>> GetCategoryByKey(string categoryKey)
        {
            var category = await _categoryDal.GetCategoryByKey(categoryKey);
            if(category ==  null)
            {
                return new ResultModel<CategoryDTO>
                {
                    Data = null,
                    Message = "Category not found",
                    StatusCode = HttpStatusCode.BadRequest,
                };
            }
            var categoryDto = _mapper.Map<CategoryDTO>(category);
            return new ResultModel<CategoryDTO>
            {
                Data = categoryDto,
                Message = "Succeeded",
                StatusCode = HttpStatusCode.OK,
            };
        }
    }
}
