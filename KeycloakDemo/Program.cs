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
		if (string.IsNullOrEmpty(credentials.KeycloakUrl)) return;
		if (string.IsNullOrEmpty(credentials.Realm)) return;
		if (credentials is PasswordGrantFlow passwordGrant)
		{
			if (string.IsNullOrEmpty(passwordGrant.UserName)) return;
			if (string.IsNullOrEmpty(passwordGrant.Password)) return;
		}
		if (credentials is ClientCredentialsFlow clientCredentials)
		{
			if (string.IsNullOrEmpty(clientCredentials.ClientId)) return;
			if (string.IsNullOrEmpty(clientCredentials.ClientSecret)) return;
		}

		// Portioned data reading, since downloading all data at once crashes on timeout.
		var usersCount = usersApi.GetUsersCount(credentials.Realm);
		Console.WriteLine($"  Users count: {usersCount}");
		var batchSize = 100;
		var firstPosition = 0;
		while (firstPosition < usersCount)
		{
			List<UserRepresentation> users = await usersApi.GetUsersAsync(credentials.Realm, first: firstPosition, max: batchSize);
			foreach (UserRepresentation user in users)
				Console.WriteLine(user);
			firstPosition += batchSize;
		}
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
	KeycloakUtils.PrintLine();
}
