using eReconciliationProject.Core.Concrete;
using eReconciliationProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace eReconciliationProject.DA.Context
{
    public class ProjectContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=MERVE;Database=eReconciliationDB;Integrated Security=True");

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<AccountReconciliationDetail> AccountReconciliationDetails { get; set; }
        public DbSet<AccountReconciliaton> AccountReconciliatons { get; set; }
        public DbSet<BaBsReconciliation> BaBsReconciliations { get; set; }
        public DbSet<BaBsReconciliationDetail> BaBsReconciliationDetails { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyAccount> CurrencyAccounts { get; set; }
        public DbSet<MailParameter> MailParameters { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCompany> UserCompanies { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        public DbSet<MailTemplate> MailTemplates { get; set; }
        public DbSet<TermsandCondition> TermsandConditions { get; set; }
        public DbSet<UserForgotPassword> UserForgotPasswords { get; set; }
        public DbSet<UserRelationship> UserRelationships { get; set; }

    }
}
