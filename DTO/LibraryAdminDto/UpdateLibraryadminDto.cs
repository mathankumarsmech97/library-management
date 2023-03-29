using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.LibraryAdminDto
{
    public class UpdateLibraryadminDto
    {
        public int id { get; set; }
        [Required]
        public string adminname { get; set; }
        [Required]
        public string shift { get; set; }
    }
}
