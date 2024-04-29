
namespace DocumentManagement.Data.Models;

public class CategoryFilterModel  : FilterModel
{
    public string? Name { get; set; }
    public int[] Tags { get; set; } = [];
}
