namespace WinUIDemo.Core.Helpers;

public static class Json
{
    public static Task<T?> ToObjectAsync<T>(string value)
    {
        try
        {
            return Task.FromResult(JsonConvert.DeserializeObject<T>(value));
        }
        catch (JsonException ex)
        {
#if DEBUG
            Debug.WriteLine("Deserialization error");
            Debug.WriteLine(ex);
#endif
            return Task.FromResult<T?>(default);
        }
    }

    public static async Task<string> StringifyAsync(object value) => 
        await Task.Run<string>(() => JsonConvert.SerializeObject(value));
}
