namespace ProductManagement.Api.Middlewares
{
    //public class AuthMiddleware
    //{
    //    private readonly RequestDelegate _next;

    //    public AuthMiddleware(RequestDelegate next)
    //    {
    //        _next = next;
    //    }

    //    public async Task Invoke(HttpContext context)
    //    {
    //        if (context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
    //        {
    //            var token = authorizationHeader.ToString().Replace("Bearer ", "");

    //            // Validate or process the token as needed
    //            if (IsValidToken(token))
    //            {
    //                // Continue with the request pipeline
    //                await _next(context);
    //                return;
    //            }
    //        }

    //        // If no valid token is found, you can return an unauthorized response or handle it as per your application's logic.
    //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    //        await context.Response.WriteAsync("Unauthorized: Missing or invalid bearer token.");
    //    }

    //    private bool IsValidToken(string token)
    //    {
    //        // Implement your token validation logic here
    //        // You may want to use a library like IdentityServer, JWT libraries, or your custom validation logic.
    //        // Return true if the token is valid; otherwise, return false.
    //        return true;
    //    }

    //    public static IApplicationBuilder UseBearerTokenMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<AuthMiddleware>();
    //    }
    //}

    //public static class BearerTokenMiddlewareExtensions
    //{
       
    //}
}
