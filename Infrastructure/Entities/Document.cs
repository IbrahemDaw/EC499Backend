using Shared.Enums;

namespace Infrastructure.Entities;

public class Document : BaseEntity
{
    public string Title { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Previwe { get; set; } = "";
    public string Path { get; set; } = null!;
    public DocumentExtension DocumentExtension { get; set; }
    public ICollection<Category> Categories { get; set; } = [];
    public ICollection<Tag> Tags { get; set; } = [];
}
