# ?? DTO Structure Documentation

## T?ng Quan

Folder `Dto/` ch?a các **Data Transfer Objects (DTO)** ð?:
1. **Lo?i b? circular reference** khi serialize Entity sang JSON
2. **Chu?n hóa API responses** (consistent field naming: camelCase)
3. **Validate input** cho create/update operations
4. **Gi?m kích thý?c d? li?u** tr? v? (ch? l?y field c?n thi?t)

---

## ?? C?u Trúc File

### `ProductListDto.cs`
**Dùng cho:** `/Sale/GetAllProduct` API (Client-side listing)

- **ProductListDto**: DTO s?n ph?m trong danh sách (lo?i b? circular ref)
- **SaleDto**: DTO khuy?n m?i
- **GetAllProductResponseDto**: Response object c?a API (ch?a m?ng products + sales + phân trang)

**M?c ðích:** Thay th? vi?c tr? raw Entity `Product` (gây circular ref v?i `Category`), ch? tr? field c?n thi?t

**Example JSON Response:**
```json
{
  "data": [
    {
      "id": 1,
      "name": "Áo thun Uniqlo",
      "sellprice": 199000,
      "image": "/UserImage/images/shirt.jpg",
      "category_id": 5,
      "category_name": "T-shirt"
    }
  ],
  "totalItems": 100,
  "currentPage": 1,
  "numberpage": 5,
  "pagesize": 12,
  "objectsale": [
    {
      "id": 1,
      "productId": 1,
      "discountpercentage": 20,
      "startDate": "2025-01-01",
      "endDate": "2025-01-31"
    }
  ]
}
```

---

### `CategoryDto.cs`
**Dùng cho:** Admin Category Management

- **CategoryDto**: DTO category (output)
- **CreateUpdateCategoryDto**: Input DTO cho create/update

**Key:** Ch?a `claim_name` ð? display thông tin, không l?y c? object `claim` (tránh circular ref)

---

### `CategoryClaimDto.cs`
**Dùng cho:** Admin CategoryClaim (phân m?c chung)

- **CategoryClaimDto**: Output DTO
- **CreateUpdateCategoryClaimDto**: Input DTO

---

### `ColorDto.cs` & `SizeDto.cs`
**Dùng cho:** Admin Color/Size Management

- Ðõn gi?n v? không có circular reference
- Ch? serialize field ID + Name

---

### `ProductDto.cs`
**Dùng cho:** Admin Product Management

- **ProductDto**: Product info (output)
- **CreateProductDto**: T?o m?i s?n ph?m
- **UpdateProductDto**: C?p nh?t s?n ph?m
- **ProductVariantDto**: Bi?n th? s?n ph?m
- **CreateProductVariantDto**: T?o m?i variant

**Note:** Khác v?i `ProductListDto` ? ch?:
- `ProductListDto`: Dùng cho client listing (ch? có sellprice, lo?i b? importprice)
- `ProductDto`: Dùng cho admin (ð?y ð? c? importprice, không có category_id ph?c t?p)

---

### `SaleDto.cs`
**Dùng cho:** Admin Sale Management

- **SaleManagementDto**: Khuy?n m?i (output) - có `productName`
- **CreateSaleDto**: T?o m?i khuy?n m?i - nh?n list `productIds`

**Note:** C?n th?n ð?ng nh?m v?i `SaleDto` trong `ProductListDto.cs` (là 2 context khác nhau)

---

### `OrderDto.cs`
**Dùng cho:** Admin Bill Management + Checkout response

- **OrderDto**: Ðõn hàng (output)
- **OrderDetailDto**: Chi ti?t ðõn hàng (s?n ph?m trong ðõn)
- **OrderWithDetailsDto**: Ðõn hàng + danh sách s?n ph?m (dùng khi load chi ti?t Bill)
- **UpdateOrderStatusDto**: Input ð? thay ð?i status

---

### `UserDto.cs`
**Dùng cho:** User Profile, Order info, etc.

- **UserDto**: User info cõ b?n (output)
- **UpdateUserProfileDto**: Input cho c?p nh?t profile

---

## ?? Extension Methods (`DtoMappingExtensions.cs`)

Ð? d? mapping Entity ? DTO, s? d?ng extension methods:

