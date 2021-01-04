using GameScorecardsDataAccess;
using GameScorecardsDataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GameScorecardsAPI
{
    public interface IDbInitializer
    {
        void Initialize();
    }

    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext m_DB;
        private readonly UserManager<ApplicationUser> m_UserManager;
        private readonly RoleManager<IdentityRole> m_RoleManager;

        public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            m_DB = db;
            m_UserManager = userManager;
            m_RoleManager = roleManager;
        }

        public void Initialize()
        {
            try
            {
                if (m_DB.Database.GetPendingMigrations().Any())
                {
                    m_DB.Database.Migrate();
                }
            }
            catch (Exception)
            {
                // TODO: Log
                throw;
            }

            //if (_db.Roles.Any(x => x.Name == SD.Role_Admin)) return;

            //_roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            //_roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            //_roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();

            //_userManager.CreateAsync(new IdentityUser
            //{
            //    UserName = "admin@gmail.com",
            //    Email = "admin@gmail.com",
            //    EmailConfirmed = true
            //}, "Admin123*").GetAwaiter().GetResult();

            //IdentityUser user = m_DB.Users.FirstOrDefault(u => u.Email == "admin@gmail.com");
            //_userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
        }
    }
}
