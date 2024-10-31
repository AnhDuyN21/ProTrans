using Domain.Entities;
using Infrastructures.FluentAPIs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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
        public DbSet<TranslationSkill> TranslationSkill { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<QuotePrice> QuotePrice { get; set; }
        public DbSet<Document> Document { get; set; }
        public DbSet<DocumentType> DocumentType { get; set; }
        public DbSet<DocumentHistory> DocumentHistory { get; set; }
        public DbSet<DocumentPrice> DocumentPrice { get; set; }
        public DbSet<Notarization> Notarization { get; set; }
        public DbSet<NotarizationDetail> NotarizationDetail { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ImageShipping> ImageShipping { get; set; }
        public DbSet<AssignmentTranslation> AssignmentTranslation { get; set; }
        public DbSet<AssignmentNotarization> AssignmentNotarization { get; set; }
        public DbSet<AssignmentShipping> AssignmentShipping { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new QuotePriceConfiguration());
            modelBuilder.ApplyConfiguration(new RequestConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new NotarizationConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());

        }

    }

}
