using LibraryManagementSystem.Model.MainModel;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IReturnRepository
    {
        public int Create(int bookid, int memberid,int issueId, MainReturnDetails entity);
        public bool Delete(MainReturnDetails entiry);
        public MainReturnDetails GetId(int id);
        public List<MainReturnDetails> GetAll();
        public bool Update(int bookId, int memberId, MainReturnDetails entity);
        public bool Isadmin(int adminId);
        public bool IsBookId(int bookId,int issueId);
        public bool IsMemberId(int memberId,int issueId);
        public bool IssueId(int issueId);
        bool IsRecordExists(int issue);
        bool UpdateReturnIdChuck(int returnId);
        bool UpdateBookIdChuck(int bookId );
        bool UpdateMemberIDChuck(int memberID);
        public bool Save();
    }
}
