using Infrastructure.DataSeed;

namespace Infrastructure.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Permission> Permissions { get; set; } = [];
    public ICollection<User> Users { get; set; } = [];
    public bool IsEnabled { get; set; }
}

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);
        builder.HasData(RoleSeed.GetRoleSeeds());

    }
}
