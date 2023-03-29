using LibraryManagementSystem.Model.Returnmodel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Model.MainModel
{
    public class MainReturnDetails
    {
        [Key]
        public int ReturnId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDateTime { get; set; }


        public double PenaltyAmount { get; set; }

        public ICollection<ConnectionBookReturn> ConnectionBookReturns { get; set; }

        public ICollection<ConnectionLibraryAdminReturn> ConnectionLibraryAdminReturns { get; set; }

        public ICollection<ConnectionMemberReturn> ConnectionMemberReturns { get; set; }
    }
}
