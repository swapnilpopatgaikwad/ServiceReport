//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.IdentityModel.Tokens;
//using ServiceReport.Data;
//using ServiceReport.Helper;
//using ServiceReport.Interface;
//using ServiceReport.Services;
//using System.Configuration;
//using System.Text;
//namespace ServiceReport
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);
//            //builder.Services.AddDbContext<ServiceReportContext>(options =>
//           //     options.UseSqlServer(builder.Configuration.GetConnectionString("ServiceReportContext") ?? throw new InvalidOperationException("Connection string 'ServiceReportContext' not found.")));

//            builder.Services.AddDbContext<ServiceReportContext>(options =>
//         options.UseMySql(builder.Configuration.GetConnectionString("ServiceReportContext") ?? throw new InvalidOperationException("Connection string 'ServiceReportContext' not found."),
//         new MySqlServerVersion(new Version(8, 0, 25)))); // Adjust MySQL version accordingly



//            builder.Services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();
//            builder.Services.AddSingleton<ITokenService, ITokenService>();
//            // Register TokenService properly
//            builder.Services.AddScoped<ITokenService, TokenService>();  // <-- Correct registration

//            // Add services to the container.
//            builder.Services.AddControllersWithViews();
//            builder.Services.AddControllers();
//            builder.Services.AddSession();
//            builder.Services.AddHttpContextAccessor();
//            builder.Services.AddEndpointsApiExplorer();

//            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

//            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

//            builder.Services.AddAuthentication(x =>
//            {
//                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                //x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//            })
//            .AddCookie(options =>
//            {
//                options.LoginPath = "/Users/Login"; // Set your login path
//                options.LogoutPath = "/Users/Logout"; // Set your logout path
//            })
//            .AddJwtBearer(x =>
//            {
//                x.RequireHttpsMetadata = true;
//                x.SaveToken = true;
//                x.TokenValidationParameters = new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateIssuer = true,
//                    ValidateAudience = true,
//                    ValidIssuer = jwtSettings.Issuer,
//                    ValidAudience = jwtSettings.Audience,
//                    ClockSkew = TimeSpan.Zero
//                };
//            });


//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Home/Error");
//                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//                app.UseHsts();
//            }
//            app.UseSession();

//            app.Use(async (context, next) =>
//            {
//                var token = context.Session.GetString("Token");
//                if (!string.IsNullOrEmpty(token))
//                {
//                    context.Request.Headers.Add("Authorization", "Bearer " + token);
//                }
//                await next();
//            });

//            app.UseHttpsRedirection();
//            app.UseStaticFiles();

//            app.UseRouting();

//            app.UseAuthentication();
//            app.UseAuthorization();

//            app.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}");

//            app.Run();
//        }
//    }
//}
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServiceReport.Data;
using ServiceReport.Helper;
using ServiceReport.Interface;
using ServiceReport.Services;
using System.Configuration;
using System.Text;

namespace ServiceReport
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Register DbContext with MySQL
            builder.Services.AddDbContext<ServiceReportContext>(options =>
                options.UseMySql(builder.Configuration.GetConnectionString("ServiceReportContext")
                    ?? throw new InvalidOperationException("Connection string 'ServiceReportContext' not found."),
                    new MySqlServerVersion(new Version(8, 0, 25))));

            // Register services
            builder.Services.AddScoped<IJwtAuthenticationService, JwtAuthenticationService>();

            // Register TokenService properly
            builder.Services.AddScoped<ITokenService, TokenService>();  // <-- Correct registration

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddControllers();
            builder.Services.AddSession();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddEndpointsApiExplorer();

            // Get JWT settings from configuration
            var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

            var key = Encoding.ASCII.GetBytes(jwtSettings.SecretKey);

            // Configure authentication
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                // x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Optional
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Users/Login"; // Set your login path
                options.LogoutPath = "/Users/Logout"; // Set your logout path
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    ClockSkew = TimeSpan.Zero
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();

            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
