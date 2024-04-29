namespace DocumentManagement.Data.Models;

public class DocumentDownloadModel
{
    public byte[] File { get; set; } =[];
    public string FileContent { get; set; } =null!;
    public string Title { get; set;} = null!;
}
