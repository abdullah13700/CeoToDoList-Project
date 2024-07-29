namespace CeoToDoList.Models.Domain
{
    public class CeoList
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        // Navigation property
        public ICollection<CeoTask> Tasks { get; set; } = new List<CeoTask>();
    }
}
