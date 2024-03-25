// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

int menuItem = -1;
while (menuItem is < 0 or > 2)
{
	KeycloakUtils.PrintTitle();
	menuItem = KeycloakUtils.GetMenuItem();
	switch (menuItem)
	{
		case 0:
			return;
		case 1:
			await CommandAuthUserNameAsync();
			break;
		case 2:
			await CommandClientAsync();
			break;
	}
}
return;

async Task CommandAuthUserNameAsync()
{
	KeycloakUtils.PrintTitleUsername();

	PasswordGrantFlow credentials = FillPasswordGrantFlow();
	using AuthenticationHttpClient httpClient = AuthenticationHttpClientFactory.Create(credentials);
	using UsersApi usersApi = ApiClientFactory.Create<UsersApi>(httpClient);
	
	await WriteUsersAsync(usersApi, credentials);
}

async Task CommandClientAsync()
{
	KeycloakUtils.PrintTitleClient();
	
	ClientCredentialsFlow credentials = FillClientCredentialsFlow();
	using AuthenticationHttpClient httpClient = AuthenticationHttpClientFactory.Create(credentials);
	using UsersApi usersApi = ApiClientFactory.Create<UsersApi>(httpClient);
	
	await WriteUsersAsync(usersApi, credentials);
}

PasswordGrantFlow FillPasswordGrantFlow()
{
	string keycloakUrl = KeycloakUtils.GetKeycloakUrl();
	string realm = KeycloakUtils.GetRealm();
	string userName = KeycloakUtils.GetUserName();
	string password = KeycloakUtils.GetPassword();
	PasswordGrantFlow credentials = new()
	{
		KeycloakUrl = keycloakUrl,
		Realm = realm,
		UserName = userName,
		Password = password
	};
	return credentials;
}

ClientCredentialsFlow FillClientCredentialsFlow()
{
	string keycloakUrl = KeycloakUtils.GetKeycloakUrl();
	string realm = KeycloakUtils.GetRealm();
	string clientId = KeycloakUtils.GetClientId();
	string clientSecret = KeycloakUtils.GetClientSecret();
	ClientCredentialsFlow credentials = new()
	{
		KeycloakUrl = keycloakUrl,
		Realm = realm,
		ClientId = clientId,
		ClientSecret = clientSecret
	};
	return credentials;
}

async Task WriteUsersAsync(UsersApi usersApi, AuthenticationFlow credentials)
{
	try
	{
		Console.WriteLine($"  Users: {usersApi.GetUsersCountAsync(credentials.Realm)}");
		List<UserRepresentation> users = await usersApi.GetUsersAsync(credentials.Realm);
		foreach (UserRepresentation user in users)
		{
			Console.WriteLine(user);
		}
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	KeycloakUtils.PrintLine();
}
