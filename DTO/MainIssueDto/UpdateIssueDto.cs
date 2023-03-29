using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.MainIssueDto
{
    public class UpdateIssueDto
    {
        [Key]
        public int issueId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime issueDateTime { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public bool ReturnStatement { get; set; }

        public DateTime enddateTime { get; set; }
    }
}
