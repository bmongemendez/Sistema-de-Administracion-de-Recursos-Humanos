using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SARH___JMéndez_Constructora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;

        public UsuariosController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<AspNetUsers> userList = new List<AspNetUsers>();
            foreach (var user in _userManager.Users)
            {
                userList.Add(new AspNetUsers
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    IsDeleted = user.isDeleted
                });
            }
            ViewBag.Users = userList;
            return View();
        }
    }
}
