using LibraryManagementSystem.Model.Issuemodel;
using LibraryManagementSystem.Model.Returnmodel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Model.MainModel
{
    public class MembersDetails
    {
        [Key]
        public int MemberId { get; set; }

        [Required(ErrorMessage = " MemberName Must Need Enter")]
        public string MemberName { get; set; }

        [EmailAddress(ErrorMessage = "EmailId Must Need Enter")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "PhoneNumber Must Need Enter")]
        [Range(0, 1000000000)]
        public double PhoneNumber { get; set; }

        public ICollection<ConnectionMemberIssue> CoonectionMemberIssues { get; set; }

        public ICollection<ConnectionMemberReturn> ConnectionMemberReturns { get; set; }
    }
}
