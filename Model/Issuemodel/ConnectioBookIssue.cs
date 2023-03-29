using LibraryManagementSystem.Model.MainModel;

namespace LibraryManagementSystem.Model.Issuemodel
{
    public class ConnectioBookIssue
    {
        public int ConbookId { get; set; }
        public int ConIssueId { get; set; }

        public BookDetails BookDetails { get; set; }
        public MainIssueDetails MainIssueDetails { get; set; }

    }
}
