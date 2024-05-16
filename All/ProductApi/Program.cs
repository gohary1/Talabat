using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductApi.Error;
using ProductApi.Extensions;
using ProductApi.Helper;
using ProductApi.MiddleWares;
using ProductService.Interface;
using ProductData.Entites;
using ProductRepository;
using ProductRepository.Identity;
using ProductRepository.Interfaces;
using ProductRepository.Repositories;
using StackExchange.Redis;
using ProductService.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ProductApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<StoreDpContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Identity"));
            });
            builder.Services.AddSingleton<IConnectionMultiplexer>(options =>
            {
                var connectionString =builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connectionString);
            });
            builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAud"],
                    ValidateLifetime = true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:AuthKey"]))
                };
            });
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();   
            builder.Services.AddScoped<IAuthService, AuthService>();   
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();   
            builder.Services.AddScoped<IOrderService, OrderService>();   
            builder.Services.ApplicationServicesAdd();
            var app = builder.Build();
            
            await ApplySeeding.ApplySeedingAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.middleWheresProviderSwagger();
            }
            app.middleWheresProvider();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Run();

            
        }
    }
}
