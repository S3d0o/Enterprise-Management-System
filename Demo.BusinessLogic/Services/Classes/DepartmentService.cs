using Demo.BusinessLogic.DTOS.DepartmentDTOS;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Data.Repositories.Classes;
using Demo.DataAccess.Data.Repositories.Interfaces;
using Demo.DataAccess.Data.Repositories.Repositories;
using Demo.DataAccess.Models;
using System.Threading.Channels;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
    // Dependency Injection, Primar constructors for classes
    {
        // GET ALL => Id,Code,Name,Description,DateOfCreation [Date part only] 
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            // call the repo
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return departments.Select(d => d.ToDepartmentDto() /* Extension Method Mapping */
            );
        }

        // GET BY ID 
        public DepartmentDetailsDto GetDepartmentById(int id)
        {
            var dept = _unitOfWork.DepartmentRepository.GetById(id);
            return dept is null ? null : dept.ToDepartmentDetailsDto();// Extension Method Mapping

            // Mapping Types =>
            // AutoMapper => Package [Auto Mapper]
            //,Extension Method Mapping // good in readability 
            //, Constructor Mapping => bad in readability so will not use it
            //, Manual Mapping

            // Manual Mapping

        }

        // ADD
        public int AddDepartment(CreateDepartmentDto departmentDto)
        {
            _unitOfWork.DepartmentRepository.Add(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges(); // return the number of affected rows
        }
        // UPDATE
        public int UpdateDepartment(UpdatedDepartmentDto updatedDepartmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(updatedDepartmentDto.ToEntity());
            return _unitOfWork.SaveChanges(); // return the number of affected rows
        }
        // DELETE
        public bool DeleteDepartment(int id)
        {
            var dept = _unitOfWork.DepartmentRepository.GetById(id);
            if (dept is null)
                return false;
             _unitOfWork.DepartmentRepository.Delete(dept);
           return _unitOfWork.SaveChanges() > 0; // return true if the number of affected rows > 0
        }
    }
}
