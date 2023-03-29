using LibraryManagementSystem.Data;
using LibraryManagementSystem.Model.MainModel;
using LibraryManagementSystem.Repository.IRepository;

namespace LibraryManagementSystem.Repository.RepositoryClass
{
    public class LibraryRepository: ILibraryRepository
    {
        private readonly LibraryDbContext _libraryDb;
        public LibraryRepository(LibraryDbContext libraryDb)
        {
            _libraryDb = libraryDb;
        }
        public void Create(LibraryAdmin entity)
        {
            _libraryDb.Add(entity);
            Save();
        }
        public bool Delete(LibraryAdmin entiry)
        {
            _libraryDb.LibraryAdmins.Remove(entiry);
            return Save();
        }
        public List<LibraryAdmin> GetAll()
        {
            var lbAdmin = _libraryDb.LibraryAdmins.ToList();
            return lbAdmin;
        }
        public LibraryAdmin GetId(int id)
        {
            var Result = _libraryDb.LibraryAdmins.Find(id);
            return Result;
        }
        public ICollection<MainIssueDetails> GetIssues(int adminId)
        {
            return _libraryDb.ConnectionLibraryAdminIssues.Where(x => x.ConAdminId == adminId).Select(p => p.MainIssueDetails).ToList();
        }
       
        public bool IsNameExists(string name)
        {
            var result = _libraryDb.LibraryAdmins.Any(x => x.Adminname.ToLower().Trim() == name.ToLower().Trim());
            return result;
        }
        public bool IsRecordExists(int id)
        {
            var result1 = _libraryDb.LibraryAdmins.Any(x => x.Id == id);
            return result1;
        }
        public bool Save()
        {
            var result = _libraryDb.SaveChanges();
            return result > 0 ? true : false;
        }
        public bool Update(LibraryAdmin entity)
        {
            _libraryDb.Update(entity);
            return Save();
        }
    }
}
