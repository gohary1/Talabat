using Microsoft.Extensions.Logging;
using ProductData;
using ProductData.Entites;
using ProductData.Entites.orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductRepository
{
    public class StoreContentSeed
    {
        public static async Task seedData(StoreDpContext context,ILogger logger)
        {
            var brands = File.ReadAllText("../ProductRepository/SeedData/brands.json");
            var productBrands = JsonSerializer.Deserialize<List<ProductBrand>>(brands);
            try
            {
                if(productBrands != null && !context.Brands.Any())
                {
                    foreach(var productBrand in productBrands)
                    {
                        await context.Brands.AddAsync(productBrand);                      
                    }
                        await context.SaveChangesAsync();
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
            }
            
            //
            var type = File.ReadAllText("../ProductRepository/SeedData/types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(type);
            try
            {
                if (types != null && !context.Types.Any())
                {
                    foreach (var typeProduct in types)
                    {
                        await context.Types.AddAsync(typeProduct);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            //
            var product = File.ReadAllText("../ProductRepository/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(product);
            try
            {
                if (products != null && !context.Products.Any())
                {
                    foreach (var product1 in products)
                    {
                        await context.Products.AddAsync(product1);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
            var delevary = File.ReadAllText("../ProductRepository/SeedData/delivery.json");
            var delvs = JsonSerializer.Deserialize<List<DeliveryMethod>>(delevary);
            try
            {
                if(context.DeliveryMethod!=null&&!context.DeliveryMethod.Any())
                if (delvs.Count()>0)
                {
                    foreach (var product1 in delvs)
                    {
                        context.Set<DeliveryMethod>().Add(product1);
                    }
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}
