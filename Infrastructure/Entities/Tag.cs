namespace Infrastructure.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = " ";
    public bool IsEnable { get; set; }
    public ICollection<Document> Documents { get; set; } = [];
}
