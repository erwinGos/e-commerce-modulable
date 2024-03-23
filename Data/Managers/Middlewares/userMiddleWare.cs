using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Data.Services.Contract;
using Database.Entities;
using System.Globalization;

public class UserMiddleWare
{
    private readonly RequestDelegate _next;

    public UserMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IUserService _userService)
    {
        if (context.User.Identity.IsAuthenticated) {
            var userId = Int32.Parse(context.User.FindFirst("UserId").Value);
            User checkUser = await _userService.GetUserById(userId);
            if(checkUser.IsBanned || checkUser.IsDeactivated) {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                
                DateTime time = DateTime.UtcNow.AddMicroseconds(1000);
                string timeToString = time.ToString("R", CultureInfo.InvariantCulture);

                context.Response.Headers.Add("Set-Cookie", "auth_token= ; Path=/; Secure;Expires=" + timeToString);
                await context.Response.WriteAsync("Votre compte est " + (checkUser.IsBanned ? "banni" : "désactivé"));
                return;
            }
        }
        await _next(context);
    }
}