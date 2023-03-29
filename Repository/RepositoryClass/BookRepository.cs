using LibraryManagementSystem.Data;
using LibraryManagementSystem.Model.MainModel;
using LibraryManagementSystem.Model.Report;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;

namespace LibraryManagementSystem.Repository.RepositoryClass
{
    public class Book
    {
        public OftenBook MostRead(SqlConnection sqlConnection)
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("MostReadBook", sqlConnection);
            OftenBook oftenBook = new OftenBook();
            DataTable dataTable = new DataTable();
            MostReadedBook ReadBooks = new MostReadedBook();
            sqlDataAdapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; dataTable.Rows.Count > i; i++)
                {
                    MostReadedBook mostBookRead = new MostReadedBook();
                    mostBookRead.BookId = Convert.ToInt32(dataTable.Rows[i]["ConbookId"]);
                    mostBookRead.Ordercount = Convert.ToInt32(dataTable.Rows[i]["MostOrdercount"]);
                    oftenBook.MostBookRead = mostBookRead;
                }
            }
            return oftenBook;
        }
    }
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _libraryDb;
        public BookRepository(LibraryDbContext libraryDb)
        {
            _libraryDb = libraryDb;
        }
        public void Create(BookDetails entity)
        {
            _libraryDb.Add(entity);
            Save();
        }
        public bool Delete(BookDetails entiry)
        {
            _libraryDb.BookDetails.Remove(entiry);
            return Save();
        }
        public ICollection<BookDetails> GetAll()
        {
            var book = _libraryDb.BookDetails.ToList();
            return book;
        }
        public BookDetails GetId(int id)
        {
            var Result = _libraryDb.BookDetails.Find(id);
            return Result;
        }
        public ICollection<MainIssueDetails> GetIssues(int bookId)
        {
            return _libraryDb.ConnectioBookIssues.Where(x => x.ConbookId == bookId).Select(p => p.MainIssueDetails).ToList();
        }
        public ICollection<MainReturnDetails> GetReturnDetails(int bookId)
        {
            return _libraryDb.ConnectionBookReturns.Where(x => x.ConbookId == bookId).Select(p => p.MainReturnDetails).ToList();
        }
        public bool IsNameandEditonExists(string name, int edition)
        {
            int count = 0;
            List<BookDetails> bookDetails = new List<BookDetails>();
            bookDetails = _libraryDb.BookDetails.ToList();
            foreach (var book in bookDetails)
            {
                if (book.BookName.ToLower().Trim() == name.ToLower().Trim())
                {
                    if (book.Edition == edition)
                    {
                        count++;
                    }
                }
            }
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        public bool IsNameandEditonExists2(string name, int edition)
        {
            int count = 0;
            if (_libraryDb.BookDetails.Any(x => x.BookName.ToLower().Trim() == name.ToLower().Trim()))
            {
                if (_libraryDb.BookDetails.Any(x => x.Edition == edition))
                {
                    count++;
                }
            }
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        public bool IsRecordExistsBook(int id)
        {
            var result1 = _libraryDb.BookDetails.Any(x => x.BookId == id);
            return result1;
        }

        public ICollection<BookDetails> NewlyAddBooks()
        {
            var book = _libraryDb.BookDetails.Where(x => x.AddDateLibrary.Month > 02 && x.AddDateLibrary.Year > 2022).ToList();
            return book;
        }

        public bool Save()
        {
            var result = _libraryDb.SaveChanges();
            return result > 0 ? true : false;
        }

        public bool Update(BookDetails entity)
        {
            _libraryDb.BookDetails.Update(entity);
            return Save();
        }
    }
}
