using Demo.BusinessLogic.DTOS.RoleDTOS;
using Demo.BusinessLogic.Enums;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;

namespace Demo.BusinessLogic.Services.Classes
{
    public class RoleService(RoleManager<ApplicationRole> _roleManager, UserManager<ApplicationUser> _userManager) : IRoleService
    {
        public IEnumerable<RoleDto> GetAllRoles(string? searchByName = null)
        {
            var rolesQuery = _roleManager.Roles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchByName))
                rolesQuery = rolesQuery.Where(r => r.Name!.ToLower().Contains(searchByName.ToLower()));

            var roles = rolesQuery.ToList(); // Close DB reader

            var roleDtos = new List<RoleDto>();

            foreach (var role in roles)
            {
                var usersInRole = _userManager.GetUsersInRoleAsync(role.Name!).Result;

                roleDtos.Add(new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name!,
                    Description = role.Description,
                    UserCount = usersInRole.Count
                });
            }

            return roleDtos;
        }

        public RoleEditDto GetRoleById(string? id)
        {
            if (string.IsNullOrEmpty(id)) return null!;
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null) return null!;
            var roleEditDto = new RoleEditDto
            {
                Name = role.Name!,
                Description = role.NormalizedName ?? string.Empty,
            };
            return roleEditDto;
        }
        public bool UpdateRole(string id, RoleEditDto roleEditDto)
        {
            if (id != roleEditDto.Id) return false;
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null) return false;
            role.Name = roleEditDto.Name;
            role.Description = roleEditDto.Description;
            var result = _roleManager.UpdateAsync(role).Result;
            return result.Succeeded;
        }
        public bool DeleteRole(string id)
        {
            var role = _roleManager.FindByIdAsync(id).Result;
            if (role == null) return false;
            var usersInRole = _userManager.GetUsersInRoleAsync(role.Name!).Result;
            //if (usersInRole.Any()) return false; //should be enhance with the (service result pattern)  Prevent deletion if users are assigned to the role 
            var result = _roleManager.DeleteAsync(role).Result;
            return result.Succeeded;
        }

        public RoleCreateResult CreateRole(RoleCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) return RoleCreateResult.Failed;
            var existingRole = _roleManager.FindByNameAsync(dto.Name).Result;
            if (existingRole != null) return RoleCreateResult.AlreadyExists; // Role already exists
            var newRole = new ApplicationRole
            {
                Name = dto.Name,
                Description = dto.Description
            };
            var result = _roleManager.CreateAsync(newRole).Result;
            return result.Succeeded ? RoleCreateResult.Success : RoleCreateResult.Failed;
        }
    }
}
