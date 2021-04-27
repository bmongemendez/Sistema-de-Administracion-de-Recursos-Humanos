using SARH___JMéndez_Constructora.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Models
{
    public static class IdentitySeed
    {
        public static void SeedRoles(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            IdentityResult roleResult;
            //Seed Roles
            roleResult = roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString())).Result;
            roleResult = roleManager.CreateAsync(new IdentityRole(Roles.Gerencia.ToString())).Result;
            roleResult = roleManager.CreateAsync(new IdentityRole(Roles.Auditoria.ToString())).Result;
            roleResult = roleManager.CreateAsync(new IdentityRole(Roles.RRHH.ToString())).Result;
            
        }

        public static void SeedUsersWithRoles(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser { UserName = "bmonge70449" };

            if (userManager.Users.Where(u => u.UserName == defaultUser.UserName).SingleOrDefault() == null) 
            {
                IdentityResult result;
                result = userManager.CreateAsync(defaultUser, "$tr1ng123Pa$$word.").Result;
                if (result.Succeeded)
                { 
                      userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString()); 
                }
                
            }

            // Seed RRHH User
            var rrhhUser = new ApplicationUser { UserName = "bleal70466" };

            if (userManager.Users.Where(u => u.UserName == rrhhUser.UserName).SingleOrDefault() == null)
            {
                IdentityResult result;
                result = userManager.CreateAsync(rrhhUser, "$tr1ng123Pa$$word.").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(rrhhUser, Roles.RRHH.ToString());
                }

            }
        }
    }

    public enum Roles
    {
        Admin,
        Gerencia,
        Auditoria,
        RRHH
    }
}
