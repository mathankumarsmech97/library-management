using LibraryManagementSystem.Model.Issuemodel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Model.MainModel
{
    public class MainIssueDetails
    {
        [Key]
        public int IssueId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime IssueDateTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnddateTime { get; set; }

        public bool ReturnStatement { get; set; }

        public ICollection<ConnectioBookIssue> ConnectioBookIssues { get; set; }

        public ICollection<ConnectionLibraryAdminIssue> ConnectionLibraryAdminIssues { get; set; }

        public ICollection<ConnectionMemberIssue> CoonectionMemberIssues { get; set; }
    }
}
