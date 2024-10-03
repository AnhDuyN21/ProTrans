using Domain.Entities;
using Infrastructures.FluentAPIs;
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new DocumentConfiguration());
            modelBuilder.ApplyConfiguration(new QuotePriceConfiguration());
            modelBuilder.ApplyConfiguration(new RequestConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new NotarizationConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new AttachmentConfiguration());

            modelBuilder.Entity<Role>().HasData(
            new Role { Id = Guid.NewGuid(), Name = "Admin" },
            new Role { Id = Guid.NewGuid(), Name = "Customer" },
            new Role { Id = Guid.NewGuid(), Name = "Shipper" },
            new Role { Id = Guid.NewGuid(), Name = "Manager" },
            new Role { Id = Guid.NewGuid(), Name = "Staff" },
            new Role { Id = Guid.NewGuid(), Name = "Translator" }
            );
            modelBuilder.Entity<Notarization>().HasData(
            new Notarization { Id = Guid.NewGuid(), Name = "Công chứng bản dịch tiếng Anh", Price = 500000 },    
            new Notarization { Id = Guid.NewGuid(), Name = "Công chứng bản dịch tiếng Pháp", Price = 500000 },    
            new Notarization { Id = Guid.NewGuid(), Name = "Công chứng bản dịch tiếng Nhật", Price = 500000 },    
            new Notarization { Id = Guid.NewGuid(), Name = "Công chứng bản dịch tiếng Trung", Price = 500000 }    
            );
        }
    }
}
