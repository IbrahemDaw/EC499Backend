// Ignore Spelling: Repo

namespace ManagementApi.Repo.Roles;
public interface IRoleRepo
{
    Task<OneOf<RoleOutputModelSimple, ErrorResponse>> CreateAsync(RoleInputModel model);
    Task<RoleOutputModelDetailed?> GetByIdAsync(int Id);
    Task<List<RoleOutputModelSimple>> GetAllAsync();
    Task DeleteAsync(int[] Ids);
    Task<PaginationModel<RoleOutputModelSimple>?> FilterAsync(RoleFilterModel filter);
    Task<OneOf<RoleOutputModelSimple, ErrorResponse>> UpdateAsync(RoleUpdateModel model);
    Task<OneOf<RoleOutputModelDetailed, ErrorResponse>> AssignPermissionsAsync(RolePermissionsUpdateModel model);
    Task<OneOf<RoleOutputModelDetailed, ErrorResponse>> RemovePermissionsAsync(RolePermissionsUpdateModel model);
    Task DisableRoleAsync(int[] ids);
    Task EnableRoleAsync(int[] ids);
}
