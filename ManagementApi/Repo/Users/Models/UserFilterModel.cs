namespace ManagementApi.Repo.Users.Models;


public class UserFilterModel : FilterModel
{
    public string? FullName { get; set; }
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool? IsEnabled { get; set; }
}
