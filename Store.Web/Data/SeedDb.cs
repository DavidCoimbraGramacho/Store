using Microsoft.AspNetCore.Identity;
using Store.Web.Data.Entities;
using Store.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context,IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Admin");
            await this.userHelper.CheckRoleAsync("Customer");

            var user = await this.userHelper.GetUserByEmailAsync("davidfgramacho@portugalmail.pt");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "David",
                    LastName = "Gramacho",
                    Email = "davidfgramacho@portugalmail.pt",
                    UserName = "davidfgramacho@portugalmail.pt",
                    PhoneNumber = "217538964"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }
                

            if (!this.context.Products.Any())
            {
                this.AddProduct("Equipamento oficial SCP", user);
                this.AddProduct("Chuteiras oficiais", user);
                this.AddProduct("Leão pequena", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Dados.Entities.Product
            {
                Name = name,
                Price = this.random.Next(200),
                IsAvailable = true,
                Stock = this.random.Next(100),
                User = user
            });
        }
    }
}
