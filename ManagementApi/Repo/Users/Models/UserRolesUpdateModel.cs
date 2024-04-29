namespace ManagementApi.Repo.Users.Models
{
    public class UserRolesUpdateModel
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; } = [];
    }
}
