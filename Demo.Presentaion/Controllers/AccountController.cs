using Demo.BusinessLogic.Services.EmailSender;
using Demo.DataAccess.Models.IdentityModule;
using Demo.DataAccess.Models.Shared;
using Demo.Presentaion.ViewModels.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace Demo.Presentaion.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : Controller
    {
        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
            var user = new ApplicationUser()
            {
                UserName = registerViewModel.UserName,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Email = registerViewModel.Email,
            };
            // Check if email or username already exists
            var isEmailExist = _userManager.FindByEmailAsync(registerViewModel.Email).Result;
            if (isEmailExist is not null)
            {
                ModelState.AddModelError(nameof(registerViewModel.Email), "This Email is already exist");
                return View(registerViewModel);
            }
            var isUserNameExist = _userManager.FindByNameAsync(registerViewModel.UserName).Result;
            if (isUserNameExist != null)
            {
                ModelState.AddModelError(nameof(registerViewModel.UserName), "This UserName already exists");
                return View(registerViewModel);
            }

            var result = _userManager.CreateAsync(user, registerViewModel.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(registerViewModel);
        }
        #endregion

        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel _loginViewModel)
        {
            if (!ModelState.IsValid) return View(_loginViewModel);
            // 1] Find user by email
            var user = _userManager.FindByEmailAsync(_loginViewModel.Email).Result;
            if (user is not null)
            {
                // 2] Check if the password is correct
                var flag = _userManager.CheckPasswordAsync(user, _loginViewModel.Password).Result;
                if (flag)
                {
                    // User is valid
                    // 3] Sign in the user
                    var result = _signInManager.PasswordSignInAsync(user, _loginViewModel.Password, _loginViewModel.RememberMe, false).Result;
                    if (result.IsNotAllowed) ModelState.AddModelError(string.Empty, "Your Account is not Allowed");
                    // 4] check if the user is locked out
                    if (result.IsLockedOut) ModelState.AddModelError(string.Empty, "Your Account is Locked");
                    // 5] Redirect to the home page
                    if (result.Succeeded) return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Login is invalid,your password or email is wrong"); // adding a nonspecifying error 
            return View(_loginViewModel);

        }
        #endregion

        #region TermService 
        public IActionResult TermService()
        {
            return View();
        }
        #endregion

        #region SignOut
        public new IActionResult SignOut()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login");
        }
        #endregion

        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendResetPasswordUrl(ForgetPasswordViewModel _forgetPasswordViewModel, [FromServices] IEmailSender _emailSender)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(_forgetPasswordViewModel.Email).Result;
                if (user is not null)
                {
                    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
                    var url = Url.Action("ResetPassword", "Account", new { email = user.Email, token = token }, Request.Scheme);
                    var email = new Email()
                    {
                        To = _forgetPasswordViewModel.Email,
                        Subject = "Reset Password",
                        Body = EmailTemplateBuilder.BuildResetPasswordEmail(user.Email!, url!) // helper method from the service
                    };

                    // send email
                    _emailSender.SendEmail(email);
                    return RedirectToAction("CheckYourInbox");
                }
                else
                    ModelState.AddModelError(nameof(_forgetPasswordViewModel.Email), "This Email is not found");

            }
            return View("ForgetPassword", _forgetPasswordViewModel);
        }

        [HttpGet]
        public IActionResult CheckYourInbox()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel _resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                if (email is null || token is null) return BadRequest();
                var user = _userManager.FindByEmailAsync(email).Result; // get user by email more safety if someone try to hack us
                if (user is not null)
                {
                    var result = _userManager.ResetPasswordAsync(user, token, _resetPasswordViewModel.NewPassword).Result;
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(_resetPasswordViewModel);
                }
            }
            ModelState.AddModelError("", "Invalid Operation, Please try again");
            return View(_resetPasswordViewModel);
        }


        #endregion

        #region Access Denied
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion

        #region SignOut
        [HttpPost]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login");
        }

        #endregion
    }
}
