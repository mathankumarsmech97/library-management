using LibraryManagementSystem.Model.MainModel;

namespace LibraryManagementSystem.Repository.IRepository
{
    public interface IMemberRepository
    {
        public void Create(MembersDetails entity);
        public bool Delete(MembersDetails entiry);
        public MembersDetails GetId(int id);
        public List<MembersDetails> GetAll();
        public bool Update(MembersDetails entity);
        bool IsRecordExists(int id);
        bool IsPhoneNumberAndEmail(string email, double phoneNumber);
        public ICollection<MainIssueDetails> GetIssues(int issueid);
        public ICollection<MainReturnDetails> GetReturnDetails(int returnid);
        public bool Save();
    }
}
