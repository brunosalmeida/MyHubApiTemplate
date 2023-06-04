using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Error;

public class ErrorMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }
    
    private static Task HandleExceptionAsync(HttpContext context, Exception exception) 
    {
        const HttpStatusCode code = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new {error = "Error occured while processing your request" });
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}