using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProductData;
using ProductData.Entites.Idintity;
using ProductRepository;
using ProductRepository.Identity;
using ProductRepository.Identity.DataSeed;

namespace ProductApi.Helper
{
    public class ApplySeeding 
    {

     public static async Task ApplySeedingAsync(WebApplication app)
    {
        var scope = app.Services.CreateScope(); 
        var services = scope.ServiceProvider;

        var logger = services.GetRequiredService<ILogger<StoreContentSeed>>();
            var manager = services.GetRequiredService<UserManager<AppUser>>();
        try
        {
            var _dataBase = services.GetRequiredService<AppIdentityDbContext>();
            var context = services.GetRequiredService<StoreDpContext>();
                await context.Database.MigrateAsync();   
                await _dataBase.Database.MigrateAsync();   
            await StoreContentSeed.seedData(context, logger);
            await AccountDataSeed.applyAccountSeeds(manager);
        }
        catch (Exception ex)
        {
           logger.LogError(ex.Message);
        }
        
    }

    }  
}
