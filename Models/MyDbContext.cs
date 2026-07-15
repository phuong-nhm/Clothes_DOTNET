using CLothes_DOTNET.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Emit;

namespace Clothes_DOTNET.Models
{
	public class MyDbContext : IdentityDbContext<AppUser>
	{
		public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
		{

		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Nếu muốn thay đổi tên bảng hoặc tùy chỉnh thêm
			builder.Entity<AppUser>().ToTable("Users"); // Đảm bảo tên bảng là 'Users'
			builder.Entity<IdentityRole>().ToTable("Roles");
			builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
			builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
			builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
			builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
			builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
			builder.Entity<Product>().ToTable("product");
			builder.Entity<Category>().ToTable("category");
            builder.Entity<Order>().ToTable("order");
            builder.Entity<Order_detail>().ToTable("orderdetail");
            builder.Entity<Sale>().ToTable("Sale");
            builder.Entity<CategoryClaim>().ToTable("CategoryClaim");
            builder.Entity<Nhap>().ToTable("Nhap");
            builder.Entity<Xuat>().ToTable("Xuat");
            builder.Entity<Ton>().ToTable("Ton");
            builder.Entity<Color>().ToTable("Color");
            builder.Entity<Size>().ToTable("Size");
            builder.Entity<ProductVariant>().ToTable("ProductVariant");
            
            builder.Entity<Roles>().ToTable("Roles");
            builder.Entity<RoleClaim>().ToTable("RoleClaims");


        }
        public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_detail> Order_details { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<CategoryClaim> CategoryClaims { get; set; }
        public DbSet<Nhap> Nhaps { get; set; }
        public DbSet<Xuat> Xuats { get; set; }
        public DbSet<Ton> Tons { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
    }
}
