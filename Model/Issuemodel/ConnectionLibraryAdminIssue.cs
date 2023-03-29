using LibraryManagementSystem.Model.MainModel;

namespace LibraryManagementSystem.Model.Issuemodel
{
    public class ConnectionLibraryAdminIssue
    {
        public int ConAdminId { get; set; }
        public int ConIssueId { get; set; }


        public LibraryAdmin LibraryAdmin { get; set; }
        public MainIssueDetails MainIssueDetails { get; set; }
    }
}
