namespace ManagementApi.Repo.Users.Models;
public class UserOutputModelSimple
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }
    public bool IsEnabled { get; set; }
    public bool RequirePasswordChange { get; set; }

}
public class UserOutputModelDetailed : UserOutputModelSimple
{
    public List<RoleOutputModelSimple> Roles { get; set; } = [];

}

public class UserLoginOutputModel
{
    public string Token { get; set; } = null!;
    public UserOutputModelSimple User { get; set; } = null!;
}
