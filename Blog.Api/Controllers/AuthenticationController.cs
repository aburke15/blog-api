using System;
using System.Threading.Tasks;
using Blog.Api.Controllers.Requests;
using Blog.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Blog.Api.Controllers
{
    [Route("auth"), AllowAnonymous]
    public class AuthenticationController : BaseApiController
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthenticationController(
            ILogger<AuthenticationController> logger,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterNewUserRequest request)
        {
            const string failureMessage = "User registration failed.";

            var user = new User
            {
                UserName = request.Username,
                Email = request.Email
            };

            var result = await _userManager
                .CreateAsync(user, request.Password);

            if (result.Succeeded == false)
            {
                var dictionary = new ModelStateDictionary();

                foreach (IdentityError error in result.Errors)
                    dictionary.AddModelError(error.Code, error.Description);

                return new BadRequestObjectResult(new
                {
                    Message = failureMessage,
                    Errors = dictionary
                });
            }

            return Ok(new { Message = "User registration successful." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest request)
        {
            var result = await _signInManager
                .PasswordSignInAsync(
                    request.Username,
                    request.Password,
                    request.RememberMe,
                    lockoutOnFailure: false
                );

            if (result.Succeeded == false)
            {
                var dictionary = new ModelStateDictionary();

                dictionary.AddModelError("Error", "Invalid username or password.");

                return new BadRequestObjectResult(new
                {
                    Message = "Login attempt failed.",
                    Errors = dictionary
                });
            }

            return Ok(new { Message = "Login successful." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { Message = "Logout successful." });
        }
    }
}