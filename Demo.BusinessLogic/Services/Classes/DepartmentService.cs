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
        public IEnumerable<DepartmentDto> GetAllDepartments(string? searchName = null)
        {
            var deptRepo = _unitOfWork.DepartmentRepository;

            var departments = deptRepo.GetAll();

            if (!string.IsNullOrWhiteSpace(searchName))
                departments = departments
                    .Where(d => d.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase));

            return departments.Select(d => d.ToDepartmentDto());
        }


        // GET BY ID 
        public DepartmentDetailsDto GetDepartmentById(int id)
        {
            var dept = _unitOfWork.DepartmentRepository.GetById(id);
            return dept is null ? null : dept.ToDepartmentDetailsDto();// Extension Method Mapping
        }

        // ADD
        public int AddDepartment(CreateDepartmentDto departmentDto)
        {
            var entity = departmentDto.ToEntity();
            entity.CreatedAt = DateTime.Now;
            entity.CreatedById = departmentDto.CreatedById;

            _unitOfWork.DepartmentRepository.Add(entity);
            return _unitOfWork.SaveChanges();
        }
        // UPDATE
        public int UpdateDepartment(UpdatedDepartmentDto updatedDepartmentDto)
        {
            // Fetch the existing department from DB (tracked by EF)
            var existingDept = _unitOfWork.DepartmentRepository.GetById(updatedDepartmentDto.Id);
            if (existingDept is null)
                return 0;

            // Update only the modifiable fields
            existingDept.Name = updatedDepartmentDto.Name;
            existingDept.Code = updatedDepartmentDto.Code;
            existingDept.Description = updatedDepartmentDto.Description;
            existingDept.ModifiedAt = DateTime.Now;
            existingDept.ModifiedById = updatedDepartmentDto.ModifiedById;

            _unitOfWork.DepartmentRepository.Update(existingDept);
            return _unitOfWork.SaveChanges();
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
