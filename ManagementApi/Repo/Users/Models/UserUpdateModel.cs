namespace ManagementApi.Repo.Users.Models;
public class UserUpdateModel
{
    public int Id { get; set; }
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsEnabled { get; set; }
    public List<int> Roles { get; set; } = [];
    public List<int> Permissions { get; set; } = [];

}
public class UserUpdateModelValidator : AbstractValidator<UserUpdateModel>
{
    public UserUpdateModelValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0);

        RuleFor(x => x.FullName)
            .MaximumLength(100)
            .When(x => x.FullName != null);

        RuleFor(x => x.UserName)
            .MaximumLength(100)
            .When(x => x.UserName != null);


        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches("^[0-9]+$")
            .MaximumLength(20)
            .When(x => x.PhoneNumber != null);

        RuleFor(x => x.Roles)
            .ForEach(branchId => branchId
                .GreaterThan(0))
            .When(x => x.Roles.Count != 0);
    }
}
