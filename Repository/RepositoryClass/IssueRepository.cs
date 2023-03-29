using LibraryManagementSystem.Data;
using LibraryManagementSystem.Model.Issuemodel;
using LibraryManagementSystem.Model.MainModel;
using LibraryManagementSystem.Repository.IRepository;

namespace LibraryManagementSystem.Repository.RepositoryClass
{
    public class IssueRepository: IIssueRepository
    {
        private readonly LibraryDbContext _libraryDb;
        public IssueRepository(LibraryDbContext libraryDb)
        {
            _libraryDb = libraryDb;
        }
        public int Create(int bookid, int adminId, int memberid, MainIssueDetails entity)
        {
            var bookResult = _libraryDb.BookDetails.Where(x => x.BookId == bookid).FirstOrDefault();
            var libraryadminId = _libraryDb.LibraryAdmins.Where(y => y.Id == adminId).FirstOrDefault();
            var memberId = _libraryDb.MembersDetails.Where(z => z.MemberId == memberid).FirstOrDefault();
            var commonEntity = new ConnectioBookIssue()
            {
                BookDetails = bookResult,
                MainIssueDetails = entity
            };
            _libraryDb.Add(commonEntity);
            var commonEntity1 = new ConnectionLibraryAdminIssue()
            {
                LibraryAdmin = libraryadminId,
                MainIssueDetails = entity
            };
            _libraryDb.Add(commonEntity1);
            var commonEntity2 = new ConnectionMemberIssue()
            {
                MembersDetails = memberId,
                MainIssueDetails = entity
            };
            _libraryDb.Add(commonEntity2);
            var result = _libraryDb.BookDetails.Find(bookid);
            if (result.QuantityBooks > 0)
            {
                result.QuantityBooks = result.QuantityBooks - 1;
                _libraryDb.BookDetails.Update(result);
                _libraryDb.Add(entity);
                Save();
                return 1;
            }
            return 2;
        }
        public bool Delete(MainIssueDetails entiry)
        {
            _libraryDb.Remove(entiry);
            return Save();
        }
        public List<MainIssueDetails> GetAll()
        {
            var issue = _libraryDb.MainIssueDetails.ToList();
            return issue;
        }
        public MainIssueDetails GetId(int id)
        {
            var result = _libraryDb.MainIssueDetails.Find(id);
            return result;
        }
        public bool IsMemberId(int memberId)
        {
            var result1 = _libraryDb.MembersDetails.Any(x => x.MemberId == memberId);
            if (result1)
            {
                return true;
            }
            return false;
        }
        public bool IsBookId(int bookId)
        {
            var result2 = _libraryDb.BookDetails.Any(x => x.BookId == bookId);
            if (result2)
            {
                return true;
            }
            return false;
        }
        public bool Isadmin(int adminId)
        {
            var result3 = _libraryDb.LibraryAdmins.Any(x => x.Id == adminId);
            if (result3)
            {
                return true;
            }
            return false;
        }
        public bool Save()
        {
            var r = _libraryDb.SaveChanges();
            return r > 0 ? true : false;
        }
        public bool Update(int BookId, int memberId, MainIssueDetails entity)
        {
            _libraryDb.MainIssueDetails.Update(entity);
            return Save();
        }
        public bool IsRecordExists(int issueId)
        {
            return _libraryDb.MainIssueDetails.Any(x => x.IssueId == issueId);
        }
        public bool UpdateCheckBookId(int bookId,int issueId)
        {
            var result2 = _libraryDb.ConnectioBookIssues.Any(x => x.ConbookId == bookId && x.ConIssueId == issueId);
            if (result2)
            {
                return true;
            }
            return false;
        }
        public bool UpdateCheckMemberId(int memberId, int issueId)
        {
            var result1 = _libraryDb.ConnectionMemberIssue.Any(x => x.ConMemberId == memberId && x.ConIssueId == issueId);
            if (result1)
            {
                return true;
            }
            return false;
        }
        public bool UpdateCheckIssueId(int issueId)
        {
            var result = _libraryDb.MainIssueDetails.Any(x => x.IssueId == issueId);
            if (result)
            {
                return true;
            }
            return false;
        }
    }
}
