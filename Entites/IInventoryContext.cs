using Entites.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;

namespace Entites
{
    public interface IInventoryContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<CategoryLanguageDetails> CategoryDetails { get; set; }
        DbSet<Language> Languages { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductLanguageDetails> ProductDetails { get; set; }
        DbSet<ProductStatus> ProductStatuses { get; set; }
        DbSet<ProductRating> ProductRatings { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserSearchHistory> UserSearchHistories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
