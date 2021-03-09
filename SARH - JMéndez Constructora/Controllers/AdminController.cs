
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SARH___JMéndez_Constructora.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SARH___JMéndez_Constructora.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController (UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: AdminController
        public IActionResult IndexAsync()
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
            return View(userList);
        }

        [HttpGet]
        public async Task<IActionResult>Edit(string id)
        {
            //ApplicationUser user = await _userManager.FindByIdAsync(id);
            ApplicationUser tempUser = _userManager.Users
                .SingleOrDefault(u => u.Id == id);

            AspNetUsers user = new AspNetUsers
            {
                Id = tempUser.Id,
                IsDeleted = tempUser.isDeleted,
                UserName = tempUser.UserName,
            };
            user.Roles = await _userManager.GetRolesAsync(tempUser);

            if (user != null) return View(user);
            else return RedirectToAction("Index");
        }

        //[HttpPost]
        //public async Task<IActionResult> Update(AspNetUsers user)
        //{
        //    ApplicationUser userAux = await _userManager.FindByIdAsync(user.Id);
        //    if (user != null)
        //    {
        //        if (!string.IsNullOrEmpty(user.PasswordHashpassword))
        //            user.PasswordHash = passwordHasher.HashPassword(user, password);
        //        else
        //            ModelState.AddModelError("", "Password cannot be empty");

        //        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        //        {
        //            IdentityResult result = await userManager.UpdateAsync(user);
        //            if (result.Succeeded)
        //                return RedirectToAction("Index");
        //            else
        //                Errors(result);
        //        }
        //    }
        //    else
        //        ModelState.AddModelError("", "User Not Found");
        //    return View(user);
        //}
        //

        // GET: /Manage/ChangePassword
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
                return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
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
                    return RedirectToAction(nameof(UsuariosController.Index), "Usuarios", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(removeResult);
                AddErrors(addResult);
                return View(model);
            }
            return RedirectToAction(nameof(UsuariosController.Index), "Usuarios", new { Message = ManageMessageId.SetPasswordSuccess });
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
                    return RedirectToAction(nameof(UsuariosController.Index), "Usuarios", new { Message = ManageMessageId.Error });
                }
                AddErrors(result);
                
                return View(nameof(Index));
            }

            ApplicationUser usr = await GetCurrentUserAsync();

            if (usr.Id == id) {
                return RedirectToAction(nameof(AccountController.Login), "Account", new { Message = ManageMessageId.Error });
            }
            return RedirectToAction(nameof(UsuariosController.Index), "Usuarios", new { Message = ManageMessageId.Error });

        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }
    }
}
