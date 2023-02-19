using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Interfaces
{
    public interface IUserSearchHistoryDal
    {
        Task<IEnumerable<UserSearchHistory>> GetSearchHistoryByUserId(int userId);
        UserSearchHistory AddSearchHistory(UserSearchHistory searchHistory);
    }
}
