using AutoMapper;
using LibraryManagementSystem.DTO.LibraryAdminDto;
using LibraryManagementSystem.DTO.MainIssueDto;
using LibraryManagementSystem.DTO.MainReturnDto;
using LibraryManagementSystem.Model.MainModel;
using LibraryManagementSystem.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryAdminInfoController : ControllerBase
    {
        private readonly ILibraryRepository _libraryRepository;
        private readonly IMapper _mapper;
        public LibraryAdminInfoController(ILibraryRepository libraryRepository, IMapper mapper)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
        }
        [HttpGet("Get All AdminDetails")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CommonLibraryAdminDto>> GetAll()
        {
            var libraryDetails = _libraryRepository.GetAll();
            var libraryDetail = _mapper.Map<List<CommonLibraryAdminDto>>(libraryDetails);
            if (libraryDetail == null)
            {
                return NotFound();
            }
            return Ok(libraryDetail);
        }
        [HttpGet("{libraryAdminId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<CommonLibraryAdminDto> GetId(int libraryAdminId)
        {
            if (!_libraryRepository.IsRecordExists(libraryAdminId))
            {
                return Conflict("This AdminId is Not Exists");
            }
            var lbAdmin = _libraryRepository.GetId(libraryAdminId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _mapper.Map<CommonLibraryAdminDto>(lbAdmin);
            return Ok(result);
        }
        [HttpGet("Admin Issue Details/{libraryAdminId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CommonIssueDto>> GetIssue(int libraryAdminId)
        {
            if (!_libraryRepository.IsRecordExists(libraryAdminId))
            {
                return NotFound();
            }
            var admin = _mapper.Map<List<CommonIssueDto>>(_libraryRepository.GetIssues(libraryAdminId));
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
        public ActionResult<LibraryAdmin> Create([FromBody] CreateLibraryAdminDto createLibraryAdmin)
        {
            var result = _libraryRepository.IsNameExists(createLibraryAdmin.adminname);
            if (result)
            {
                return Conflict("This admin Name  is Already Exists in Table");
            }
            var admin = _mapper.Map<LibraryAdmin>(createLibraryAdmin);
            _libraryRepository.Create(admin);
            return Ok(admin);
        }
        [HttpPut("{libraryAdminId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LibraryAdmin> Update(int libraryAdminId, [FromBody] UpdateLibraryadminDto libraryadminDto)
        {
            if (libraryadminDto == null)
            {
                return BadRequest(ModelState);
            }
            else if(libraryAdminId != libraryadminDto.id)
            {
                return Conflict("AdminId Mismatch");
            }
           else if (!_libraryRepository.IsRecordExists(libraryAdminId))
            {
                return Conflict("This AdminId is Not Exists");
            }
            var result = _mapper.Map<LibraryAdmin>(libraryadminDto);
            var result2 = _libraryRepository.Update(result);
            if (!result2)
            {
                ModelState.AddModelError("", "Somrthing went wrong update data");
                return StatusCode(500, ModelState);
            }
            return Ok("data is Successfully updated");
        }
        [HttpDelete("Delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int adminId)
        {
            if (!_libraryRepository.IsRecordExists(adminId))
            {
                return Conflict("This AdminId is Not Exists");
            }
            var result = _libraryRepository.GetId(adminId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_libraryRepository.Delete(result))
            {
                ModelState.AddModelError("", "Somethink went wrong Delete data");
            }
            return Ok("Data Successfully Deleted");
        }
    }
}
