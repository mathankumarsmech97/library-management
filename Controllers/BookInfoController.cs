using AutoMapper;
using LibraryManagementSystem.DTO.BookDto;
using LibraryManagementSystem.DTO.MainIssueDto;
using LibraryManagementSystem.DTO.MainReturnDto;
using LibraryManagementSystem.Model.MainModel;
using LibraryManagementSystem.Model.Report;
using LibraryManagementSystem.Repository.IRepository;
using LibraryManagementSystem.Repository.RepositoryClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookInfoController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public BookInfoController(IBookRepository bookRepository, IMapper mapper, IConfiguration configuration)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
       
        [HttpGet("MostBookRead")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public MostReadedBook MostBook()
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
            OftenBook oftenBook = new OftenBook();
            Book bookRead = new Book();

            oftenBook = bookRead.MostRead(sqlConnection);
            return oftenBook.MostBookRead;
        }
        [HttpGet]
        [Route("GetAllBookDetails")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CommonBookDto>> GetAll()
        {
            var book = _bookRepository.GetAll();

            var books = _mapper.Map<List<CommonBookDto>>(book);

            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }
        [HttpGet("GetBookdetailIdBased/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CommonBookDto> GetId(int Id)
        {

            if (!_bookRepository.IsRecordExistsBook(Id))
            {
                return NotFound();
            }
            var book = _bookRepository.GetId(Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var books = _mapper.Map<CommonBookDto>(book);

            return Ok(books);

        }
        [HttpGet("BookIssueDetailsIdBased/{bookid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CommonIssueDto>> GetIssue(int bookid)
        {

            var book = _mapper.Map<List<CommonIssueDto>>(_bookRepository.GetIssues(bookid));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(book);
        }
        [HttpGet("BookReturnDetailsIdBased/{bookid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CommonReturnDto>> GetReturn(int bookid)
        {
            if (!_bookRepository.IsRecordExistsBook(bookid))
            {
                return NotFound();
            }
            var book = _mapper.Map<List<CommonReturnDto>>(_bookRepository.GetReturnDetails(bookid));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(book);
        }
        [HttpPost("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public ActionResult<BookDetails> Create([FromBody] CreateBookDto bookDetailsDto)
        {
            var result = _bookRepository.IsNameandEditonExists(bookDetailsDto.bookName, bookDetailsDto.edition);

            if (result)
            {
                return Conflict("This book name or Edition is Already Exists");
            }
            else if (!(bookDetailsDto.quantityBooks > 0))
            {
                return Conflict("Quantity of Book Not Be Zero");
            }
            else if (!(bookDetailsDto.edition > 0))
            {
                return Conflict("Quantity Edition Not Be Zero");

            }
            var book = _mapper.Map<BookDetails>(bookDetailsDto);

            _bookRepository.Create(book);

            return Ok(book);
        }
        [HttpPut("Put/{bookId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BookDetails> Update(int bookId, [FromBody] UpdateBookDto updateBookDto)
        {
            if (updateBookDto == null)
            {
                return BadRequest(ModelState);
            }
            else if(bookId != updateBookDto.bookId)
            {
                return Conflict("BookId Mismatch");
            }
            else if (!_bookRepository.IsRecordExistsBook(bookId))
            {
                return Conflict("This BookId is Not Exists");
            }
            else if (!(updateBookDto.quantityBooks > 0))
            {
                return Conflict("Quantity of Book Not Be Zero");
            }
            var result = _mapper.Map<BookDetails>(updateBookDto);

            var result2 = _bookRepository.Update(result);

            if (!result2)
            {
                ModelState.AddModelError("", "Somrthing went wrong update data");
                return StatusCode(500, ModelState);
            }
            return Ok("Book Details Successfully updated");
        }
        [HttpGet]
        [Route("NewlyAddedBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CommonBookDto>> NewlyAddBooks()
        {
            var book = _bookRepository.NewlyAddBooks();

            var books = _mapper.Map<List<CommonBookDto>>(book);

            if (books == null)
            {
                return NotFound();
            }
            return Ok(books);
        }

        [HttpDelete("Delete/{bookId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int bookId)
        {
            if (!_bookRepository.IsRecordExistsBook(bookId))
            {
                return Conflict("This BookId is Not Exists");
            }
            var result = _bookRepository.GetId(bookId);
            if (!_bookRepository.Delete(result))
            {
                ModelState.AddModelError("", "Somethink went wrong Delete data");
            }
            return Ok("BookDetails is Successfully Deleted");
        }
    }
}
