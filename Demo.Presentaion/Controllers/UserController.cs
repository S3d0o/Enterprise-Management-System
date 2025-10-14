
using Demo.BusinessLogic.DTOS.UserDTOS;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.IdentityModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Demo.Presentaion.Controllers
{
    [Authorize]
    public class UserController(IUserService _userService, IWebHostEnvironment _env) : Controller
    {
        #region Index
        [HttpGet]
        public IActionResult Index(string? searchByEmail)
        {
            var users = _userService.GetAllUsers(searchByEmail);
            return View(users);
        }
        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();

            return View(user);
        }
        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();
            var UserEdit = new UserEditDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = user.Roles
            };

            return View(UserEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] string id, UserEditDto dto)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                    return NotFound();

                var updated = _userService.UpdateUser(id, dto);
                if (updated)
                    return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Failed to update user.");
            }
            catch (Exception ex)
            {
                var errorMsg = _env.IsDevelopment() ? ex.Message : "User cannot be updated.";
                ModelState.AddModelError(string.Empty, errorMsg);
            }

            return View(dto);
        }

        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            try
                {
                var user = _userService.GetUserById(id);
                if (user == null)
                    return NotFound();
                // Prevent deletion of admin users
                //if (user.Roles.Contains("Admin"))
                //{
                //    return BadRequest("Admin users cannot be deleted.");
                //}
                // Proceed with deletion
                var result = _userService.DeleteUser(id);
                if (result)
                    return RedirectToAction(nameof(Index));
                return BadRequest("Failed to delete user.");
            }
            catch (Exception ex)
            {
                var errorMsg = _env.IsDevelopment() ? ex.Message : "User cannot be deleted.";
                return RedirectToAction(nameof(Index));
            }
        }

        #endregion
    }
}
