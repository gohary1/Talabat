using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Error;
using ProductApi.Helper;
using ProductApi.MiddleWares;
using ProductData;
using ProductData.Entites.Idintity;
using ProductRepository.Identity;
using ProductRepository.Interfaces;
using ProductRepository.Repositories;
using StackExchange.Redis;

namespace ProductApi.Extensions
{
    public static class ApplicationServices
    {
        public static IServiceCollection ApplicationServicesAdd(this IServiceCollection service)
        {
            
            service.AddScoped(typeof(IGenaricRepository<,>), typeof(GenaricRepository<,>));
            service.AddAutoMapper(typeof(MappingProfiles));
            service.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                         .Select(e => e.ErrorMessage)
                                                         .ToList();
                    var response = new ApiValidationErrorRes()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
                
            });
            service.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();
            return service;
        }
        public static WebApplication middleWheresProviderSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
        public static WebApplication middleWheresProvider(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleWare>();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            return app;
        }
    }
}
