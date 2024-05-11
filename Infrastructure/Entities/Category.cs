namespace Infrastructure.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<Document> Documents { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
}
internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasData(CategorySeed.GetCategorySeed());

    }
}