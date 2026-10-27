using CLothes_DOTNET.Dto;
using Clothes_DOTNET.Models;

namespace CLothes_DOTNET.Extensions
{
    /// <summary>
    /// Extension methods ð? map Entity sang DTO
    /// Giúp d? convert data trong Service/Controller
    /// </summary>
    public static class DtoMappingExtensions
    {
        #region Product Mapping
        
        /// <summary>
        /// Map Product Entity sang ProductListDto (dùng cho GetAllProduct API)
        /// </summary>
        public static ProductListDto ToListDto(this Product product, string categoryName = null)
        {
            if (product == null) return null;

            return new ProductListDto
            {
                id = product.ID,
                name = product.name,
                sellprice = product.sellprice,
                image = product.image,
                category_id = product.category_id,
                category_name = categoryName ?? product.category?.Name
            };
        }

        /// <summary>
        /// Map Product Entity sang ProductDto (dùng cho Admin Product Management)
        /// </summary>
        public static ProductDto ToDto(this Product product)
        {
            if (product == null) return null;

            return new ProductDto
            {
                id = product.ID,
                name = product.name,
                importprice = product.importprice,
                sellprice = product.sellprice,
                image = product.image,
                category_id = product.category_id,
                category_name = product.category?.Name
            };
        }

        #endregion

        #region Category Mapping

        /// <summary>
        /// Map Category Entity sang CategoryDto
        /// </summary>
        public static CategoryDto ToDto(this Category category)
        {
            if (category == null) return null;

            return new CategoryDto
            {
                id = category.ID,
                name = category.Name,
                gender = category.Gender,
                category_claim_id = category.Category_claim_Id,
                claim_name = category.claim?.ClaimName
            };
        }

        #endregion

        #region CategoryClaim Mapping

        /// <summary>
        /// Map CategoryClaim Entity sang CategoryClaimDto
        /// </summary>
        public static CategoryClaimDto ToDto(this CategoryClaim claim)
        {
            if (claim == null) return null;

            return new CategoryClaimDto
            {
                id = claim.ID,
                claimName = claim.ClaimName
            };
        }

        #endregion

        #region Color Mapping

        /// <summary>
        /// Map Color Entity sang ColorDto
        /// </summary>
        public static ColorDto ToDto(this Color color)
        {
            if (color == null) return null;

            return new ColorDto
            {
                colorId = color.ColorID,
                colorName = color.ColorName
            };
        }

        #endregion

        #region Size Mapping

        /// <summary>
        /// Map Size Entity sang SizeDto
        /// </summary>
        public static SizeDto ToDto(this Size size)
        {
            if (size == null) return null;

            return new SizeDto
            {
                sizeId = size.SizeID,
                sizeName = size.SizeName
            };
        }

        #endregion

        #region ProductVariant Mapping

        /// <summary>
        /// Map ProductVariant Entity sang ProductVariantDto
        /// </summary>
        public static ProductVariantDto ToDto(this ProductVariant variant)
        {
            if (variant == null) return null;

            return new ProductVariantDto
            {
                variantId = variant.VariantID,
                productId = variant.ProductID,
                sizeId = variant.SizeID ?? 0,
                sizeName = variant.Size?.SizeName,
                colorId = variant.ColorID ?? 0,
                colorName = variant.Color?.ColorName,
                quantity = variant.Quantity
            };
        }

        #endregion

        #region Sale Mapping

        /// <summary>
        /// Map Sale Entity sang SaleDto (dùng trong GetAllProduct API)
        /// </summary>
        public static SaleDto ToListDto(this Sale sale)
        {
            if (sale == null) return null;

            return new SaleDto
            {
                id = sale.ID,
                productId = sale.ProductId,
                discountpercentage = sale.discountpercentage,
                startDate = sale.StartDate,
                endDate = sale.EndDate
            };
        }

        /// <summary>
        /// Map Sale Entity sang SaleManagementDto (dùng cho Admin Sale Management)
        /// </summary>
        public static SaleManagementDto ToManagementDto(this Sale sale, string productName = null)
        {
            if (sale == null) return null;

            return new SaleManagementDto
            {
                id = sale.ID,
                productId = sale.ProductId,
                productName = productName ?? sale.product?.name,
                discountpercentage = sale.discountpercentage,
                startDate = sale.StartDate,
                endDate = sale.EndDate
            };
        }

        #endregion

        #region Order Mapping

        /// <summary>
        /// Map Order Entity sang OrderDto
        /// </summary>
        public static OrderDto ToDto(this Order order)
        {
            if (order == null) return null;

            return new OrderDto
            {
                idOrder = order.IDOrder,
                fullName = order.FullName,
                email = order.Email,
                phoneNumber = order.PhoneNumber,
                address = order.Address,
                note = order.Note,
                orderDate = order.OrderDate,
                status = order.Status,
                totalMoney = order.TotalMoney,
                userId = order.UserID
            };
        }

        #endregion

        #region Order_detail Mapping

        /// <summary>
        /// Map Order_detail Entity sang OrderDetailDto
        /// </summary>
        public static OrderDetailDto ToDto(this Order_detail orderDetail, string productName = null, string productImage = null)
        {
            if (orderDetail == null) return null;

            return new OrderDetailDto
            {
                id = orderDetail.Id,
                productId = orderDetail.product_id,
                productName = productName ?? orderDetail.productvariants?.Product?.name,
                productImage = productImage ?? orderDetail.productvariants?.Product?.image,
                price = orderDetail.Price,
                quantity = orderDetail.Quantity,
                totalMoney = orderDetail.TotalMoney,
                orderId = orderDetail.idOder
            };
        }

        #endregion

        #region AppUser Mapping

        /// <summary>
        /// Map AppUser Entity sang UserDto (thông tin cõ b?n)
        /// </summary>
        public static UserDto ToDto(this AppUser user)
        {
            if (user == null) return null;

            return new UserDto
            {
                id = user.Id,
                userName = user.UserName,
                email = user.Email,
                fullName = user.FullName,
                address = user.Address,
                phoneNumber = user.PhoneNumber,
                birthDate = user.BirthDate,
                gender = user.Gender
            };
        }

        #endregion
    }
}
