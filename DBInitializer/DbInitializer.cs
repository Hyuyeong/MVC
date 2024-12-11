using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.Utility;

namespace MVC.DBInitializer;

public class DbInitializer : IDbInitializer
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _db;

    public DbInitializer(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext db
    )
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _db = db;
    }

    public void Initialize()
    {
        //migrations if they are not applied
        try
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }
        }
        catch (Exception ex) { }

        //create roles if they are not created
        if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();

            //if roles are not created, then we will create admin user as well
            _userManager
                .CreateAsync(
                    new ApplicationUser
                    {
                        UserName = "choibeny@gmail.com",
                        Email = "choibeny@gmail.com",
                        Name = "Hyuyeong Choe",
                        PhoneNumber = "027 353 4478",
                        StreetAddress = "169 Hobsonville Point Road",
                        State = "Auckland",
                        PostalCode = "0616",
                        City = "Auckland",
                    },
                    "Admin@@"
                )
                .GetAwaiter()
                .GetResult();

            ApplicationUser user = _db.applicationUsers.FirstOrDefault(u =>
                u.Email == "admin@dotnetmastery.com"
            );
            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
        }

        return;
    }
}
