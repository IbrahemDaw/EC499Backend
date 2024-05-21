namespace ManagementApi.Repo.Users.Models;
public class CheangePasswordInputModel
{
    public string OldPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
}