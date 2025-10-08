using System.ComponentModel.DataAnnotations;

namespace Demo.Presentaion.ViewModels.Identity
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare(nameof(NewPassword), ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
