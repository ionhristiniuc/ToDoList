using System.Security.Claims;
using ToDoList.API.Exceptions;

namespace ToDoList.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetLoggedInUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new UnauthorizedException();

            string userId = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                throw new UnauthorizedException();

            return userId;
        }
    }
}
