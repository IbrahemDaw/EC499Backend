namespace DocumentManagement.Data.Models;

public class DocumentFilterModel : FilterModel
{
    public string? Title { get; set; }
    public string? Description { get; set;}
    public List<int> Tags { get; set; } = [];
    public List<int> Categories { get; set; } = [];
}
