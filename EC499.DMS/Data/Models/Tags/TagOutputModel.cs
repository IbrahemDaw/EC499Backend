namespace DMS.Data.Models.Tags;
public class TagOutputModelSimple
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
public class TagOutputModel : TagOutputModelSimple
{
    public string Description { get; set; } = null!;
    public bool IsEnable { get; set; }
}
