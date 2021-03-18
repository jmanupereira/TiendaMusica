using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TiendaMusica.Data
{
    public class ContextSeed
    {
        public static async Task SeedRolesAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(IdentityEnums.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(IdentityEnums.Roles.Empleado.ToString()));
            await roleManager.CreateAsync(new IdentityRole(IdentityEnums.Roles.Basico.ToString()));
        }
    }
}