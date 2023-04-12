using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Syntax.WebApp.Internal.Controllers
{
    public class BaseController : Controller
    {

        protected ILogger<BaseController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        protected string Token { get; private set; }

        public BaseController(ILogger<BaseController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var token = _httpContextAccessor.HttpContext.Session.GetString("SyntaxToken");
            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    Token = token;
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);

                    // Extraindo as claims do token
                    var claims = jwtToken.Claims;

                    ViewBag.UserId = claims.FirstOrDefault(c => c.Type == "sub")?.Value;
                    ViewBag.UserName = claims.FirstOrDefault(c => c.Type == "name")?.Value;
                    ViewBag.UserEmail = claims.FirstOrDefault(c => c.Type == "email")?.Value;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao validar o token");
                }
            }
            else
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Logon", action = "Index" }));
            }
        }
    }

}
