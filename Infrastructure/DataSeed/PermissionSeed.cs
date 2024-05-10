using Shared;
namespace Infrastructure.DataSeed;

public static class PermissionSeed
{
    public static List<Permission> GetPermissions()
    {
        return new List<Permission>
        {
            new Permission {Id = Permissions.TagRead, IsEnabled = true, Name = "Read ", Feature = Feature.Tag},
            new Permission {Id = Permissions.TagWrite, IsEnabled = true, Name = "Update ", Feature = Feature.Tag},
            new Permission {Id = Permissions.TagCreate, IsEnabled = true, Name = "Create ", Feature = Feature.Tag},
            new Permission {Id = Permissions.TagDelete, IsEnabled = true, Name = "Delete ", Feature = Feature.Tag},

            new Permission {Id = Permissions.DocumentRead, IsEnabled = true, Name = "Read ", Feature = Feature.Document},
            new Permission {Id = Permissions.DocumentWrite, IsEnabled = true, Name = "Update ", Feature = Feature.Document},
            new Permission {Id = Permissions.DocumentCreate, IsEnabled = true, Name = "Create ", Feature = Feature.Document},
            new Permission {Id = Permissions.DocumentDelete, IsEnabled = true, Name = "Delete ", Feature = Feature.Document},

            new Permission {Id = Permissions.UserRead, IsEnabled = true, Name = "Read ", Feature = Feature.User},
            new Permission {Id = Permissions.UserWrite, IsEnabled = true, Name = "Update ", Feature = Feature.User},
            new Permission {Id = Permissions.UserCreate, IsEnabled = true, Name = "Create ", Feature = Feature.User},
            new Permission {Id = Permissions.UserDelete, IsEnabled = true, Name = "Delete ", Feature = Feature.User},

            new Permission {Id = Permissions.RoleRead, IsEnabled = true, Name = "Read ", Feature = Feature.Role},
            new Permission {Id = Permissions.RoleWrite, IsEnabled = true, Name = "Update ", Feature = Feature.Role},
            new Permission {Id = Permissions.RoleCreate, IsEnabled = true, Name = "Create ", Feature = Feature.Role},
            new Permission {Id = Permissions.RoleDelete, IsEnabled = true, Name = "Delete ", Feature = Feature.Role},

            new Permission {Id = Permissions.CategoryRead, IsEnabled = true, Name = "Read ", Feature = Feature.Category},
            new Permission {Id = Permissions.CategoryWrite, IsEnabled = true, Name = "Update ", Feature = Feature.Category},
            new Permission {Id = Permissions.CategoryCreate, IsEnabled = true, Name = "Create ", Feature = Feature.Category},
            new Permission {Id = Permissions.CategoryDelete, IsEnabled = true, Name = "Delete ", Feature = Feature.Category},

            new Permission {Id = Permissions.GradustionProjectRead, IsEnabled = true, Name = "Read ", Feature = Feature.GradustionProject},
            new Permission {Id = Permissions.GradustionProjectWrite, IsEnabled = true, Name = "Update ", Feature = Feature.GradustionProject},
            new Permission {Id = Permissions.GradustionProjectCreate, IsEnabled = true, Name = "Create ", Feature = Feature.GradustionProject},
            new Permission {Id = Permissions.GradustionProjectDelete, IsEnabled = true, Name = "Delete ", Feature = Feature.GradustionProject},
        };

    }
}
