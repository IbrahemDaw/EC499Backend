namespace Infrastructure.Entities;

public class GradustionProject : BaseEntity
{
    public Document Document { get; set; } = null!;
    public int DocumentId { get; set; }
    public int UserId { get; set; }
    public int DoctorId { get; set; }
}
