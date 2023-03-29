using AutoMapper;
using LibraryManagementSystem.DTO.MainReturnDto;
using LibraryManagementSystem.Model.MainModel;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookReturnInfoController : ControllerBase
    {
        private readonly IReturnRepository _returnRepository;
        private readonly IMapper _mapper;
        public BookReturnInfoController(IReturnRepository returnRepository, IMapper mapper)
        {
            _returnRepository = returnRepository;
            _mapper = mapper;
        }
        [HttpGet("Get all Return Details")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommonReturnDto>))]
        public IActionResult GetAll()
        {
            var returnDetails = _returnRepository.GetAll();
            var returnDtos = _mapper.Map<List<CommonReturnDto>>(returnDetails);
            if (returnDtos == null)
            {
                return NotFound();
            }
            return Ok(returnDtos);
        }
        [HttpPost("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateIssue([FromQuery] int bookId, [FromQuery] int memberId, [FromQuery] int issueId, [FromBody] CreateReturnDto createReturnDto)
        {
            if (createReturnDto == null)
                return BadRequest(ModelState);
            if (!_returnRepository.IsBookId(bookId,issueId))
            {
                return Conflict("This bookId or IssueId is Invalid");
            }
            else if (!_returnRepository.IsMemberId(memberId,issueId))
            {
                return Conflict("This MemberId or IssueId is Invalid");
            }

             if (!_returnRepository.IssueId(issueId))
            {
                return Conflict("This IssueId is Invalid or Already return ");
            }
            
            var mainReturnDto = _mapper.Map<MainReturnDetails>(createReturnDto);


            var result = _returnRepository.Create(bookId, memberId,issueId, mainReturnDto);
            if (result == 1)
            {
                return Ok("Data is Successfully insert");
            }
            else if (result == 2)
            {
                ModelState.AddModelError("", "somr Went Wrong to save data");
                return StatusCode(500, ModelState);
            }
            return Ok();
        }
        [HttpGet("{returnId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CommonReturnDto> GetId(int returnId)
        {
            if (!_returnRepository.IsRecordExists(returnId))
            {
                return Conflict("ReturnId is Not Exists");
            }
            var result = _returnRepository.GetId(returnId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var returnDto = _mapper.Map<CommonReturnDto>(result);
            return Ok(returnDto);
        }
        [HttpPut("{returnId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MainReturnDetails> Update(int returnId, [FromQuery] int bookId, [FromQuery] int memberId, [FromBody] UpdateReturnDto updateReturn)
        {
            if (updateReturn == null)
            {
                return BadRequest(ModelState);
            }
            else if(returnId!= updateReturn.returnId)
            {
                return Conflict("ReturnId MisMatch");
            }
            else if (!_returnRepository.UpdateReturnIdChuck(returnId))
            {
                return Conflict("ReturnId is not exists");
            }
            else if(!_returnRepository.UpdateBookIdChuck(bookId))
            {
                return Conflict("BookId is not exists");
            }
            else if (!_returnRepository.UpdateMemberIDChuck(memberId))
            {
                return Conflict("MemberId is not exists");
            }
            var result = _mapper.Map<MainReturnDetails>(updateReturn);
            var result2 = _returnRepository.Update(bookId, memberId, result);
            if (!result2)
            {
                ModelState.AddModelError("", "Somrthing went wrong update data");
                return StatusCode(500, ModelState);
            }
            return Ok("Data is Successfully updated");
        }
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int returnId)
        {
            if (!_returnRepository.IsRecordExists(returnId))
            {
                return Conflict("This ReturnId is not exists");
            }
            var result = _returnRepository.GetId(returnId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_returnRepository.Delete(result))
            {
                ModelState.AddModelError("", "Somethink went wrong Delete data");
            }
            return Conflict("Data is successfully deleted");
        }
    }
}
