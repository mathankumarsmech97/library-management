using LibraryManagementSystem.Model.MainModel;

namespace LibraryManagementSystem.Model.Returnmodel
{
    public class ConnectionLibraryAdminReturn
    {
        public int ConAdminId { get; set; }
        public int ConReturnId { get; set; }



        public LibraryAdmin LibraryAdmin { get; set; }
        public MainReturnDetails MainReturnDetails { get; set; }
    }
}
