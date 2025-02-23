using Domain.Interfaces;
using ApiEndPoint.Attributes;
using System.Security.Claims;

namespace ApiEndPoint.MiddleWare
{
    public class PermissionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
        {
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }

            var userId = userIdClaim;
            var endpoint = context.GetEndpoint();

            var requiredPermission = endpoint?.Metadata.GetMetadata<RequiredPermissionAttribute>()?.Permission;
            if (requiredPermission != null)
            {
                var hasPermission = await userRepository.HasPermissionAsync(userId, requiredPermission);
                if (!hasPermission)
                {
                    context.Response.StatusCode = 403; // Forbidden
                    return;
                }
            }

            await _next(context);
        }
    }
}
