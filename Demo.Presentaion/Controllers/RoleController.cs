using Demo.BusinessLogic.DTOS.RoleDTOS;
using Demo.BusinessLogic.Enums;
using Demo.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentaion.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController(IRoleService _roleService, IWebHostEnvironment _env) : Controller
    {
        #region Index
        [HttpGet]
        public IActionResult Index(string? searchRole)
        {
            var roles = _roleService.GetAllRoles(searchRole);
            return View(roles);
        }
        #endregion

        #region Edit 

        [HttpGet]
        public IActionResult Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            var role = _roleService.GetRoleById(id);
            if (role is null) return NotFound();

            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, RoleEditDto roleEditDto)
        {
            if (!ModelState.IsValid) return View(roleEditDto);
            if (id != roleEditDto.Id) return BadRequest();
            var errorMsg = string.Empty;
            try
            {
                var role = _roleService.GetRoleById(id);
                if (role is null) return NotFound();
                if (!_roleService.UpdateRole(id, roleEditDto))
                    return View(roleEditDto);
                else
                    errorMsg = "Failed to update role.";
            }
            catch (Exception ex)
            {
                errorMsg = _env.IsDevelopment() ? ex.Message : "Role cannot be updated.";
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Delete

        public IActionResult Delete(string? id)
        {
            var role = _roleService.GetRoleById(id);
            if (role is null) return NotFound();
            string errorMsg = string.Empty;
            try
            {
                var result = _roleService.DeleteRole(id!);
                if (result)
                    return RedirectToAction(nameof(Index));
                else
                    errorMsg = "Failed to delete role.";
            }
            catch (Exception ex)
            {
                errorMsg = _env.IsDevelopment() ? ex.Message : "Role cannot be deleted.";
            }

            return BadRequest("Deleting roles is not allowed.");
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleCreateDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            try
            {
                var result = _roleService.CreateRole(dto);

                switch (result)
                {
                    case RoleCreateResult.Success:
                        {
                            TempData["message"] = $"Role {dto.Name} Created Succefully";
                            return RedirectToAction(nameof(Index));
                        }
                    case RoleCreateResult.AlreadyExists:
                        ModelState.AddModelError(nameof(dto.Name), "A role with this name already exists.");
                        break;

                    default:
                        ModelState.AddModelError(string.Empty, "An error occurred while creating the role.");
                        break;
                }
            }
            catch (Exception ex)
            {
                var msg = _env.IsDevelopment() ? ex.Message : "Unexpected error while creating role.";
                ModelState.AddModelError(string.Empty, msg);
            }

            return View(dto);
        }


        #endregion
    }
}
