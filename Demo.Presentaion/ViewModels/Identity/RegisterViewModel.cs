using System.ComponentModel.DataAnnotations;

namespace Demo.Presentaion.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "User Name Cannot be empty")]
        [MaxLength(50)]
        [MinLength(3,ErrorMessage ="user name must contain at least 3 characters")]
        [Display(Name ="User Name")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "First Name Name Cannot be empty")]
        [MaxLength(50)]
        [MinLength(3, ErrorMessage = "name must contain at least 3 characters")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Last Name Name Cannot be empty")]
        [MaxLength(50)]
        [MinLength(3, ErrorMessage = "name must contain at least 3 characters")]
        public string LastName { get; set; } = string.Empty;
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and Confirm Password do not match.")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "You must agree to the terms.")]
        public bool IsAgree { get; set; }

    }
}
