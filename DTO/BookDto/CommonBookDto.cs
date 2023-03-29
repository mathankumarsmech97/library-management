using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTO.BookDto
{
    public class CommonBookDto
    {
        public int bookId { get; set; }
        public string bookName { get; set; }
        public string bookDescription { get; set; }
        public int quantityBooks { get; set; }
        public string authorName { get; set; }
        public int edition { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

        public DateTime AddDateLibrary { get; set; }

    }
}
