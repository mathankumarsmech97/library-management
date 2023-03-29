using LibraryManagementSystem.Data;
using LibraryManagementSystem.Model.MainModel;
using LibraryManagementSystem.Repository.IRepository;

namespace LibraryManagementSystem.Repository.RepositoryClass
{
    public class MemberRepository: IMemberRepository
    {
        private readonly LibraryDbContext _libraryDb;
        public MemberRepository(LibraryDbContext libraryDbContext)
        {
            _libraryDb = libraryDbContext;
        }
        public void Create(MembersDetails entity)
        {
            _libraryDb.Add(entity);
            Save();
        }
        public bool Delete(MembersDetails entiry)
        {
            _libraryDb.MembersDetails.Remove(entiry);
            return Save();
        }
        public List<MembersDetails> GetAll()
        {
            var member = _libraryDb.MembersDetails.ToList();
            return member;
        }
        public MembersDetails GetId(int id)
        {
            var member = _libraryDb.MembersDetails.Find(id);
            return member;
        }
        public bool IsPhoneNumberAndEmail(string email, double phoneNumber)
        {
            if (_libraryDb.MembersDetails.Any(x => x.EmailId.Trim() == email.Trim()) && _libraryDb.MembersDetails.Any(x => x.PhoneNumber == phoneNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public ICollection<MainIssueDetails> GetIssues(int memberId)
        {
            return _libraryDb.ConnectionMemberIssue.Where(x => x.ConMemberId == memberId).Select(p => p.MainIssueDetails).ToList();
        }
        public ICollection<MainReturnDetails> GetReturnDetails(int mamberId)
        {
            return _libraryDb.ConnectionMemberReturn.Where(x => x.ConMemberId == mamberId).Select(p => p.MainReturnDetails).ToList();
        }
        public bool IsRecordExists(int id)
        {
            var result1 = _libraryDb.MembersDetails.Any(x => x.MemberId == id);
            return result1;
        }

        public bool Save()
        {
            var result = _libraryDb.SaveChanges();
            return result > 0 ? true : false;
        }
        public bool Update(MembersDetails entity)
        {
            _libraryDb.Update(entity);
            return Save();
        }
    }
}
