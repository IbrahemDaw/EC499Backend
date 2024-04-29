namespace ManagementApi.Repo.Roles.Models;

public class RolePermissionsUpdateModel
{
    public int RoleId { get; set; }
    public List<int> PermissionIds { get; set; } = [];
}
public class AssignPermissionToRoleModelValidator : AbstractValidator<RolePermissionsUpdateModel>
{
    public AssignPermissionToRoleModelValidator()
    {
        RuleFor(x => x.RoleId)
            .GreaterThan(0);

        RuleFor(x => x.PermissionIds)
            .NotEmpty()
            .ForEach(x => x.GreaterThan(0));

    }
}
