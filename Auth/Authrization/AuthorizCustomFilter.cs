using Microsoft.EntityFrameworkCore;

namespace Auth.Authorization;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class AuthorizedActionAttribute(int feature) : System.Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {

        var UserId = context.HttpContext.User.Claims.Where(c => c.Type == "id").Select(c => c.Value).First();

        var dbContext = context.HttpContext.RequestServices.GetRequiredService<UserManagementContext>();
        var userId = int.Parse(UserId);

        var isAuthorized = IsAuthorized(feature, userId, dbContext);

        if (!await isAuthorized)
        {
            context.Result = new JsonResult(new { message = "UnAuthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }
    }

    private static async Task<bool> IsAuthorized(int feature, int userId, UserManagementContext dbContext)
    {
        var user = await dbContext.Users.Where(user => userId == user.Id)
            .Include(x => x.Roles)
            .ThenInclude(x => x.Permissions)
            .SingleOrDefaultAsync();
        if (user == null)
            return false;
        if (user.Roles.Any(x => x.Id == 1) || user.Id ==1)
            return true;

        return user.Roles
            .SelectMany(x => x.Permissions)
            .Any(x => x.Id == feature);
    }
}
