namespace CeoToDoList.Models.DTO
{
    public class CeoTaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; } = false;
        public Guid ListId { get; set; }
    }
}
