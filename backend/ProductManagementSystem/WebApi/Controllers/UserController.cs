using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Core.Domain.Models.ViewModels;
using RepositoryAndServices.Service.CustomService.UserService;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
       // private readonly ILogger logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            this.userService = userService;
            //this.logger = logger;
        }

        [HttpGet("GetAllUsers")]

        public async Task<IActionResult> GetAllUsers()
        {
            var alluser = await userService.GetAllAsync();
            if (alluser != null)
            {
                return Ok(alluser);
            }
            else
            {
                return BadRequest("Unable to retrieve user");
            }
        }

        [HttpGet("GetUserById")]

        public async Task<IActionResult> GetUserById(int id)
        {
            if (id != 0)
            {
                var user = await userService.GetByIDAsync(id);
                if (user == null)
                {
                    return BadRequest("Unable to retrive requested user");
                }
                return Ok(user);
            }
            else
            {
                return BadRequest("Invalid Request...!");
            }
        }

        [HttpPut("UpdateUser")]

        public async Task<IActionResult> UpdateUser([FromForm] UserUpdateModel userUpdateModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var checkUserName = await userService.FindAsync(x => x.UserName == userUpdateModel.UserName);
            if (checkUserName != null)
                return BadRequest($"UserName: {userUpdateModel.UserName} already exists.");

            var result = await userService.UpdateAsync(userUpdateModel);
            if (result)
                return Ok("Updated Successfully");

            return BadRequest("Something Went Wrong");
        }

        [HttpDelete("DeleteUser")]

        public async Task<IActionResult> DeleteUser(int id)
        {
            var existingUserType = await userService.GetByIDAsync(id);

            if (existingUserType != null)
            {
                var success = await userService.DeleteAsync(id);

                if (success)
                {
                    return Ok("UserType deleted successfully.");
                }
                else
                {
                    return BadRequest("Failed to delete UserType.");
                }
            }
            return BadRequest("User not found.");
        }
    }
}
