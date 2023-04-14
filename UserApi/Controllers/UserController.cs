using Microsoft.AspNetCore.Mvc;
using UserApi.Services;
using UserApi.Dtos;

namespace UserApi.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LoginUser(LoginUser loginUser)
    {
        return Ok(await _userService.GetOrCreateUser(loginUser.Id, loginUser.RefreshToken));
    }

    [HttpPut("/update")]
    public async Task<IActionResult> UpdateUser(UpdateUser updateUser)
    {
        var user = await _userService.UpdateUser(updateUser.UserId, updateUser.Settings);

        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpDelete("/delete")]
    public async Task<IActionResult> DeleteUser(DeleteUser deleteUser)
    {
        var successful = await _userService.DeleteUserById(deleteUser.UserId);
        return successful ? NoContent() : NotFound();
    }
}