using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace CeoToDoList.Models.DTO
{
    public class AddTaskReqDto
    {
        [Required]
        [MaxLength(150, ErrorMessage = "Title must be Under 150 letters")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool Completed { get; set; } = false;
        [Required]
        public Guid ListId { get; set; }
    }
}
