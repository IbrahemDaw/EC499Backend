
namespace DocumentManagement.Data.Model;

public class CategoryUpdateModel
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int[] Tags { get; set; } = [];
}
