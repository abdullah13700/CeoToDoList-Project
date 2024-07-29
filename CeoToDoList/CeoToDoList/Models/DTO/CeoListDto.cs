namespace CeoToDoList.Models.DTO
{
    public class CeoListDto
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String? Description { get; set; }
        public List<CeoTaskDto> Tasks { get; set; } = new List<CeoTaskDto>();
    }
}
