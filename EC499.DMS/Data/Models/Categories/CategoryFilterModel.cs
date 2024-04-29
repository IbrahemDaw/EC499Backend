namespace EC499.DMS.Data.Models.Categories;

public class CategoryFilterModel // : FilterModel
{
    public string? Name { get; set; }
    public int[] Tags { get; set; } = [];
}
