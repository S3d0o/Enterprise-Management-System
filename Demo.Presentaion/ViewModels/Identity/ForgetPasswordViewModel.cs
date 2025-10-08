using System.ComponentModel.DataAnnotations;

namespace Demo.Presentaion.ViewModels.Identity
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = string.Empty;
    }
}
