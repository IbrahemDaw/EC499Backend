namespace ManagementApi.Repo.Roles.Models;

public class RoleUpdateModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool? IsEnabled { get; set; }
    public List<int> Permissions { get; set; } = [];
}
public class RoleUpdateModelValidator : AbstractValidator<RoleUpdateModel>
{
    public RoleUpdateModelValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);


        RuleFor(x => x.Name)
            .MaximumLength(100)
            .When(x => x.Name != null);

    }
}
