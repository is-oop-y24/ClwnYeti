using Microsoft.EntityFrameworkCore;
using Reports.Core.Entities;

namespace Reports.Application.Database
{
    public sealed class ReportsDatabaseContext : DbContext
    {
        public ReportsDatabaseContext(DbContextOptions<ReportsDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
            Employees.Load();
            Tasks.Load();
            Comments.Load();
            Reports.Load();
            Modifications.Load();
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<ReportTask> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Modification> Modifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasOne(e => e.Mentor);
            modelBuilder.Entity<ReportTask>().HasOne(t => t.Employee);
            modelBuilder.Entity<Comment>().HasOne(c => c.Task);
            modelBuilder.Entity<Report>().HasOne(r => r.Employee);
            modelBuilder.Entity<Modification>().HasOne(m => m.Employee);
            modelBuilder.Entity<Modification>().HasOne(m => m.Task);
        }
    }
}