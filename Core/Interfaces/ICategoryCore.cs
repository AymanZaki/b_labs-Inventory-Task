using Models;
using Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICategoryCore
    {
        Task<ResultModel<CategoryDTO>> GetCategoryByKey(string categoryKey);
    }
}
