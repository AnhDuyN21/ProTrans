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
        public DbSet<Notarization> Notarization { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<AssignmentTranslation> AssignmentTranslation { get; set; }
        public DbSet<AssignmentNotarization> AssignmentNotarization { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Shipping> Shipping { get; set; }
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
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());

        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //base.OnConfiguring(optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ProTransDB;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));
        //}

    }
    //public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    //{
    //    public AppDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    //        optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ProTransDB;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    //        return new AppDbContext(optionsBuilder.Options);
    //    }
    //}
}
