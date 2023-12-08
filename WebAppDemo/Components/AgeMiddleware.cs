namespace WebAppDemo.Components;

public sealed class AgeMiddleware
{
    #region Public and private fields, properties, constructor

    private readonly RequestDelegate _next;

    public AgeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    #endregion

    #region Public and private methods

    public async Task InvokeAsync(HttpContext context)
    {
        int age = string.IsNullOrEmpty(context.Request.Query["age"])
            ? 0
            : Convert.ToInt32(context.Request.Query["age"]);
        if (age < 18)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Age is invalid!");
        }
        else
        {
            await _next.Invoke(context);
        }
    }

    #endregion
}