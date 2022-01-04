using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            EntityTypeBuilder<Employee> employees = modelBuilder.Entity<Employee>();
            employees.ToTable("Employees");
            employees.HasOne(e => e.Mentor);
            EntityTypeBuilder<ReportTask> reportTasks = modelBuilder.Entity<ReportTask>();
            reportTasks.ToTable("Tasks");
            reportTasks.HasOne(t => t.Employee);
            EntityTypeBuilder<Comment> comments = modelBuilder.Entity<Comment>();
            comments.ToTable("Comments");
            comments.HasOne(c => c.Task);
            EntityTypeBuilder<Report> reports = modelBuilder.Entity<Report>();
            reports.ToTable("Reports");
            reports.HasOne(r => r.Employee);
            reports.HasMany(r => r.Tasks);
            EntityTypeBuilder<Modification> modifications = modelBuilder.Entity<Modification>();
            modifications.ToTable("Modifications");
            modifications.HasOne(m => m.Employee);
            modifications.HasOne(m => m.Task);
            base.OnModelCreating(modelBuilder);
        }
    }
}