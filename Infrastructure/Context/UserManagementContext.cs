namespace Infrastructure;
public class UserManagementContext(DbContextOptions<UserManagementContext> options) : DbContext(options)
{

    public DbSet<Permission> Permissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementContext).Assembly);
    }
    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>().ToList())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.UpdatedAt = DateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.Now;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.IsDeleted = true;
                    break;
            }
        }
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var ManagementBaseEntry = ChangeTracker.Entries<BaseEntity>().ToList();
        var ManagementBaseEntities = ManagementBaseEntry.Select(x => x.Entity).ToList();
        foreach (var entry in ManagementBaseEntry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.UpdatedAt = DateTime.Now;
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.Now;
                    break;

                    // case EntityState.Deleted:
                    //     entry.State = EntityState.Modified;
                    //     entry.Entity.UpdatedAt = DateTime.Now;
                    //     entry.Entity.IsDeleted = true;
                    //     break;
            }
        }
        return await base.SaveChangesAsync(cancellationToken);
    }
}
