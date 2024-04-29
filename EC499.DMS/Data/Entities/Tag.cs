namespace DMS.Data.Entities;

public class Tag : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = " ";
    public bool IsEnable { get; set; }
    public ICollection<Document> Documents { get; set; } = [];
}
