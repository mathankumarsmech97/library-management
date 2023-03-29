using AutoMapper;
using LibraryManagementSystem.DTO.BookDto;
using LibraryManagementSystem.DTO.LibraryAdminDto;
using LibraryManagementSystem.DTO.MainIssueDto;
using LibraryManagementSystem.DTO.MainReturnDto;
using LibraryManagementSystem.DTO.MemberDto;
using LibraryManagementSystem.Model.MainModel;

namespace LibraryManagementSystem.Common
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDetails, CommonBookDto>().ReverseMap();
            CreateMap<BookDetails, CreateBookDto>().ReverseMap();
            CreateMap<BookDetails, UpdateBookDto>().ReverseMap();

            CreateMap<MembersDetails, CommonMemberDto>().ReverseMap();
            CreateMap<MembersDetails, CreateMemberDto>().ReverseMap();
            CreateMap<MembersDetails, UpdateMemberDto>().ReverseMap();

            CreateMap<LibraryAdmin, CommonLibraryAdminDto>().ReverseMap();
            CreateMap<LibraryAdmin, CreateLibraryAdminDto>().ReverseMap();
            CreateMap<LibraryAdmin, UpdateLibraryadminDto>().ReverseMap();

            CreateMap<MainIssueDetails, CommonIssueDto>().ReverseMap();
            CreateMap<MainIssueDetails, CreateIssueDto>().ReverseMap();
            CreateMap<MainIssueDetails, UpdateIssueDto>().ReverseMap();

            CreateMap<MainReturnDetails, CommonReturnDto>().ReverseMap();
            CreateMap<MainReturnDetails, CreateReturnDto>().ReverseMap();
            CreateMap<MainReturnDetails, UpdateReturnDto>().ReverseMap();
        }

    }
}
