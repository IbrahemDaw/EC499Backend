using Microsoft.EntityFrameworkCore;

namespace Auth.Authorization;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class AuthorizedActionAttribute(int feature) : System.Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var Permissions = context.HttpContext.User.Claims.Where(c => c.Type == "permission").Select(c => int.Parse(c.Value)).ToList();
        var UserId = context.HttpContext.User.Claims.Where(c => c.Type == "id").Select(c => int.Parse(c.Value)).First();


        var isAuthorized = Permissions.Contains(feature) || UserId ==1;

        if (!isAuthorized)
        {
            context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
            return;
        }
    }

}
