namespace ManagementApi.Repo.Users.Models;
public class UserLoginInputModel
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
}
public class UserLoginInputModelValidator : AbstractValidator<UserLoginInputModel>
{
    public UserLoginInputModelValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}
