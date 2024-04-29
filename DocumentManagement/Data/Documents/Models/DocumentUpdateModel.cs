namespace DocumentManagement.Data.Models;

public class DocumentUpdateModel
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<int> Categories { get; set; } = [];
    public List<int> Tags { get; set; } = [];
}
