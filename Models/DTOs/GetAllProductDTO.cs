
using ProductStatusEnum = Models.Enums.ProductStatus;

namespace Models.DTOs
{
    public class GetAllProductDTO
    {
        public int Page { get; set; } = Constants.Page;
        public int PageSize { get; set; } = Constants.PageSize;
        public int ProductStatus { get; set; } = (int)ProductStatusEnum.Active;
        public string CategoryKey { get; set; }
    }
}