```csharp
// Entity ? ListDto
var productDto = product.ToListDto(categoryName);

// Entity ? FullDto
var productDto = product.ToDto();

// Entity ? SaleDto
var saleDto = sale.ToListDto();
```

---

## ?? Cách S? D?ng (Ví d?)

### Service Layer
```csharp
public List<ProductListDto> SearchProducts(SearchProduct criteria)
{
    var query = _productService.SearchProducts(criteria, listid);
    var products = _productService.GetProducts(query);
    
    // Map Entity ? DTO
    return products.Select(p => p.ToListDto()).ToList();
}
```

### Controller Layer
```csharp
[HttpGet]
public ActionResult GetAllProduct(string searchname, int page = 1, int pagesize = 12)
{
    var data = _productService.SearchProducts(/* criteria */);
    var sales = _saleService.Getproductidobject();
    
    return Json(new GetAllProductResponseDto
    {
        data = data,
        objectsale = sales.Select(s => s.ToListDto()).ToList(),
        totalItems = totalCount,
        currentPage = page,
        numberpage = totalPages,
        pagesize = pagesize
    });
}
```

---

## ?? Lýu ? Quan Tr?ng

1. **Naming Convention:**
   - DTO properties dùng **camelCase** (id, name, categoryName)
   - Ð? `System.Text.Json` t? convert camelCase khi serialize (không c?n thêm `[JsonPropertyName]`)

2. **Circular Reference:**
   - ? Dùng DTO: `category_name` (string) thay v? `category` (object)
   - ? KHÔNG dùng: `public Category category { get; set; }`

3. **Validation:**
   - Input DTO (`CreateXxxDto`, `UpdateXxxDto`) dùng `[Required]`, `[StringLength]`, etc.
   - Output DTO (không c?n validate v? data t? DB ð? valid)

4. **Tách Output/Input DTO:**
   - `ProductDto`: Output (read-only, có ð?y ð? data)
   - `CreateProductDto`: Input (ch? nh?ng field c?n nh?p)
   - `UpdateProductDto`: Input (có ID + fields ð? update)

5. **M? R?ng:**
   - Khi thêm DTO m?i, thêm extension method vào `DtoMappingExtensions.cs`
   - Ð?t tên chu?n: `ToDto()` (full), `ToListDto()` (simplified), `ToManagementDto()` (management)

---

## ?? Next Steps (Session Ti?p Theo)

1. **Map Services:** S?a ProductService, SaleService, etc. ð? tr? DTO thay v? Entity
2. **Update Controllers:** S?a SaleController.GetAllProduct(), Areas/Admin/* ð? dùng DTO
3. **Update JS:** C?p nh?t wwwroot/js/Getproduct.js ð? không ð?c `$values` (v? JSON s? là m?ng thu?n)
4. **Remove ReferenceHandler.Preserve:** C?p nh?t Program.cs n?u không c?n c?n

---

## ?? Summary

| File | Dùng Cho | Entity | DTO |
|------|----------|--------|-----|
| ProductListDto.cs | Client GetAllProduct API | Product | ProductListDto |
| ProductDto.cs | Admin Product CRUD | Product | ProductDto / CreateProductDto / UpdateProductDto |
| CategoryDto.cs | Admin Category CRUD | Category | CategoryDto / CreateUpdateCategoryDto |
| CategoryClaimDto.cs | Admin CategoryClaim CRUD | CategoryClaim | CategoryClaimDto |
| ColorDto.cs | Admin Color CRUD | Color | ColorDto / CreateUpdateColorDto |
| SizeDto.cs | Admin Size CRUD | Size | SizeDto / CreateUpdateSizeDto |
| SaleDto.cs | Admin Sale CRUD | Sale | SaleManagementDto / CreateSaleDto |
| OrderDto.cs | Admin Bill Management | Order, Order_detail | OrderDto, OrderDetailDto, OrderWithDetailsDto |
| UserDto.cs | User Profile | AppUser | UserDto / UpdateUserProfileDto |

---

**T?o ngày:** 2025-01-XX
**Status:** ? Các DTO ð? ðý?c t?o, ch? mapping vào Service/Controller ? session ti?p theo
