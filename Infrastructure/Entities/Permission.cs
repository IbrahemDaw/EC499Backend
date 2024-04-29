using Infrastructure.DataSeed;

namespace Infrastructure.Entities;

public class Permission : BaseEntity
{
    public string Name { get; set; } = null!;
    public bool IsEnabled { get; set; }
    public Feature Feature { get; set; }
    public ICollection<Role> Roles { get; set; } = [];
}
public enum Feature
{
    User, Role, Tag, Document, Category
}

internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(128);

        builder.HasData(PermissionSeed.GetPermissions());

    }
}
