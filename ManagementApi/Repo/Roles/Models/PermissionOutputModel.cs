namespace ManagementApi.Repo.Roles.Models;
public class PermissionOutputModel
{

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsEnabled { get; set; }
    public string Feature { get; set; } = null!;

}
