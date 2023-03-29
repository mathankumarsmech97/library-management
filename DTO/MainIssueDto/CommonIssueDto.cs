using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.MainIssueDto
{
    public class CommonIssueDto
    {
       
        public int issueId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime issueDateTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]

        public bool ReturnStatement { get; set; }

        public DateTime enddateTime { get; set; }
    }
}
