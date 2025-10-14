using Demo.BusinessLogic.DTOS.UserDTOS;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUsers(string? searchName = null);
        UserDetailsDto? GetUserById(string id);
        public bool UpdateUser(string id, UserEditDto userEditDto);

        public bool DeleteUser(string id);



    }
}
