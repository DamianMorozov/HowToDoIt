namespace WinUIDemo.Core.Helpers;

public static class Json
{
    public static async Task<T> ToObjectAsync<T>(string value) => 
        await Task.Run<T>(() => JsonConvert.DeserializeObject<T>(value));

    public static async Task<string> StringifyAsync(object value) => 
        await Task.Run<string>(() => JsonConvert.SerializeObject(value));
}
