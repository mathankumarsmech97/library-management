using LibraryManagementSystem.Model.MainModel;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IBookRepository
    {
        public void Create(BookDetails bookDetails);
        public bool Delete(BookDetails entiry);
        public BookDetails GetId(int id);
        public ICollection<BookDetails> GetAll();
        public bool Update(BookDetails entity);
        bool IsRecordExistsBook(int Id);
        bool IsNameandEditonExists(string name, int edition);
        bool IsNameandEditonExists2(string name, int edition);
        public ICollection<MainIssueDetails> GetIssues(int issueid);
        public ICollection<MainReturnDetails> GetReturnDetails(int returnid);
        public ICollection<BookDetails> NewlyAddBooks();


        public bool Save();
    }
}
