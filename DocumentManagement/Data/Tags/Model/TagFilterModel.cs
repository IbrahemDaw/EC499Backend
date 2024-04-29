namespace DocumentManagement.Data.Models;

public class TagFilterModel : FilterModel
{
     public string? Name { get; set; } 
    public string? Description { get; set; }
    public bool? IsEnable { get; set; }
}
