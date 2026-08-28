using App.Services;
using Clothes_DOTNET.Models;
using Clothes_DOTNET.Service;
using CLothes_DOTNET.ExtendMethods;
using CLothes_DOTNET.Models;
using CLothes_DOTNET.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using System;

namespace Clothes_DOTNET

{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectString = "";
            builder.Services.AddDbContext<MyDbContext>(options =>
            {
                connectString = builder.Configuration.GetConnectionString("AppMvcConnectionString");
                options.UseSqlServer(connectString);
            });
            builder.Services.AddOptions();
            var mailsetting = builder.Configuration.GetSection("MailSettings");
            builder.Services.Configure<MailSettings>(mailsetting);
            builder.Services.AddSingleton<IEmailSender, SendMailService>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ColorService>();
            builder.Services.AddScoped<SizeService>();
            builder.Services.AddScoped<ProductVariantService>();
            builder.Services.AddScoped<CartServices>();
            builder.Services.AddScoped<OrderDetailService>();
            builder.Services.AddScoped<OrderService>();
            builder.Services.AddScoped<CategoryService>();
            builder.Services.AddScoped<SaleService>();
            builder.Services.AddScoped<CategoryClaimService>();
            builder.Services.AddSingleton<List_Category_Service>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddLogging();

            //dang ki identity
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<MyDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<IdentityOptions>(options => {
                // Thiết lập về Password
                options.Password.RequireDigit = false; // Không bắt phải có số
                options.Password.RequireLowercase = false; // Không bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = false; // Không bắt buộc chữ in
                options.Password.RequiredLength = 3; // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 1; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = true;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = true;
            });
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Login";
                options.LogoutPath = "/Logout";
                options.AccessDeniedPath = "/khongduoctruycap";

            });

            builder.Services.AddAuthentication()
                .AddCookie()
                .AddGoogle(options =>
                {
                    var googleconfig = builder.Configuration.GetSection("Authentication:Google");
                    options.ClientId = googleconfig["ClientId"];
                    options.ClientSecret = googleconfig["ClientSecret"];
                    //signin-google
                    options.CallbackPath = "/dang-nhap-tu-google";
                });
            builder.Services.AddSingleton<IdentityErrorDescriber, AppIdentityErrorDescriber>();
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                });

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDistributedMemoryCache(); // Lưu cache trong bộ nhớ (cho Session)
            builder.Services.AddSession(cfg =>
            {
                cfg.Cookie.Name = "phuong";             // Đặt tên Session
                cfg.IdleTimeout = new TimeSpan(0, 30, 0);   // Thời gian tồn tại của Session
            });
            var app = builder.Build();
            

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.AddStatusCodePage();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();

        }
    }
}
