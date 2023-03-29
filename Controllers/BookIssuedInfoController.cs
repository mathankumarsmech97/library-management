using AutoMapper;
using LibraryManagementSystem.DTO.MainIssueDto;
using LibraryManagementSystem.Model.MainModel;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookIssuedInfoController : ControllerBase
    {
        private readonly IIssueRepository _issueRepository;
        private readonly IMapper _mapper;

        public BookIssuedInfoController(IIssueRepository issueRepository, IMapper mapper)
        {
            _issueRepository = issueRepository;
            _mapper = mapper;
        }
        [HttpPost("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateIssue(int bookId, int adminId, int memberId, [FromBody] CreateIssueDto mainIssue)
        {
            if (mainIssue == null)
            {
                return BadRequest(ModelState);
            }
            else if (!_issueRepository.IsBookId(bookId))
            {
                return Conflict("This BookId is Invalid");
            }
            else if (!_issueRepository.IsMemberId(memberId))
            {
                return Conflict("Check MemberId is not valid");
            }
            else if (!_issueRepository.Isadmin(adminId))
            {
                return Conflict("Check AdminId is not valid");
            }
            var mainIssueDt = _mapper.Map<MainIssueDetails>(mainIssue);
            var result = _issueRepository.Create(bookId, adminId, memberId, mainIssueDt);
            if (result == 1)
            {
                return Ok("Data is Successfully insert");
            }
            else if (result == 2)
            {
                return Conflict("Book Stock is 0 ");
            }
            else
            {
                ModelState.AddModelError("", "Somrthing went wrong Create a data");
                return StatusCode(500, ModelState);
            }
        }
        [HttpGet("Get all issue Details")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CommonIssueDto>))]
        public IActionResult GetAll()
        {
            var issueDetails = _issueRepository.GetAll();
            var issueDtos = _mapper.Map<List<CommonIssueDto>>(issueDetails);
            if (issueDtos == null)
            {
                return NotFound();
            }
            return Ok(issueDtos);
        }
        [HttpGet("{issueId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CommonIssueDto> GetId(int issueId)
        {
            if (!_issueRepository.IsRecordExists(issueId))
            {
                return NotFound();
            }
            var result = _issueRepository.GetId(issueId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var issueDto = _mapper.Map<CommonIssueDto>(result);
            return Ok(issueDto);
        }
        [HttpPut("{issueId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MainIssueDetails> Update(int issueId, [FromQuery] int bookId, [FromQuery] int memberId, [FromBody] UpdateIssueDto updateIssueDto)
        {
            if (updateIssueDto == null)
            {
                return BadRequest(ModelState);
            }
            else if(issueId != updateIssueDto.issueId)
            {
                return Conflict("IssueId mismatch");
            }
            else if (!_issueRepository.UpdateCheckIssueId(issueId))
            {
                return Conflict("This IssueId is Not Exists");
            }
            else if (!_issueRepository.UpdateCheckBookId(bookId,issueId))
            {
                return Conflict("This bookId is Invalid");
            }
            else if (!_issueRepository.UpdateCheckMemberId(memberId,issueId))
            {
                return Conflict("This MemberId is Invalid");
            }
            var result = _mapper.Map<MainIssueDetails>(updateIssueDto);
            var result2 = _issueRepository.Update(bookId, memberId, result);
            if (!result2)
            {
                ModelState.AddModelError("", "Somrthing went wrong update data");
                return StatusCode(500, ModelState);
            }
            return Ok("Your Data is successfully update");
        }
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int isuueId)
        {
            if (!_issueRepository.IsRecordExists(isuueId))
            {
                return Conflict("This IssueId is Not Exists");
            }
            var result = _issueRepository.GetId(isuueId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_issueRepository.Delete(result))
            {
                ModelState.AddModelError("", "Somethink went wrong Delete data");
            }
            return Ok("Successfully Deleted");
        }
    }
}
