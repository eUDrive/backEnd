using System.Linq;
using eUDrive.BusinessLogic.Interfaces;
using eUDrive.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eUDrive.Api.Filters
{
    public class SessionAuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<Microsoft.AspNetCore.Authorization.IAllowAnonymous>().Any();

            if (allowAnonymous) return;


            var cookie = context.HttpContext.Request.Cookies["X-KEY"];
            if (string.IsNullOrWhiteSpace(cookie))
            {
                context.Result = new UnauthorizedObjectResult(new { IsSucces = false, message = "Not authorized" });
                return;
            }

            var bl = new BusinessLogic.BusinessLogic();
            ISessionActions session = bl.GetSessionActions();

            var userId = session.GetUserIdByCookie(cookie);

            if (userId == null)
            {
                context.HttpContext.Response.Cookies.Append("X-KEY", "", new CookieOptions
                {
                    Expires = DateTimeOffset.Now.AddDays(-1),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                });

                context.Result = new UnauthorizedObjectResult(new { IsSuccess = false, message = "Session expired or Invalid" });
                return;
            }

            using (var db = new UserContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Id == userId.Value && u.IsActive);
                if (user == null)
                {
                    context.Result = new UnauthorizedObjectResult(new { IsSuccesss = false, message = "User nor found" });

                    return;
                }

                context.HttpContext.Items["UserId"] = userId.Value;
                context.HttpContext.Items["Role"] = user.Role;
            }
        }
    }
}
