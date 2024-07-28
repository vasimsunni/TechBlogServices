using Blog.Domain.Models;
using System.Security.Claims;

namespace Blog.API.Extensions
{
    public static class UserClaimsExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Actor)?.Value!;
        }

        public static string GetUserRole(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value!;
        }
    }
}
