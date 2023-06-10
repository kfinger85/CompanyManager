using System.Diagnostics;

using CompanyManager.Logging;

namespace CompanyManager.Middleware
{
    public class CustomMiddleware
{
    private readonly RequestDelegate _next;

    public CustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Logger.LogInformationForNetworkLog($"Executing {nameof(CustomMiddleware)}");
        foreach (var entry in context.Request.Headers)
        {
            Debug.WriteLine($"\t{entry.Key}: {entry.Value}");
        }
        // Log, inspect or modify the context.Request here...
        Debug.WriteLine($"\tPath: {context.Request.Path}");
        Logger.LogInformationForNetworkLog($"Path: {context.Request.Path}");
        Debug.WriteLine($"\tMethod: {context.Request.Method}");
        Logger.LogInformationForNetworkLog($"Method: {context.Request.Method}");
        Logger.LogInformationForNetworkLog($"QueryString: {context.Request.QueryString}");

        context.Request.EnableBuffering();

        var buffer = new MemoryStream();
        await context.Request.Body.CopyToAsync(buffer);

        context.Request.Body.Position = 0;

        string bodyAsText = new StreamReader(context.Request.Body).ReadToEnd();
        context.Request.Body.Position = 0;
        Debug.WriteLine($"\t\tBody: {bodyAsText}");
        if(bodyAsText.Length > 0)
            Logger.LogInformationForNetworkLog($"\n\tBody: \n\t{bodyAsText}\n");
        else
            Logger.LogInformationForNetworkLog($"\n\tBody: \n\tNo body\n");
        // Reset the request body stream to the copied memory stream
        context.Request.Body = new MemoryStream(buffer.ToArray());
        await _next(context);
    }
}

}

