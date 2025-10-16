
using Demo.BusinessLogic.DTOS.UserDTOS;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.IdentityModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace Demo.Presentaion.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public IActionResult Edit(string? id, [FromServices] RoleManager<ApplicationRole> _roleManager)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var user = _userService.GetUserById(id);
            if (user == null) return NotFound();

            var userEdit = new UserEditDto
            {
                Id = user.Id, // make sure ID is set
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = user.Roles.ToList()
            };

            // Get all roles
            var allRoles = _roleManager.Roles.Select(r => r.Name).ToList();

            ViewBag.AllRoles = allRoles.Select(r => new SelectListItem
            {
                Text = r,
                Value = r,
                Selected = userEdit.Roles.Contains(r) // <-- fix here
            }).ToList();

            return View(userEdit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, UserEditDto dto, [FromServices] RoleManager<ApplicationRole> roleManager)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            if (!ModelState.IsValid)
            {
                // Re-populate roles for dropdown in case of validation errors
                var allRoles = roleManager.Roles.Select(r => r.Name).ToList();
                ViewBag.AllRoles = allRoles.Select(r => new SelectListItem
                {
                    Text = r,
                    Value = r,
                    Selected = dto.Roles?.Contains(r) ?? false
                }).ToList();

                return View(dto);
            }

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

            // Re-populate roles for dropdown if update failed
            var roles = roleManager.Roles.Select(r => r.Name).ToList();
            ViewBag.AllRoles = roles.Select(r => new SelectListItem
            {
                Text = r,
                Value = r,
                Selected = dto.Roles?.Contains(r) ?? false
            }).ToList();

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
