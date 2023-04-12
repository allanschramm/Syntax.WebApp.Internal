using Microsoft.AspNetCore.Mvc;

namespace Syntax.WebApp.Internal.Middleware
{
    public class TokenRequest
    {
        private readonly RequestDelegate _next;

        public TokenRequest(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Session.GetString("SyntaxToken");

            if (token == null)
            {
                context.Response.Redirect("/Logon");
                return;
            }
            await _next(context);

        }
    }
}
