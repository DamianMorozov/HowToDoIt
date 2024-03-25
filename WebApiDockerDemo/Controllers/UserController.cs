// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebApiDockerDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUsersService usersService) : ControllerBase
{
	[HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
	{
        var users = await usersService.GetAllUsers();
        return users.Any() ? Ok(users) : NotFound();
	}
}