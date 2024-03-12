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