
using Microsoft.AspNetCore.Authorization;

namespace ManagementApi.Controllers;

[Route("api/[controller]")]
public class UserManagementController(IUserRepo userRepo, IRoleRepo roleRepo) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserLoginOutputModel))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Login([FromBody] UserLoginInputModel model)
    {
        OneOf<UserLoginOutputModel, ErrorResponse> res = await userRepo.Login(model);
        return res.Match<ObjectResult>(Ok
        , BadRequest);
    }
    [HttpPut("AssignRolesToUser")]
    [AuthorizedAction(Permissions.UserWrite)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserOutputModelDetailed))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AssignRolesToUser([FromBody] UserRolesUpdateModel model)
    {
        OneOf<UserOutputModelDetailed, ErrorResponse> res = await userRepo.AssignRolesAsync(model);
        return res.Match<ObjectResult>(Ok,
            BadRequest);
    }
    [HttpPut("RemoveRolesFromUser")]
    [AuthorizedAction(Permissions.UserWrite)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserOutputModelDetailed))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RemoveRolesFromUser([FromBody] UserRolesUpdateModel model)
    {
        OneOf<UserOutputModelDetailed, ErrorResponse> res = await userRepo.RemoveRolesAsync(model);
        return res.Match<ObjectResult>(Ok,
            BadRequest);
    }
    [HttpGet("Permission")]
    [AuthorizedAction(Permissions.PermissionRead)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PermissionOutputModelSimple>))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetPermission()
    {
        List<PermissionOutputModelSimple>? res = await userRepo.GetAllPermissionAsync();
        return Ok(res);
    }
    [HttpPut("AssignPermissionsToRole")]
    [AuthorizedAction(Permissions.RoleWrite)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleOutputModelDetailed))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AssignPermissionsToRole([FromBody] RolePermissionsUpdateModel model)
    {
        OneOf<RoleOutputModelDetailed, ErrorResponse> res = await roleRepo.AssignPermissionsAsync(model);
        return res.Match<ObjectResult>(Ok, BadRequest);
    }
    [HttpPut("RemovePermissionsFromRole")]
    [AuthorizedAction(Permissions.RoleWrite)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleOutputModelDetailed))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RemovePermissionsFromRole([FromBody] RolePermissionsUpdateModel model)
    {
        var res = await roleRepo.RemovePermissionsAsync(model);
        return res.Match<ObjectResult>(Ok, BadRequest);
    }
    [HttpPut("DisableRoles")]
    [AuthorizedAction(Permissions.RoleWrite)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DisableRoles(int[] Ids)
    {
        await roleRepo.DisableRoleAsync(Ids);
        return NoContent();
    }
    [HttpPut("DisableUsers")]
    [AuthorizedAction(Permissions.UserWrite)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DisableUsers(int[] Ids)
    {
        await userRepo.DisableUsersAsync(Ids);
        return NoContent();
    }
    [HttpPut("EnableRoles")]
    [AuthorizedAction(Permissions.RoleWrite)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EnableRoles(int[] Ids)
    {
        await roleRepo.EnableRoleAsync(Ids);
        return NoContent();
    }
    [HttpPut("EnableUsers")]
    [AuthorizedAction(Permissions.UserWrite)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> EnableUsers(int[] Ids)
    {
        await userRepo.EnableUsersAsync(Ids);
        return NoContent();
    }
    [HttpGet("Profile")]
    public async Task<IActionResult> GetUserProfile()
    {
        var res = await userRepo.GetUserProfileAsync();
        return Ok(res);
    }
    [HttpPost("CheangePassword")]
    public async Task<IActionResult> CheangePassword(CheangePasswordInputModel model)
    {   
        string? res = await userRepo.CheangePasswordAsync(model);
        return res == null ? NoContent() : BadRequest(res);
    }

}
