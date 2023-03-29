using LibraryManagementSystem.Model.MainModel;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IIssueRepository
    {
        public int Create(int Bookid, int adminid, int memberid, MainIssueDetails entity);
        public bool Delete(MainIssueDetails entiry);
        public MainIssueDetails GetId(int id);
        public List<MainIssueDetails> GetAll();
        public bool Update(int BookId, int memberId, MainIssueDetails entity);
        public bool Isadmin(int adminId);
        public bool IsBookId(int bookId);
        public bool IsMemberId(int memberId);
        public bool IsRecordExists(int issueId);
        public bool UpdateCheckBookId(int bookId,int issueId);
        public bool UpdateCheckMemberId(int memberId,int issueId);
        public bool UpdateCheckIssueId(int issueId);
        public bool Save();
    }
}
