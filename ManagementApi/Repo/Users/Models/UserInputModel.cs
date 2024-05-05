namespace ManagementApi.Repo.Users.Models;

public class UserInputModel
{
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public List<int> Roles { get; set; } = [];

}
public class UserInputModelValidator : AbstractValidator<UserInputModel>
{
    public UserInputModelValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .MaximumLength(100);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .Matches("^[0-9]+$");
    }
}
