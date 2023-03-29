using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.BookDto
{
    public class CreateBookDto
    {
        [Required]
        public string bookName { get; set; }
        [Required]
        public string bookDescription { get; set; }
        [Required(ErrorMessage = "BookQuantity Must Need Enter")]
        public int quantityBooks { get; set; }
        [Required(ErrorMessage = "AuthorName Must Enter")]
        public string authorName { get; set; }
        [Required(ErrorMessage = "Edition Must Enter")]
        public int edition { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime publishDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AddDateLibrary { get; set; }


    }
}
