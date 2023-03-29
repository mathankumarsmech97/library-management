using LibraryManagementSystem.Data;
using LibraryManagementSystem.Model.MainModel;
using LibraryManagementSystem.Model.Returnmodel;
using LibraryManagementSystem.Repository.IRepository;

namespace LibraryManagementSystem.Repository.RepositoryClass
{
    public class ReturnRepository: IReturnRepository
    {
        private readonly LibraryDbContext _libraryDb;
        public ReturnRepository(LibraryDbContext libraryDb)
        {
            _libraryDb = libraryDb;
        }
        public int Create(int bookid, int memberid,int issueId, MainReturnDetails entity)
        {
            var bookResult = _libraryDb.BookDetails.Where(x => x.BookId == bookid).FirstOrDefault();
            var memberId = _libraryDb.MembersDetails.Where(z => z.MemberId == memberid).FirstOrDefault();

            var commonEntity = new ConnectionBookReturn()
            {
                BookDetails = bookResult,
                MainReturnDetails = entity
            };
            _libraryDb.Add(commonEntity);
            var commonEntity2 = new ConnectionMemberReturn
            {
                MembersDetails = memberId,
                MainReturnDetails = entity
            };
            _libraryDb.Add(commonEntity2);
            _libraryDb.Add(entity);
            if (_libraryDb.MainIssueDetails.Any(x => x.IssueId == issueId && x.ReturnStatement == true))
            {
                var statement = _libraryDb.MainIssueDetails.Find(issueId);
                if(statement.ReturnStatement == true)
                {
                    statement.ReturnStatement = false;
                }
            }

                var result = _libraryDb.BookDetails.Find(bookid);
            if (result.QuantityBooks >= 0)
            {
                result.QuantityBooks = result.QuantityBooks + 1;
                _libraryDb.BookDetails.Update(result);
                _libraryDb.Add(entity);
                Save();
                return 1;
            }
            return 2;
        }
        public bool Delete(MainReturnDetails entiry)
        {
            _libraryDb.Remove(entiry);
            return Save();
        }
        public List<MainReturnDetails> GetAll()
        {
            var mainReturns = _libraryDb.MainReturnDetails.ToList();
            return mainReturns;
        }
        public MainReturnDetails GetId(int id)
        {
            var result = _libraryDb.MainReturnDetails.Find(id);
            return result;
        }
        public bool IsRecordExists(int returnId)
        {
            var result1 = _libraryDb.MainReturnDetails.Any(x => x.ReturnId == returnId);
            return result1;
        }
        public bool Save()
        {
            var save = _libraryDb.SaveChanges();
            return save > 0 ? true : false;
        }
        public bool Update(int BookId, int memberId, MainReturnDetails entity)
        {
            _libraryDb.MainReturnDetails.Update(entity);
            return Save();
        }
        public bool IsMemberId(int memberId,int issueId)
        {
            var result1 = _libraryDb.ConnectionMemberIssue.Any(x => x.ConMemberId == memberId && x.ConIssueId ==issueId);
            if (result1)
            {
                return true;
            }
            return false;
        }
        public bool IsBookId(int bookId,int issueId)
        {
            var result2 = _libraryDb.ConnectioBookIssues.Any(x => x.ConbookId == bookId && x.ConIssueId ==issueId);
            if (result2)
            {
                return true;
            }
            return false;
        }
        public bool Isadmin(int adminId)
        {
            var result3 = _libraryDb.ConnectionLibraryAdminIssues.Any(x => x.ConAdminId == adminId);
            if (result3)
            {
                return true;
            }
            return false;
        }
        public bool IssueId(int issueId)
        {
            if( _libraryDb.MainIssueDetails.Any(x => x.IssueId == issueId && x.ReturnStatement == true))
            {
                return true;
            }
            return false;
        }
        public bool UpdateReturnIdChuck(int returnId)
        {
            var result =_libraryDb.MainReturnDetails.Any(x=>x.ReturnId== returnId);
            if(result)
            {
                return true;
            }
            return false;
        }
        public bool UpdateBookIdChuck(int bookId)
        {
            var result =_libraryDb.ConnectionBookReturns.Any(x=> x.ConbookId== bookId);
            if (result)
            {
                return true;
            }
            return false;
        }
        public bool UpdateMemberIDChuck(int memberId)
        {
            var result =_libraryDb.ConnectionMemberReturn.Any(x=> x.ConMemberId==memberId);
            if (result)
            {
                return true;
            }
            return false;
        }
    }
}
