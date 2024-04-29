namespace DMS.Data.Entities;

public class Category : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ICollection<Document> Documents { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
}
