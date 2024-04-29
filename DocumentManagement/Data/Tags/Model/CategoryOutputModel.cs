namespace DocumentManagement.Data.Model;
public class CategoryOutputModelSimple
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
public class CategoryOutputModel : CategoryOutputModelSimple
{
    public string Description { get; set; } = null!;
}
public class CategoryOutputModelDetailed : CategoryOutputModel
{
    public int[] Tags {get;set;} =[];
}