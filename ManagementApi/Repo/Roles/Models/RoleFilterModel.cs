namespace ManagementApi.Repo.Roles.Models;

public class RoleFilterModel : FilterModel
{
    public string? Name { get; set; }
    public bool? IsEnabled { get; set; }
}
