using System.ComponentModel.DataAnnotations.Schema;

namespace CeoToDoList.Models.Domain
{
    public class CeoTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; }=false;
        public Guid ListId { get; set; }

        // Navigation property
        public CeoList CeoList { get; set; }
    }
}
