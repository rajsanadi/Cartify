using Domain.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Core.Domain.Models.ViewModels;
using RepositoryAndServices.Common;
using RepositoryAndServices.Service.CustomService.UserService;
using WebApi.Auth;

namespace WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> logger;
        private readonly IUserService userService;
        private readonly IJWTAuthorization jWTAuthorization;
        private readonly IWebHostEnvironment webHostEnvironment;

        public LoginController(ILogger<LoginController> logger, IUserService userService, IJWTAuthorization jWTAuthorization, IWebHostEnvironment webHostEnvironment)
        {
            this.logger = logger;
            this.userService = userService;
            this.jWTAuthorization = jWTAuthorization;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("RegisterUser")]

        public async Task<IActionResult> RegisterUser([FromForm] UserInsertModel userInsertModel)
        {
            logger.LogInformation("Registering supplier with UserName: {UserName}", userInsertModel.UserName);

            if (!ModelState.IsValid)
            {
                logger.LogWarning("Invalid model state for UserName: {UserName}", userInsertModel.UserName);
                return BadRequest(ModelState);
            }

            var checkUserName = await userService.FindAsync(x => x.UserName == userInsertModel.UserName);
            if (checkUserName != null)
            {
                logger.LogWarning("UserName: {UserName} already exists.", userInsertModel.UserName);
                return BadRequest($"UserName: {userInsertModel.UserName} already exists.");
            }

            var result = await userService.InsertAsync(userInsertModel);
            if (result)
            {
                logger.LogInformation("Successfully registered supplier with UserName: {UserName}", userInsertModel.UserName);
                return Ok("Registered Successfully");
            }


            logger.LogError("Failed to register supplier with UserName: {UserName}", userInsertModel.UserName);
            return BadRequest("Failed to register supplier.");
        }

        [HttpPost("LoginUser")]

        public async Task<IActionResult> LoginUser([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new Response<string> { Status = 400, Message = "Invalid model state." });

            var user = await userService.FindAsync(x => x.UserName == loginModel.UserName && x.UserPassword == Encrypter.EncryptString(loginModel.UserPassword));

            if (user == null)
                return Unauthorized(new Response<string> { Status = 401, Message = "Invalid username or password." });

            var token = jWTAuthorization.GenerateJWT();

            return Ok(new Response<string>
            {
                Status = 200,
                Message = "Login successful.",
                Data = token

            });
        }
    }
}
