using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductManagement.DataAccess.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DataContext _db;

        public DbInitializer(DataContext db)
        {
            _db = db;
        }


        public async void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }

            var roleStore = new RoleStore<IdentityRole>(_db);
            if (!_db.Roles.Any(r => r.Name == "admin"))
            {
                await roleStore.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "admin" });
            }

            var user = new AppUser
            {
                UserName = "admin@test.com",
                NormalizedUserName = "admin@test.com",
                Email = "admin@test.com",
                NormalizedEmail = "admin@test.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                DisplayName = "Pankaj Thakur"
            };



            if (!_db.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(user, "Pa$$word12");
                user.PasswordHash = hashed;
                var userStore = new UserStore<AppUser>(_db);
                await userStore.CreateAsync(user);
                await userStore.AddToRoleAsync(user, "admin");
            }
            if (!_db.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        Name = "Books",
                        Products = new List<Product>
                        {
                            new Product
                            {
                                Title = "Sherlock Holmes",
                                Description = "Detective story",
                                Author = "Pankaj",
                                Price = 200,
                                CategoryId = 1
                            },
                            new Product
                            {
                                Title = "The Big Bang",
                                Description = "Ski fi",
                                Author = "Pankaj",
                                Price = 210,
                                CategoryId = 1
                            },
                            new Product
                            {
                                Title = "Love story 1920",
                                Description = "Love story",
                                Author = "Pankaj",
                                Price = 190,
                                CategoryId = 1
                            }
                        }
                    },
                    new Category
                    {
                        Name = "Mobile",
                        Products = new List<Product>
                        {
                            new Product
                            {
                                Title = "Samsung",
                                Description = "T series",
                                Author = "Pankaj",
                                Price = 2000,
                                CategoryId = 2
                            },
                            new Product
                            {
                                Title = "Nokia",
                                Description = "M series",
                                Author = "Pankaj",
                                Price = 2100,
                                CategoryId = 2
                            },
                            new Product
                            {
                                Title = "IPhone",
                                Description = "IPhone 5",
                                Author = "Pankaj",
                                Price = 19000,
                                CategoryId = 2
                            }
                        }
                    }

                };

                 await _db.Categories.AddRangeAsync(categories);
                 await _db.SaveChangesAsync();
            }

            

            await _db.SaveChangesAsync();
        }
    }
}
