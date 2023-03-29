using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.MemberDto
{
    public class UpdateMemberDto
    {
        [Key]
        public int memberId { get; set; }
        [Required(ErrorMessage = " MemberName Must Need Enter")]
        public string memberName { get; set; }

        [EmailAddress(ErrorMessage = "EmailId Must Need Enter")]
        public string emailId { get; set; }

        [Required(ErrorMessage = "PhoneNumber Must Need Enter")]
        [Range(0, 1000000000)]
        public double phoneNumber { get; set; }
    }
}
