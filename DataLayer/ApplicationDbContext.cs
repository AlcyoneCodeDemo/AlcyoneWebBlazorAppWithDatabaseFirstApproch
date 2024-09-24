using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Define DbSets for your entities
        public DbSet<User> Users { get; set; }
        public DbSet<Holiday> Holiday { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<ExperienceInformation> ExperienceInformations { get; set; }
        public DbSet<BankInformation> BankInformation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .Property(e => e.emergency_contact_details)
            .HasColumnType("jsonb")
            .HasConversion(
                v => v, // No conversion necessary, stored as string
                v => v  // Retrieved as string
            );

            // Define primary key using Fluent API
            modelBuilder.Entity<User>()
               .HasKey(e => e.user_id);

            modelBuilder.Entity<Holiday>()
               .HasKey(e => e.holiday_id);

            modelBuilder.Entity<ExperienceInformation>()
                .HasKey(e => e.ExperienceId);

            modelBuilder.Entity<Education>()
                .HasKey(e => e.education_id);

            // Define primary key for other entities
            modelBuilder.Entity<BankInformation>()
                .HasKey(b => b.bank_id);

            modelBuilder.Entity<BankInformation>()
                .HasOne(b => b.User)
                .WithMany()
                .HasForeignKey(b => b.user_id)
                .OnDelete(DeleteBehavior.Cascade);
            // Additional entity configurations
        }

    }

}
