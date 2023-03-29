using LibraryManagementSystem.Model.Issuemodel;
using LibraryManagementSystem.Model.Returnmodel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Model.MainModel
{
    public class LibraryAdmin
    {
        public int Id { get; set; }
        [Required]
        public string Adminname { get; set; }
        [Required]
        public string Shift { get; set; }

        public ICollection<ConnectionLibraryAdminIssue> ConnectionLibraryAdminIssues { get; set; }

        public ICollection<ConnectionLibraryAdminReturn> ConnectionLibraryAdminReturns { get; set; }
    }
}
