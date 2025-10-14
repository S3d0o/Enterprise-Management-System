using System.ComponentModel.DataAnnotations;

namespace Demo.BusinessLogic.DTOS.UserDTOS
{
    public class UserEditDto
    {
        public string Id { get; set; } = string.Empty; // used to identify the record

        [MinLength(3,ErrorMessage ="Name must contain at least 3 chars"),MaxLength(15)]
        
        public string FirstName { get; set; } = string.Empty;

        [MinLength(3, ErrorMessage = "Name must contain at least 3 chars"), MaxLength(15)]
        public string LastName { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress,ErrorMessage ="this must be a valid Email")]
        public string Email { get; set; } = string.Empty;

        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
