// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace KeycloakDemo;

public static class KeycloakUtils
{
	public static void PrintLine() => Console.WriteLine("--------------------------------------------------------------------------------");

	public static void PrintTitle()
	{
		Console.Clear();
		PrintLine();
		Console.WriteLine("---                                 Keycloak                                 ---");
		PrintLine();
		Console.WriteLine("  Getting started with Dockerhttps");
		Console.WriteLine("    ttps://www.keycloak.org/getting-started/getting-started-docker");
		Console.WriteLine("  .NET / C# Keycloak.RestApiClient");
		Console.WriteLine("    https://github.com/fschick/Keycloak.RestApiClient");
		PrintLine();
	}

	public static int GetMenuItem()
	{
		Console.WriteLine("  Switch authentication type");
		Console.WriteLine("    0 - Exit");
		Console.WriteLine("    1 - With authentication by username/password");
		Console.WriteLine("    2 - With authentication by client-id/client-secret");
		PrintLine();
		Console.Write("  Menu item: ");
		string input = Console.ReadLine() ?? string.Empty;
		if (int.TryParse(input, out int result))
			return result;
		return -1;
	}

	public static void PrintTitleUsername()
	{
		PrintLine();
		Console.WriteLine("    1 - With authentication by username/password");
		PrintLine();
	}

	public static void PrintTitleClient()
	{
		PrintLine();
		Console.WriteLine("    2 - With authentication by client-id/client-secret");
		PrintLine();
	}

	public static string GetStringValue(string question, string defaultValue = "")
	{
		string extra = $" (use enter for default value | [{defaultValue}]): ";
		if (!question.Contains(extra))
			question += extra;
		Console.Write(question);
		string result = Console.ReadLine() ?? string.Empty;
		if (string.IsNullOrEmpty(result))
			result = defaultValue;
		return result;
	}

	public static string GetKeycloakUrl() => GetStringValue("Enter URL", "http://localhost:8080");
	
	public static string GetRealm() => GetStringValue("Enter Realm", "master");
	
	public static string GetUserName() => GetStringValue("Enter UserName", "admin");
	
	public static string GetPassword() => GetStringValue("Enter Password", "admin");
	
	public static string GetClientId() => GetStringValue("Enter Client Id");
	
	public static string GetClientSecret() => GetStringValue("Enter Client Secret");
}