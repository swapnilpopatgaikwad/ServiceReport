using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReport.Helper
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public CustomAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            if (_roles.Length > 0)
            {
                var userHasValidRole = _roles.Any(role => user.IsInRole(role));

                if (!userHasValidRole)
                {
                    context.Result = new ForbidResult();
                }
            }
        }
    }
}
