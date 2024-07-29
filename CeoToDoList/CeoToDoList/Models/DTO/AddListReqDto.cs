using CeoToDoList.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace CeoToDoList.Models.DTO
{
    public class AddListReqDto
    {
        [Required]
        [MaxLength(150, ErrorMessage = "Title must be Under 150 letters")]
        public String Title { get; set; }
        public String? Description { get; set; }

    }
}
