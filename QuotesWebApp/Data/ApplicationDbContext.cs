using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuotesWebApp.Models;

namespace QuotesWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext //<ApplicationUser, ApplicationRole, string>
    {
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<QuoteTag> QuoteTags { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 1, Text = "Some quote0.", Date = DateTime.Now });
            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 2, Text = "Some quote1.", Date = DateTime.Now });
            modelBuilder.Entity<Quote>().HasData(new Quote { Id = 3, Text = "Some quote2.", Date = DateTime.Now });

            modelBuilder.Entity<Tag>().HasData(new Tag { Id = 1, Name = "Honzátko", Category = Category.Author });
            modelBuilder.Entity<Tag>().HasData(new Tag { Id = 2, Name = "John Doe", Category = Category.Author });
            modelBuilder.Entity<Tag>().HasData(new Tag { Id = 3, Name = "Jane Doe", Category = Category.Author });

            modelBuilder.Entity<QuoteTag>().HasData(new QuoteTag { QuoteId = 1, TagId = 1 });
            modelBuilder.Entity<QuoteTag>().HasData(new QuoteTag { QuoteId = 2, TagId = 2 });
            modelBuilder.Entity<QuoteTag>().HasData(new QuoteTag { QuoteId = 3, TagId = 3 });

            modelBuilder.Entity<QuoteTag>().HasOne(q => q.Quote).WithMany(qt => qt.Tags).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<QuoteTag>().HasOne(t => t.Tag).WithMany(qt => qt.Quotes).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<QuoteTag>().HasKey(qt => new { qt.QuoteId, qt.TagId });
        }
    }
}
