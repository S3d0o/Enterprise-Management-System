using System.ComponentModel.DataAnnotations;

namespace Demo.Presentaion.ViewModels.Identity
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; } = false;
    }
}
