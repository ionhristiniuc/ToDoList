using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Core.Dto;
using ToDoList.API.Core.Interfaces.Services;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserRegistrationService userRegistrationService;
        private readonly IUserLoginService userLoginService;

        public AccountController(IUserRegistrationService userRegistrationService, IUserLoginService userLoginService)
        {
            this.userRegistrationService = userRegistrationService;
            this.userLoginService = userLoginService;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var loginResult = await userLoginService.LoginAsync(loginRequest);
            if (!loginResult.Success)
            {
                return Unauthorized(new LoginResult
                {
                    Success = false,
                    Message = "Invalid Email or Password."
                });
            }
            
            return Ok(new LoginResult
            {
                Success = true,
                Message = "Login successful",
                Token = loginResult.Value
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationRequest userRegistration)
        {
            var userResult = await userRegistrationService.RegisterUserAsync(userRegistration);
            return !userResult.Success ? new BadRequestObjectResult(userResult) : StatusCode(201);
        }
    }
}
