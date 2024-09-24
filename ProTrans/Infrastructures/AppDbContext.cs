using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
           
        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Agency> Agency { get; set; }
        public DbSet<TranslatorSkill> TranslatorSkill { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<QuotePrice> QuotePrice { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<Notarization> Notarization { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<AssignmentTranslation> AssignmentTranslation { get; set; }
        public DbSet<AssignmentNotarization> AssignmentNotarization { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Shipping> Shipping { get; set; }
        public DbSet<FeedBack> FeedBack { get; set; }
        public DbSet<TransactionsHistory> TransactionsHistory { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
