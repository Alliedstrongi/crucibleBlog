using crucibleBlog.Models;
using crucibleBlog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace crucibleBlog.Data


{
    public static class DataUtility
    {
        private const string? _adminRole = "Admin";
        private const string? _modRole = "Mod";

        public static string GetConnectionString(IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");
            string? databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            return string.IsNullOrEmpty(databaseUrl) ? connectionString! : BuildConnectionString(databaseUrl);
            //if (string.IsNullOrEmpty(databaseUrl))
            //{
            //    return connectionString;
            //}
            //else
            //{
            //    return BuildConnectionString(databaseUrl);
            //}
        }
        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');
            var builder = new NpgsqlConnectionStringBuilder()
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };
            return builder.ToString();
        }

        public static async Task ManageDataAsync(IServiceProvider svcProvider)
        {
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();
            var userManagerSvc = svcProvider.GetRequiredService<UserManager<BlogUser>>();
            var roleManagerSvc = svcProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var configurationSvc = svcProvider.GetRequiredService<IConfiguration>();

            await dbContextSvc.Database.MigrateAsync();

            // Seed Application Roles
            await SeedRolesAsync(roleManagerSvc);

            await SeedDemoUserAsync(userManagerSvc, configurationSvc);

        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync(_adminRole!))
            {
                await roleManager.CreateAsync(new IdentityRole(_adminRole!));
            }

            if (!await roleManager.RoleExistsAsync(_modRole!))
            {
                await roleManager.CreateAsync(new IdentityRole(_modRole!));
            }
        }

        private static async Task SeedDemoUserAsync(UserManager<BlogUser> userManager, IConfiguration configuration)
        {
            string? adminEmail = configuration["adminLoginEmail"] ?? Environment.GetEnvironmentVariable("adminLoginEmail");
            string? adminPassword = configuration["adminLoginPassword"] ?? Environment.GetEnvironmentVariable("adminLoginPassword");

            string? modEmail = configuration["modLoginEmail"] ?? Environment.GetEnvironmentVariable("modLoginEmail");
            string? modPassword = configuration["modLoginPassword"] ?? Environment.GetEnvironmentVariable("modLoginPassword");

            try
            {
				BlogUser? adminUser = new BlogUser()
				{
					UserName = adminEmail,
					Email = adminEmail,
					FirstName = "Dylan",
					LastName = "Taylor",
					EmailConfirmed = true,
				};
				BlogUser? user = await userManager.FindByEmailAsync(adminUser.Email!);

				if (user == null)
				{
					await userManager.CreateAsync(adminUser, adminPassword!);
					await userManager.AddToRoleAsync(adminUser, _adminRole!);
				}

				BlogUser? moderatorUser = new BlogUser()
				{
					UserName = modEmail,
					Email = modEmail,
					FirstName = "Antonio",
					LastName = "Raynor",
					EmailConfirmed = true,
				};
				BlogUser? modUser = await userManager.FindByEmailAsync(moderatorUser.Email!);

				if (modUser == null)
				{
					await userManager.CreateAsync(moderatorUser, modPassword!);
					await userManager.AddToRoleAsync(moderatorUser, _modRole!);
				}


			}
                catch (Exception ex)
                {
                    Console.WriteLine("*************  ERROR  *************");
                    Console.WriteLine("Error Seeding Demo Login User.");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("***********************************");
                    throw;
                }


            }
        }
    }

