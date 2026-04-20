using eUDrive.Domains.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eUDrive.Api.Filters
{
    public class AdminOnlyAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Items.TryGetValue("Role", out var roleobj) || roleobj is not URole role)
            {
                context.Result = new UnauthorizedObjectResult(new { IsSuccess = false, message = "Not authorized" });
                return;
            }

            if (role != URole.Admin)
            {
                context.Result = new ForbidResult();

                return;
            }
        }
    }
}
