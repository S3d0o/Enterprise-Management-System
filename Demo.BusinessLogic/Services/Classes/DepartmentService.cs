using Demo.BusinessLogic.DTOS.DepartmentDTOS;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Repositories.Classes;
using Demo.DataAccess.Data.Repositories.Repositories;
using Demo.DataAccess.Models;
using System.Threading.Channels;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IDepartmentRepository Departmentrepo) : IDepartmentService
    // Dependency Injection, Primar constructors for classes
    {
        // GET ALL => Id,Code,Name,Description,DateOfCreation [Date part only] 
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            // call the repo
            var departments = Departmentrepo.GetAll();
            return departments.Select(d => d.ToDepartmentDto() /* Extension Method Mapping */
            );
        }

        // GET BY ID 
        public DepartmentDetailsDto GetDepartmentById(int id)
        {
            var dept = Departmentrepo.GetById(id);
            return dept is null ? null : dept.ToDepartmentDetailsDto();// Extension Method Mapping

            // Mapping Types =>
            // AutoMapper => Package [Auto Mapper]
            //,Extension Method Mapping // good in readability 
            //, Constructor Mapping => bad in readability so will not use it
            //, Manual Mapping

            // Manual Mapping

        }

        // ADD
        public int AddDepartment(CreateDepartmentDto departmentDto) => Departmentrepo.Add(departmentDto.ToEntity());

        // UPDATE
        public int UpdateDepartment(UpdatedDepartmentDto updatedDepartmentDto)
        {
            return Departmentrepo.Update(updatedDepartmentDto.ToEntity());
        }
        // DELETE
        public bool DeleteDepartment(int id)
        {
            var dept = Departmentrepo.GetById(id);
            if (dept is null)
                return false;
            int NumOfRows = Departmentrepo.Delete(dept);
            return NumOfRows > 0; // no need for turnary operator !
        }
    }
}
