namespace WebApiDockerDemo.Services;

public interface IUsersService
{
	public Task<IEnumerable<UserModel>> GetAllUsers();
}