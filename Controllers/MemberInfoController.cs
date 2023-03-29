using AutoMapper;
using LibraryManagementSystem.DTO.BookDto;
using LibraryManagementSystem.DTO.MainIssueDto;
using LibraryManagementSystem.DTO.MainReturnDto;
using LibraryManagementSystem.DTO.MemberDto;
using LibraryManagementSystem.Model.MainModel;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberInfoController : ControllerBase
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        public MemberInfoController(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }
        [HttpGet("Get All MemberDetails")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CommonMemberDto>> GetAll()
        {
            var member = _memberRepository.GetAll();
            var result = _mapper.Map<List<CommonMemberDto>>(member);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("MemberDetailsIdBased{memberId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CommonMemberDto> GetId(int memberId)
        {
            if (!_memberRepository.IsRecordExists(memberId))
            {
                return Conflict("MemberId is Not valid");
            }
            var member = _memberRepository.GetId(memberId);
            if (!ModelState.IsValid)
            {
                return Conflict("Get Method Error");
            }
            var member1 = _mapper.Map<CommonMemberDto>(member);
            return Ok(member1);
        }
        [HttpGet("MemberIssueDetails/{memberId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CommonIssueDto>> GetIssue(int memberId)
        {
            if (!_memberRepository.IsRecordExists(memberId))
            {
                return NotFound();
            }
            var admin = _mapper.Map<List<CommonIssueDto>>(_memberRepository.GetIssues(memberId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(admin);
        }
        [HttpGet("MemberReturnDetailsBased/{memberId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CommonReturnDto>> GetReturn(int memberId)
        {
            if (!_memberRepository.IsRecordExists(memberId))
            {
                return NotFound();
            }
            var admin = _mapper.Map<List<CommonReturnDto>>(_memberRepository.GetReturnDetails(memberId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(admin);
        }
        [HttpPost("Post")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MembersDetails> Create([FromBody] CreateMemberDto createMemberdto)
        {
            var result = _memberRepository.IsPhoneNumberAndEmail(createMemberdto.emailId, createMemberdto.phoneNumber);
            if (result)
            {
                return Conflict("Check EmailId or PhoneNumber  is Already Exists in Table");
            }
            var members = _mapper.Map<MembersDetails>(createMemberdto);
            _memberRepository.Create(members);
            return Ok(members);
        }
        [HttpPut("Put/{memberId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MembersDetails> Update(int memberId, [FromBody] UpdateMemberDto updateMemberDto)
        {
            if (updateMemberDto == null)
            {
                return BadRequest(ModelState);
            }
          else if(memberId != updateMemberDto.memberId)
            {
                return Conflict("MemberId mismatch");
            }
          else if (!_memberRepository.IsRecordExists(memberId))
            {
                return Conflict("This MemberId is Not Exists");
            }
            var result = _mapper.Map<MembersDetails>(updateMemberDto);
            var result2 = _memberRepository.Update(result);
            if (!result2)
            {
                ModelState.AddModelError("", "Somrthing went wrong update data");
                return StatusCode(500, ModelState);
            }
            return Ok("Data successfully updated");
        }
        [HttpDelete("Delete/{memberId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int memberId)
        {
            if (!_memberRepository.IsRecordExists(memberId))
            {
                return Conflict("This memberId is not exists");
            }
            var result = _memberRepository.GetId(memberId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_memberRepository.Delete(result))
            {
                ModelState.AddModelError("", "Somethink went wrong delete data");
            }
            return Ok("Data successfully Deleted");
        }
    }
}
