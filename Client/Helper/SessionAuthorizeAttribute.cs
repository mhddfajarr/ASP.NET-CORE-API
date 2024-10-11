using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Client.Helper
{
    public class SessionAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public SessionAuthorizeAttribute(string roles)
        {
            _roles = roles.Split(',');
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRoles = context.HttpContext.Session.GetString("Roles");

            if (string.IsNullOrEmpty(userRoles))
            {
                context.Result = new RedirectResult("/notLogin");
                return;
            }

            // Pisahkan role yang ada di session
            var sessionRoles = userRoles.Split(',').Select(r => r.Trim()).ToList();

            // Periksa apakah ada role yang cocok
            if (!_roles.Any(role => sessionRoles.Contains(role)))
            {
                context.Result = new RedirectResult("/AccessDenied");
            }
        }

    }
}
