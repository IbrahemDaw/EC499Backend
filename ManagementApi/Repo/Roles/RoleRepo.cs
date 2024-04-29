namespace ManagementApi.Repo.Roles;
public class RoleRepo(UserManagementContext mataaDbContext) : IRoleRepo
{
    private readonly UserManagementContext _db = mataaDbContext;

    public async Task<OneOf<RoleOutputModelSimple, ErrorResponse>> CreateAsync(RoleInputModel model)
    {
        var role = model.MapTo<Role>();
        if (await _db.Roles.AnyAsync(x => x.Name == role.Name))
            return new ErrorResponse
            {
                ErrorCode = 400,
                Message = "Role Name already used"
            };
        var per = await _db.Permissions.Where(x => model.PermissionIds.Contains(x.Id)).ToListAsync();
        role.Permissions = per;
        _db.Roles.Add(role);
        await _db.SaveChangesAsync();
        return role.MapTo<RoleOutputModelSimple>();
    }

    public async Task<OneOf<RoleOutputModelDetailed, ErrorResponse>> AssignPermissionsAsync(RolePermissionsUpdateModel model)
    {
        var role = await _db.Roles
            .Where(x => x.Id == model.RoleId)
            .Include(x => x.Permissions)
            .SingleOrDefaultAsync();
        if (role == null) return new ErrorResponse { Message = "Role was not found ", ErrorCode = 4 };
        var permissions = await _db.Permissions
            .Where(x => model.PermissionIds.Contains(x.Id))
            .ToListAsync();
        role.Permissions = role.Permissions
            .Concat(permissions)
            .Distinct()
            .ToList();
        await _db.SaveChangesAsync();
        return role.MapTo<RoleOutputModelDetailed>();
    }

    public async Task DeleteAsync(int[] Ids)
    {
        var role = await _db.Roles.Where(x => !x.IsDeleted && Ids.Contains(x.Id))
            .ToListAsync();
        if (role == null) return;
        _db.Roles.RemoveRange(role);
        await _db.SaveChangesAsync();
    }

    public async Task DisableRoleAsync(int[] ids)
    {
        var roles = await _db.Roles.Where(x => x.IsEnabled && !x.IsDeleted && ids.Contains(x.Id))
            .ToListAsync();
        if (roles == null)
            return;
        roles.ForEach(x => x.IsEnabled = false);
        await _db.SaveChangesAsync();

    }

    public async Task EnableRoleAsync(int[] ids)
    {
        var roles = await _db.Roles.Where(x => !x.IsEnabled && !x.IsDeleted && ids.Contains(x.Id))
                .ToListAsync();
        if (roles == null)
            return;
        roles.ForEach(x => x.IsEnabled = true);
        await _db.SaveChangesAsync();
    }

    public async Task<PaginationModel<RoleOutputModelSimple>?> FilterAsync(RoleFilterModel filter)
    {
        return await _db.Roles.Where(role => (string.IsNullOrWhiteSpace(filter.Name) || role.Name.Contains(filter.Name))
                                         && (filter.IsEnabled == null || (bool)filter.IsEnabled == role.IsEnabled)
                                         && !role.IsDeleted)
            .MapTo<RoleOutputModelSimple>()
            .ToPaginationModelAsync(filter);
    }

    public async Task<List<RoleOutputModelSimple>> GetAllAsync()
    {
        return await _db.Roles.Where(x => !x.IsDeleted)
            .MapTo<RoleOutputModelSimple>()
            .ToListAsync();
    }

    public async Task<RoleOutputModelDetailed?> GetByIdAsync(int Id)
    {
        var role = await _db.Roles.Where(x => x.Id == Id && !x.IsDeleted)
            .MapTo<RoleOutputModelDetailed>()
            .FirstOrDefaultAsync();
        if (role == null)
            return null;
        /*  role.FeaturePermissions = role.Permissions.GroupBy(x => x.FeatureName).ToList().Select(x => new FeatureWithPermissionOutputModel
         {
             FeatureName = x.Key,
             Permissions = x
         }).ToList(); */
        return role.MapTo<RoleOutputModelDetailed>();

    }

    public async Task<OneOf<RoleOutputModelDetailed, ErrorResponse>> RemovePermissionsAsync(RolePermissionsUpdateModel model)
    {
        var role = await _db.Roles
            .Where(x => x.Id == model.RoleId)
            .Include(x => x.Permissions)
            .SingleOrDefaultAsync();
        if (role == null) return new ErrorResponse { Message = "Role was not found", ErrorCode = 4 };
        role.Permissions = role.Permissions.Where(x => !model.PermissionIds.Contains(x.Id)).ToList();
        await _db.SaveChangesAsync();
        return role.MapTo<RoleOutputModelDetailed>();
    }

    public async Task<OneOf<RoleOutputModelSimple, ErrorResponse>> UpdateAsync(RoleUpdateModel model)
    {
        var role = await _db.Roles.Where(x => x.Id == model.Id).Include(x => x.Permissions).SingleOrDefaultAsync();

        if (role == null)
            return new ErrorResponse { ErrorCode = 10, Message = "Role was not found" };

        role.Name = string.IsNullOrWhiteSpace(model.Name) ? role.Name : model.Name;
        role.IsEnabled = model.IsEnabled ?? role.IsEnabled;
        role.Permissions = await _db.Permissions.Where(x => model.Permissions.Contains(x.Id))
        .ToListAsync();
        await _db.SaveChangesAsync();

        return role.MapTo<RoleOutputModelSimple>();
    }


}

