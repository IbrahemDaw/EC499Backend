using Infrastructure.DataSeed;

namespace Infrastructure.Entities;

public class User : BaseEntity
{
    public string FullName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string? Email { get; set; }
    public bool RequirePasswordChange { get; set; } = true;
    public bool IsEnabled { get; set; }
    public ICollection<Role> Roles { get; set; } = [];
}

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.FullName)
            .HasMaxLength(256);

        builder.Property(x => x.UserName)
            .HasMaxLength(256);

        builder.Property(x => x.PasswordHash)
            .HasMaxLength(256);

        builder.Property(x => x.Email)
            .HasMaxLength(256);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(20);

        builder.HasData(UserSeed.GetUserSeed());




    }
}
