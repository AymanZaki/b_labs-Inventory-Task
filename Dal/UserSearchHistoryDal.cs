using Dal.Interfaces;
using Entites.Entities;
using Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class UserSearchHistoryDal : IUserSearchHistoryDal
    {
        private IInventoryContext _dbContext;

        public UserSearchHistoryDal(IInventoryContext dbContext)
        {
            _dbContext = dbContext;
        }
        public UserSearchHistory AddSearchHistory(UserSearchHistory searchHistory)
        {
            _dbContext.UserSearchHistories.Add(searchHistory);
            var result = _dbContext.SaveChangesAsync().Result;
            return searchHistory;
        }

        public async Task<IEnumerable<UserSearchHistory>> GetSearchHistoryByUserId(int userId)
        {
            var userSearchHistories = await _dbContext.UserSearchHistories
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Take(3)
                .ToListAsync();
            return userSearchHistories;
        }
    }
}
