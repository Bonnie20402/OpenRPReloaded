using Microsoft.EntityFrameworkCore;
using OpenRPReloaded.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenRPReloaded.Contexts
{
    public class OpenRPContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Character> Characters {get; set;}
        public string DbPath { get; }

        public OpenRPContext()
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database");
            Directory.CreateDirectory(folder);
            DbPath = Path.Join(folder, "openrp.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlite($"Data Source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(a => a.AccountID)
                .HasConversion(
                    v => v.ToString(), // Guid -> string
                    v => Guid.Parse(v) // string -> Guid
                );
            modelBuilder.Entity<Character>()
                .HasOne(e => e.Account)
                .WithMany(e => e.Characters)
                .HasForeignKey(e => e.AccountID)
                .IsRequired();     
        }
    }
}
