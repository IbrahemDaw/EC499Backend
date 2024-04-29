namespace ManagementApi.Repo.Roles.Models;

public class RoleInputModel
{
    public string Name { get; set; } = null!;
    public bool IsEnabled { get; set; }
    public List<int> PermissionIds { get; set; } = [];
}
public class RoleInputModelValidator : AbstractValidator<RoleInputModel>
{
    public RoleInputModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.IsEnabled)
            .NotNull();

        RuleFor(x => x.PermissionIds)
            .ForEach(x => x.GreaterThan(0))
            .When(x => x.PermissionIds.Count > 0);
    }
}
