using Microsoft.EntityFrameworkCore;
using OpenRPReloaded.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OpenRPReloaded.Contexts
{
    public class AccountsContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public string DbPath { get; }


        public AccountsContext()
        {
            SQLitePCL.Batteries.Init();
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"database");
            Directory.CreateDirectory(folder);
            DbPath = System.IO.Path.Join(folder, "accounts.db");
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
        }

    }
}
