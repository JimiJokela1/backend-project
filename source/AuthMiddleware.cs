using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace backend_project
{

    public class AuthMiddleware
    {

        private readonly RequestDelegate _next;
        public readonly ApiKey _apikey;

        public AuthMiddleware(RequestDelegate next, ApiKey apiKey)
        {
            _next = next;
            _apikey = apiKey;
        }
        public async Task Invoke(HttpContext context)
        {
            var claimsPrincipal = new System.Security.Claims.ClaimsPrincipal();
            var headers = context.Request.Headers;

            if (!headers.ContainsKey("x-api-key"))
            {
                context.User = claimsPrincipal;
                context.Response.StatusCode = 400;
            }
            else
            {
                if (headers["x-api-key"] == _apikey.key)
                {
                    var claim = new Claim(ClaimTypes.Role, "User");
                    var identity = new ClaimsIdentity();
                    identity.AddClaim(claim);
                    claimsPrincipal.AddIdentity(identity);
                    context.User = claimsPrincipal;

                    await _next(context);
                    return;

                }
                else if (headers["x-api-key"] == _apikey.adminKey)
                {
                    var claim = new Claim(ClaimTypes.Role, "Admin");
                    var identity = new ClaimsIdentity();
                    identity.AddClaim(claim);
                    claimsPrincipal.AddIdentity(identity);
                    context.User = claimsPrincipal;

                    await _next(context);
                    return;
                }
                else
                {
                    context.User = claimsPrincipal;
                    context.Response.StatusCode = 403;
                }
            }

        }
    }

}