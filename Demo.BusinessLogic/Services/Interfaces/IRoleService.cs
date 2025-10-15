using Demo.BusinessLogic.DTOS.RoleDTOS;
using Demo.BusinessLogic.Enums;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<RoleDto> GetAllRoles(string? searchByName = null);
        public RoleEditDto GetRoleById(string? id);
        public bool UpdateRole(string id, RoleEditDto roleEditDto);
        public bool DeleteRole(string id);
        RoleCreateResult CreateRole(RoleCreateDto dto); // Return RoleCreateResult enum instead of bool to indicate specific failure reasons


    }
}
