using LibraryManagementSystem.Model.Issuemodel;
using LibraryManagementSystem.Model.MainModel;
using LibraryManagementSystem.Model.Returnmodel;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }
        
            public DbSet<BookDetails> BookDetails { get; set; }

        public DbSet<MembersDetails> MembersDetails { get; set; }

        public DbSet<LibraryAdmin> LibraryAdmins { get; set; }

        public DbSet<MainIssueDetails> MainIssueDetails { get; set; }

        public DbSet<MainReturnDetails> MainReturnDetails { get; set; }

        public DbSet<ConnectioBookIssue> ConnectioBookIssues { get; set; }

        public DbSet<ConnectionLibraryAdminIssue> ConnectionLibraryAdminIssues { get; set; }

        public DbSet<ConnectionMemberIssue> ConnectionMemberIssue { get; set; }

        public DbSet<ConnectionBookReturn> ConnectionBookReturns { get; set; }

        public DbSet<ConnectionMemberReturn> ConnectionMemberReturn { get; set; }

        public DbSet<ConnectionLibraryAdminReturn> ConnectionLibraryAdminReturns { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConnectioBookIssue>()
                .HasKey(pc => new { pc.ConbookId, pc.ConIssueId });
            modelBuilder.Entity<ConnectioBookIssue>()
                .HasOne(p => p.BookDetails)
                .WithMany(pc => pc.ConnectioBookIssues)
                .HasForeignKey(p => p.ConbookId);
            modelBuilder.Entity<ConnectioBookIssue>()
               .HasOne(p => p.MainIssueDetails)
               .WithMany(pc => pc.ConnectioBookIssues)
               .HasForeignKey(c => c.ConIssueId);

            modelBuilder.Entity<ConnectionLibraryAdminIssue>()
               .HasKey(pc => new { pc.ConAdminId, pc.ConIssueId });
            modelBuilder.Entity<ConnectionLibraryAdminIssue>()
                .HasOne(p => p.LibraryAdmin)
                .WithMany(pc => pc.ConnectionLibraryAdminIssues)
                .HasForeignKey(p => p.ConAdminId);
            modelBuilder.Entity<ConnectionLibraryAdminIssue>()
               .HasOne(p => p.MainIssueDetails)
               .WithMany(pc => pc.ConnectionLibraryAdminIssues)
               .HasForeignKey(c => c.ConIssueId);


            modelBuilder.Entity<ConnectionMemberIssue>()
               .HasKey(pc => new { pc.ConMemberId, pc.ConIssueId });
            modelBuilder.Entity<ConnectionMemberIssue>()
                .HasOne(p => p.MembersDetails)
                .WithMany(pc => pc.CoonectionMemberIssues)
                .HasForeignKey(p => p.ConMemberId);
            modelBuilder.Entity<ConnectionMemberIssue>()
               .HasOne(p => p.MainIssueDetails)
               .WithMany(pc => pc.CoonectionMemberIssues)
               .HasForeignKey(c => c.ConIssueId);



            modelBuilder.Entity<ConnectionBookReturn>()
              .HasKey(pc => new { pc.ConbookId, pc.ConReturnId });
            modelBuilder.Entity<ConnectionBookReturn>()
                .HasOne(p => p.BookDetails)
                .WithMany(pc => pc.ConnectionBookReturns)
                .HasForeignKey(p => p.ConbookId);
            modelBuilder.Entity<ConnectionBookReturn>()
               .HasOne(p => p.MainReturnDetails)
               .WithMany(pc => pc.ConnectionBookReturns)
               .HasForeignKey(c => c.ConReturnId);

            modelBuilder.Entity<ConnectionLibraryAdminReturn>()
              .HasKey(pc => new { pc.ConAdminId, pc.ConReturnId });
            modelBuilder.Entity<ConnectionLibraryAdminReturn>()
                .HasOne(p => p.LibraryAdmin)
                .WithMany(pc => pc.ConnectionLibraryAdminReturns)
                .HasForeignKey(p => p.ConAdminId);
            modelBuilder.Entity<ConnectionLibraryAdminReturn>()
               .HasOne(p => p.MainReturnDetails)
               .WithMany(pc => pc.ConnectionLibraryAdminReturns)
               .HasForeignKey(c => c.ConReturnId);

            modelBuilder.Entity<ConnectionMemberReturn>()
             .HasKey(pc => new { pc.ConMemberId, pc.ConReturnId });
            modelBuilder.Entity<ConnectionMemberReturn>()
                .HasOne(p => p.MembersDetails)
                .WithMany(pc => pc.ConnectionMemberReturns)
                .HasForeignKey(p => p.ConMemberId);
            modelBuilder.Entity<ConnectionMemberReturn>()
               .HasOne(p => p.MainReturnDetails)
               .WithMany(pc => pc.ConnectionMemberReturns)
               .HasForeignKey(c => c.ConReturnId);
        }

    }
    
}
