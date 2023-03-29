using LibraryManagementSystem.Model.MainModel;

namespace LibraryManagementSystem.Model.Issuemodel
{
    public class ConnectionMemberIssue
    {
        public int ConMemberId { get; set; }
        public int ConIssueId { get; set; }


        public MembersDetails MembersDetails { get; set; }
        public MainIssueDetails MainIssueDetails { get; set; }
    }
}
