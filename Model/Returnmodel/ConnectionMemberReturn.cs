using LibraryManagementSystem.Model.MainModel;

namespace LibraryManagementSystem.Model.Returnmodel
{
    public class ConnectionMemberReturn
    {
        public int ConMemberId { get; set; }

        public int ConReturnId { get; set; }


        public MembersDetails MembersDetails { get; set; }
        public MainReturnDetails MainReturnDetails { get; set; }
    }
}
