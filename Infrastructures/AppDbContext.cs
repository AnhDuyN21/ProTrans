﻿using Domain.Entities;
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
            modelBuilder.Entity<DocumentType>().HasData(
            new DocumentType { Id = Guid.NewGuid(), Name = "Khoa học", PriceFactor = 200000 },
            new DocumentType { Id = Guid.NewGuid(), Name = "Trường học", PriceFactor = 200000 },
            new DocumentType { Id = Guid.NewGuid(), Name = "Hộ chiếu", PriceFactor = 200000 }
            );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ProTransDB;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));
        }

    }
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ProTransDB;User ID=sa;Password=12345;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
