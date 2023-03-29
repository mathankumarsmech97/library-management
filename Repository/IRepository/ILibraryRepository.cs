using LibraryManagementSystem.Model.MainModel;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface ILibraryRepository
    {
        public void Create(LibraryAdmin entity);
        public bool Delete(LibraryAdmin entiry);
        public LibraryAdmin GetId(int id);
        public List<LibraryAdmin> GetAll();
        public bool Update(LibraryAdmin entity);
        bool IsRecordExists(int id);
        bool IsNameExists(string name);
        public ICollection<MainIssueDetails> GetIssues(int issueid);
        public bool Save();
    }
}
