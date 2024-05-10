namespace Infrastructure.Entities;

public class GradustionProject : BaseEntity
{
    public ICollection<Document> Documents { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int UserId { get; set; }
    public int DoctorId { get; set; }
}
