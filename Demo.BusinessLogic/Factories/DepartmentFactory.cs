using Demo.BusinessLogic.DTOS.DepartmentDTOS;
using Demo.DataAccess.Models;

namespace Demo.BusinessLogic.Factories
{
    internal static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            return new DepartmentDto()
            {
                DeptId = department.Id,
                DeptCode = department.Code,
                DeptName = department.Name,
                DeptDescription = department.Description,
                DateOfCreation = department.CreatedAt.HasValue ? DateOnly.FromDateTime(department.CreatedAt.Value) : default
            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreatedBy = department.CreatedBy,
                ModifiedBy = department.ModifiedBy,
                CreatedAt = department.CreatedAt.HasValue ? DateOnly.FromDateTime(department.CreatedAt.Value) : default,
                ModifiedAt = department.ModifiedAt.HasValue ? DateOnly.FromDateTime(department.ModifiedAt.Value) : default,
                IsDeleted = department.IsDeleted
            };
        }

        public static Department ToEntity(this CreateDepartmentDto createDepartmentDto)
        {
            return new Department()
            {
                Name = createDepartmentDto.Name,
                Code = createDepartmentDto.Code,
                Description = createDepartmentDto.Description,
                CreatedAt = createDepartmentDto.CreatedAt.ToDateTime(new TimeOnly()) 
            };
        }
        public static Department ToEntity(this UpdatedDepartmentDto UpdateDepartmentDto)
        {
            return new Department()
            {
                Id = UpdateDepartmentDto.Id,
                Name = UpdateDepartmentDto.Name,
                Code = UpdateDepartmentDto.Code,
                Description = UpdateDepartmentDto.Description,
                CreatedAt = UpdateDepartmentDto.CreatedAt.ToDateTime(new TimeOnly()) 
            };
        }

    }
}
