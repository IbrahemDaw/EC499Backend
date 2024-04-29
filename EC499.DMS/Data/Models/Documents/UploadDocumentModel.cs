using Org.BouncyCastle.Bcpg;

namespace DMS;

public class DocumentInputModel
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public List<int> Categories { get; set; } = [];
    public List<int> Tags { get; set; } = [];
    //public IFormFile File {get;set;} = null!;
}
