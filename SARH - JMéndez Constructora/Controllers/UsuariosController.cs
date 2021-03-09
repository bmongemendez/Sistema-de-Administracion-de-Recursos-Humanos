using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SARH___JMéndez_Constructora.Models;
using SARH___JMéndez_Constructora.Models.AccountViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

      


        public UsuariosController( 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        public IActionResult Index(UsuariosMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == UsuariosMessageId.AddUserSuccess ? "Se ha agreado el usuario."
                : message == UsuariosMessageId.UpdateUserSuccess ? "Se ha actualizo el usuario."
                : message == UsuariosMessageId.DeleteUserSuccess ? "Se ha eliminado el usuario"
                : message == UsuariosMessageId.Error ? "Ha ocurrido un error."
                : "";
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

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var roleRsult = await _userManager.AddToRoleAsync(user, model.Role);
                    if (roleRsult.Succeeded)
                    {
                        //await _signInManager.SignInAsync(user, isPersistent: false);
                        _logger.LogInformation(3, "User created a new account with password.");
                        return RedirectToAction(nameof(Index), new { Message = UsuariosMessageId.AddUserSuccess });
                    }
                    return RedirectToAction(nameof(Index), new { Message = UsuariosMessageId.Error});
                }
                AddErrors(result);
            }

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
            // If we got this far, something failed, redisplay form
            //return RedirectToAction(nameof(UsuariosController.Index), "Usuarios");
            return View(model);
        }

        // GET: /Usuarios/ChangePassword
        [HttpGet]
        public IActionResult SetPassword(string id, string userName)
        {
            SetPasswordViewModel setPassword;
            if (_userManager.FindByIdAsync(id) != null)
            {
                setPassword = new SetPasswordViewModel
                {
                    Id = id,
                    UserName = userName
                };
                return View(setPassword);
            }
            else
            {
                return RedirectToAction(nameof(Index), new { Message = UsuariosMessageId.Error });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                var removeResult = await _userManager.RemovePasswordAsync(user); 
                var addResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
                if (removeResult.Succeeded && addResult.Succeeded)
                {
                    return RedirectToAction(nameof(UsuariosController.Index), "Usuarios", new { Message = UsuariosMessageId.UpdateUserSuccess });
                }
                AddErrors(removeResult);
                AddErrors(addResult);
                return View(model);
            }
            return RedirectToAction(nameof(UsuariosController.Index), "Usuarios", new { Message = UsuariosMessageId.Error });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string returnUrl)
        {
            string id = returnUrl;

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                //user.isDeleted = true;
                var result = await _userManager.DeleteAsync(user);
                //var result = await _userManager.UpdateAsync(user);

                
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UsuariosController.Index), "Usuarios", new { Message = UsuariosMessageId.DeleteUserSuccess });
                }
                AddErrors(result);
                
                return View(nameof(Index));
            }

            ApplicationUser usr = await GetCurrentUserAsync();

            if (usr.Id == id) {
                return RedirectToAction(nameof(AccountController.Login), "Account", new { Message = UsuariosMessageId.Error });
            }
            return RedirectToAction(nameof(UsuariosController.Index), "Usuarios", new { Message = UsuariosMessageId.Error });

        }

        #region Helpers
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(TableroController.Index), "Tablero");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public enum UsuariosMessageId
        {
            AddUserSuccess,
            Error,
            UpdateUserSuccess,
            DeleteUserSuccess
        }
        #endregion
    }
}
