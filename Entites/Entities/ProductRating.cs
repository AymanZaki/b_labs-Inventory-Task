
namespace Entites.Entities
{
    public class ProductRating : BaseEntity
    {
        public int ProductRatingId { get; set; }
        public int ProductId { get; set; }
        public int ProductRatingCount { get; set; }
        public int ProductRatings { get; set; }
    }
}
