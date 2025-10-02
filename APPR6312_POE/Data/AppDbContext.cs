using APPR6312_POE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APPR6312_POE.Data;

namespace APPR6312_POE.Data
{
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<User> Users { get; set; }
            public DbSet<Donation> Donations { get; set; }
            public DbSet<DisasterReport> DisasterReports { get; set; }
            public DbSet<Volunteer> Volunteers { get; set; }
            public DbSet<VolunteerTask> VolunteerTasks { get; set; }
            public DbSet<VolunteerTaskAssignment> VolunteerTaskAssignments { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                // Configure composite key for VolunteerTaskAssignment
                modelBuilder.Entity<VolunteerTaskAssignment>()
                    .HasKey(vt => new { vt.VolunteerId, vt.TaskId });

                // Optional: Map property names if DB columns differ
                modelBuilder.Entity<DisasterReport>()
                    .Property(d => d.DisasterDescription)
                    .HasColumnName("DisasterDescription");

                modelBuilder.Entity<DisasterReport>()
                    .Property(d => d.DisasterLocation)
                    .HasColumnName("DisasterLocation");

                modelBuilder.Entity<Donation>()
                    .Property(d => d.DonationLocation)
                    .HasColumnName("DonationLocation");
            }
        }
    }

