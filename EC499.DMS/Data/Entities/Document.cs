using System.Text.Json.Serialization;
using DMS.Data.Entities.Commen;

namespace DMS.Data.Entities
{
    public class Document : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Previwe { get; set; } = "";
        public string Path { get; set; } = null!;
        public ICollection<Category> Categories { get; set; } = [];
        public ICollection<Tag> Tags { get; set; } = [];
            }
}
