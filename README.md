# Clothing Shop Web App

Website bán hàng quần áo theo mô hình MVC truyền thống, có đầy đủ CMS quản trị.

## Tính năng chính

**Khách hàng**
- Xem/tìm kiếm sản phẩm, filter theo danh mục/thuộc tính
- Giỏ hàng, đặt hàng
- Đánh giá (review) sản phẩm

**Quản trị (CMS)**
- Quản lý sản phẩm, quản lý tồn kho
- Quản lý đơn hàng
- Phân quyền theo vai trò (admin/khách hàng)

## Tech Stack

**Backend & Frontend**: ASP.NET MVC (cùng một project, không tách API/Frontend riêng)

**Database**: SQL Server

**Authentication**: dùng cơ chế xác thực tích hợp sẵn của ASP.NET

## Cài đặt & chạy local

```
git clone <repo-url>
cd <project-folder>
# cấu hình connection string SQL Server trong appsettings.json
dotnet ef database update
dotnet run
```

Dự án hiện chỉ chạy ở môi trường local, chưa deploy lên server/cloud.

## Việc còn thiếu / định hướng mở rộng

- Tích hợp thanh toán trực tuyến (VNPay)
- Deploy lên môi trường production (hiện mới chạy local)
