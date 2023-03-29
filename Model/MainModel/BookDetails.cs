using LibraryManagementSystem.Model.Issuemodel;
using LibraryManagementSystem.Model.Returnmodel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Model.MainModel
{
    public class BookDetails
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        public string BookName { get; set; }

        [Required]
        public string BookDescription { get; set; }

        [Required]
        public int QuantityBooks { get; set; }

        [Required]
        public string AuthorName { get; set; }


        [Required]
        [Range(0, 2)]
        public int Edition { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AddDateLibrary { get; set; }


        public ICollection<ConnectioBookIssue> ConnectioBookIssues { get; set; }

        public ICollection<ConnectionBookReturn> ConnectionBookReturns { get; set; }

    }
}
