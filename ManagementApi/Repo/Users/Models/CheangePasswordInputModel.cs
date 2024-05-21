namespace ManagementApi.Repo.Users.Models;
public class CheangePasswordInputModel
{
    public string CurrentPassword { get; set; } = null!;
    public string Password { get; set; } = null!;
}