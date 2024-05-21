namespace ManagementApi.Repo.Users;

public interface IUserRepo
{
    Task<OneOf<UserOutputModelSimple, ErrorResponse>> CreateAsync(UserInputModel model);
    Task<OneOf<UserLoginOutputModel, ErrorResponse>> Login(UserLoginInputModel model);
    Task<UserOutputModelDetailed?> GetByIdAsync(int Id);
    Task<List<UserOutputModelSimple>?> GetAllAsync();
    Task DeleteAsync(int[] Ids);
    Task<PaginationModel<UserOutputModelSimple>?> FilterAsync(UserFilterModel filter);
    Task<OneOf<UserOutputModelSimple, ErrorResponse>> UpdateAsync(UserUpdateModel model);
    Task<OneOf<UserOutputModelDetailed, ErrorResponse>> AssignRolesAsync(UserRolesUpdateModel model);
    Task<List<PermissionOutputModelSimple>> GetAllPermissionAsync();
    Task<OneOf<UserOutputModelDetailed, ErrorResponse>> RemoveRolesAsync(UserRolesUpdateModel model);
    Task DisableUsersAsync(int[] ids);
    Task EnableUsersAsync(int[] ids);
    Task<UserOutputModelSimple> GetUserProfileAsync();
    Task<string?> CheangePasswordAsync(CheangePasswordInputModel model);
    Task UpdateProfileAsync(ProfileUpdateModel model);
}
