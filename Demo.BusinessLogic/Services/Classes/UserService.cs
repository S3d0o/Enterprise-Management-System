using Demo.BusinessLogic.DTOS.UserDTOS;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;

namespace Demo.BusinessLogic.Services.Classes
{
    public class UserService(UserManager<ApplicationUser> _userManager) : IUserService
    // no need for repository because we will use UserManager to manage users and roles ( avoid duplicates )
    {
        public IEnumerable<UserDto> GetAllUsers(string? searchByEmail = null)
        {
            var usersQuery = _userManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchByEmail))
                usersQuery = usersQuery.Where(u => u.Email.ToLower().Contains(searchByEmail.ToLower()));
            var users = usersQuery.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
            }).ToList();
            foreach (var user in users)
            {
                var appUser = _userManager.FindByIdAsync(user.Id).Result;
                if (appUser != null)
                    user.Roles = _userManager.GetRolesAsync(appUser).Result;
            }
            return users;

        }

        public UserDetailsDto? GetUserById(string? id)
        {
            if (string.IsNullOrEmpty(id))  return null;
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null)  return null;
            var userDetails = new UserDetailsDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Roles = _userManager.GetRolesAsync(user).Result
            };

            return userDetails;

        }

        public bool UpdateUser(string id, UserEditDto userEditDto)
        {
            if (id != userEditDto.Id) return false;
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null) return false;
            user.FirstName = userEditDto.FirstName;
            user.LastName = userEditDto.LastName;
            user.Email = userEditDto.Email;
            var result = _userManager.UpdateAsync(user).Result;
            if (!result.Succeeded) return false;
            var currentRoles = _userManager.GetRolesAsync(user).Result;

            var rolesToAdd = userEditDto.Roles.Except(currentRoles).ToList();
            var rolesToRemove = currentRoles.Except(userEditDto.Roles).ToList();

            if (rolesToAdd.Any())
            {
                var addResult = _userManager.AddToRolesAsync(user, rolesToAdd).Result;
                if (!addResult.Succeeded) return false;
            }

            if (rolesToRemove.Any())
            {
                var removeResult = _userManager.RemoveFromRolesAsync(user, rolesToRemove).Result;
                if (!removeResult.Succeeded) return false;
            }

            return true;
        }

        public bool DeleteUser(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;
            if (user == null) return false;
            var result = _userManager.DeleteAsync(user).Result;
            return result.Succeeded;
        }
    }
}
