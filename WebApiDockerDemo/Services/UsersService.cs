// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WebApiDockerDemo.Services;

public class UsersService : IUsersService
{
	public async Task<IEnumerable<UserModel>> GetAllUsers()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
		return new List<UserModel>
		{
			new(1, "User 1", "Email_1@mail.test", "Address 1"),
			new(2, "User 2", "Email_2@mail.test", "Address 2"),
			new(3, "User 3", "Email_3@mail.test", "Address 3"),
			new(4, "User 4", "Email_4@mail.test", "Address 4"),
			new(5, "User 5", "Email_5@mail.test", "Address 5"),
		};
	}
}