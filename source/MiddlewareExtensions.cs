using Microsoft.AspNetCore.Builder;

namespace backend_project
{
    public static class MiddlewareExtensions
    {
            public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<AuthMiddleware>();
            }
        }
    }
