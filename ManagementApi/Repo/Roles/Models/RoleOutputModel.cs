namespace ManagementApi.Repo.Roles.Models;
public class RoleOutputModelSimple
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsEnabled { get; set; }
}
public class RoleOutputModelDetailed : RoleOutputModelSimple
{
    public List<PermissionOutputModelSimple> Permissions { get; set; } = [];
    //public List<FeatureWithPermissionOutputModel> FeaturePermissions { get; set; } = [];
    public List<UserOutputModelSimple> Users { get; set; } = [];
}

public class FeatureWithPermissionOutputModel
{
    public string FeatureName { get; set; } = null!;
    public IGrouping<string, PermissionOutputModelSimple> Permissions { get; set; } = null!;
}
public class PermissionOutputModelSimple
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsEnabled { get; set; }
    public string Feature { get; set; } = null!;

}

