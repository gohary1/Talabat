using Microsoft.AspNetCore.Identity;
using ProductData.Entites.Idintity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductRepository.Identity.DataSeed
{
    public class AccountDataSeed
    {

        public static async Task applyAccountSeeds(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Count() == 0)
            {
                var user= new AppUser()
                {
                    DisplayName="Gohary",
                    Email="abdelrahmanelgohary2020@gmail.com",
                    PhoneNumber="01155353197",
                    UserName= "abdelrahmanelgohary"
                };
                await userManager.CreateAsync(user,"Pa$$word12");
            }
        }
    }
}
