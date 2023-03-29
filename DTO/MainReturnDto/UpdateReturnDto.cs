using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.MainReturnDto
{
    public class UpdateReturnDto
    {
        [Key]
        public int returnId { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime returnDateTime { get; set; }
        public double penaltyAmount { get; set; }
    }
}
