

namespace DocumentManagement.Data.Models;

public class DocumentOutputModelSimple
{
    public int Id { get; set; }
    public string Path { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string DocumentExtension { get; set; } = null!;
}
public class DocumentOutputModel : DocumentOutputModelSimple
{
    public string Previwe { get; set; } = null!;
    public List<int> Categories { get; set; } = [];
    public List<int> Tags { get; set; } = [];
}
public class DocumentFileOutputModel
{
    public string Path { get; set; } = null!;
    public string Title { get; set; } = null!;
}