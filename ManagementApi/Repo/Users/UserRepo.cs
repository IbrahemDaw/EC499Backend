// Ignore Spelling: Repo

namespace ManagementApi.Repo.Users;

public class UserRepo(UserManagementContext _db, JwtTokenUtility _jwtTokenUtility) : IUserRepo
{
    public async Task<OneOf<UserOutputModelSimple, ErrorResponse>> CreateAsync(UserInputModel model)
    {
        if (await _db.Users.AnyAsync(x => !x.IsDeleted && x.UserName == model.UserName))
            return new ErrorResponse { ErrorCode = 10, Message = model.UserName };

        var user = model.MapTo<User>();
        user.PasswordHash = model.Password.HashPass();
        if (model.Roles.Count != 0)
        {
            user.Roles = await _db.Roles.Where(x => model.Roles.Contains(x.Id))
                        .ToListAsync();
        }
        user.Roles.Add(new Role {Name = user.UserName,IsSelfRole = true} );
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        return user.MapTo<UserOutputModelSimple>();
    }

    public async Task<OneOf<UserOutputModelDetailed, ErrorResponse>> AssignRolesAsync(UserRolesUpdateModel model)
    {
        var user = await _db.Users.Where(x => !x.IsDeleted && x.Id == model.UserId)
            .Include(x => x.Roles)
            .SingleOrDefaultAsync();

        if (user == null)
            return new ErrorResponse { ErrorCode = 4, Message = "user was not found" };

        var roles = await _db.Roles.Where(x => model.RoleIds.Contains(x.Id))
            .ToListAsync();
        user.Roles = user.Roles
            .Concat(roles)
            .Distinct()
            .ToList();

        await _db.SaveChangesAsync();

        return user.MapTo<UserOutputModelDetailed>();
    }

    public async Task DeleteAsync(int[] Ids)
    {
        var user = await _db.Users
            .Where(x => Ids.Contains(x.Id))
            .ToListAsync();

        if (user == null)
            return;

        _db.Users.RemoveRange(user);
        await _db.SaveChangesAsync();
    }

    public async Task DisableUsersAsync(int[] ids)
    {
        var users = await _db.Users.Where(x => !x.IsDeleted && ids.Contains(x.Id))
            .ToListAsync();
        if (users == null)
            return;
        users.ForEach(x => x.IsEnabled = false);
        await _db.SaveChangesAsync();
    }

    public async Task EnableUsersAsync(int[] ids)
    {
        var users = await _db.Users.Where(x => !x.IsDeleted && ids.Contains(x.Id))
                .ToListAsync();
        if (users == null)
            return;
        users.ForEach(x => x.IsEnabled = true);
        await _db.SaveChangesAsync();
    }

    public async Task<PaginationModel<UserOutputModelSimple>?> FilterAsync(UserFilterModel filter)
    {
        return await _db.Users
            .Where(x => !x.IsDeleted && (string.IsNullOrWhiteSpace(filter.FullName) || x.FullName.Contains(filter.FullName))
                      && (string.IsNullOrWhiteSpace(filter.UserName) || x.FullName.Contains(filter.UserName))
                      && (string.IsNullOrWhiteSpace(filter.PhoneNumber) || x.FullName.Contains(filter.PhoneNumber))
                      && (filter.IsEnabled == null || filter.IsEnabled == x.IsEnabled))
            .MapTo<UserOutputModelSimple>()
            .ToPaginationModelAsync(filter);
    }

    public async Task<List<UserOutputModelSimple>?> GetAllAsync()
    {
        return await _db.Users
            .Where(x => !x.IsDeleted)
            .MapTo<UserOutputModelSimple>()
            .ToListAsync();
    }

    public async Task<List<PermissionOutputModelSimple>> GetAllPermissionAsync()
    {
        return await _db.Permissions
            .MapTo<PermissionOutputModelSimple>()
            .ToListAsync();
    }

    public async Task<UserOutputModelDetailed?> GetByIdAsync(int Id)
    {
        return await _db.Users
            .Where(x => !x.IsDeleted && x.Id == Id)
            .Include(x => x.Roles)
            .MapTo<UserOutputModelDetailed>()
            .FirstOrDefaultAsync();
    }

    public async Task<OneOf<UserLoginOutputModel, ErrorResponse>> Login(UserLoginInputModel model)
    {
        var User = await _db.Users.Where(x => !x.IsDeleted && x.UserName == model.UserName)
            .Include(x => x.Roles)
            .ThenInclude(x => x.Permissions)
            .SingleOrDefaultAsync();

        if (User == null || User.PasswordHash != model.Password.HashPass())
        {
            return new ErrorResponse
            {
                ErrorCode = 7,
                Message = "User name or Password incorrect"
            };
        }

        var generatedToken = _jwtTokenUtility.GenerateAccessToken(User);
        return new UserLoginOutputModel
        {
            Token = generatedToken,
            User = User.MapTo<UserOutputModelSimple>()
        };
    }

    public async Task<OneOf<UserOutputModelDetailed, ErrorResponse>> RemoveRolesAsync(UserRolesUpdateModel model)
    {
        var user = await _db.Users.Where(x => !x.IsDeleted && x.Id == model.UserId)
            .Include(x => x.Roles)
            .SingleOrDefaultAsync();

        if (user == null)
            return new ErrorResponse { ErrorCode = 7, Message = "User was not found" };

        user.Roles = user.Roles.Where(role => !role.IsDeleted && !model.RoleIds.Contains(role.Id))
            .ToList();

        await _db.SaveChangesAsync();

        return user.MapTo<UserOutputModelDetailed>();
    }

    public async Task<OneOf<UserOutputModelSimple, ErrorResponse>> UpdateAsync(UserUpdateModel model)
    {
        var user = await _db.Users.Include(x => x.Roles).SingleOrDefaultAsync(x => !x.IsDeleted && x.Id == model.Id);

        if (user == null)
            return new ErrorResponse { ErrorCode = 5, Message = "user was not found" };
        user.Roles = await _db.Roles.Where(x => model.RoleIds.Contains(x.Id))
            .ToListAsync();
        user.FullName = string.IsNullOrWhiteSpace(model.FullName) ? user.FullName : model.FullName;
        user.PhoneNumber = string.IsNullOrWhiteSpace(model.PhoneNumber) ? user.PhoneNumber : model.PhoneNumber;
        user.IsEnabled = model.IsEnabled;
        await _db.SaveChangesAsync();

        return user.MapTo<UserOutputModelSimple>();
    }
}
