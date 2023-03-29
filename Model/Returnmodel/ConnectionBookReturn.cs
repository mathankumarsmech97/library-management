using LibraryManagementSystem.Model.MainModel;

namespace LibraryManagementSystem.Model.Returnmodel
{
    public class ConnectionBookReturn
    {
        public int ConbookId { get; set; }
        public int ConReturnId { get; set; }



        public BookDetails BookDetails { get; set; }
        public MainReturnDetails MainReturnDetails { get; set; }
    }
}
