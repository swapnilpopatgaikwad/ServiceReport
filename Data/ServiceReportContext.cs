using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceReport.Models;
using ServiceReport.Data;
using ServiceReport.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceReport.Data
{
    public class ServiceReportContext : DbContext
    {
        internal readonly IEnumerable<object> Machines;

        public ServiceReportContext(DbContextOptions<ServiceReportContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<Role> Role { get; set; } = default!;
        public DbSet<UserRole> UserRole { get; set; } = default!;
        public DbSet<DailyWorkReport> DailyWorkReport { get; set; } = default!;
        public DbSet<ReportService> ReportService { get; set; } = default!;
        // Add this DbSet for MasterData
        public DbSet<MasterData> MasterData { get; set; }

        //public DbSet<InstallationReport> InstallationReport { get; set; }
        //public DbSet<MachineDetail> MachineDetails { get; set; }
        //public DbSet<PowerSupplyDetails> PowerSupplyDetails { get; set; }
        //public DbSet<GeneralCapability> GeneralCapabilities { get; set; }
        //public DbSet<MachineSpecification> MachineSpecifications { get; set; }
        //public DbSet<CuttingMaterialDetail> CuttingMaterialDetails { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet <ReportServiceArea> ReportServiceAreas{ get; set; }
        public DbSet <AreaProblem> AreaProblems { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SelectListGroup>().HasNoKey();

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Problem>()
               .HasOne(ur => ur.Area)
               .WithMany(u => u.Problems)
               .HasForeignKey(ur => ur.AreaId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<ReportServiceArea>()
                .HasOne(ur => ur.ReportService)
                .WithMany(r => r.ReportServiceAreas)
                .HasForeignKey(ur => ur.ReportServiceId);

            modelBuilder.Entity<AreaProblem>()
                .HasOne(ur => ur.Area)
                .WithMany(r => r.AreaProblems)
                .HasForeignKey(ur => ur.AreaId);
        }



        public override int SaveChanges()
        {
            AssignUserRole();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            AssignUserRole();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public void AssignUserRole()
        {
            var defaultRole = Role.FirstOrDefault(r => r.IsDefault);
            if (defaultRole == null) return;

            var newUsers = ChangeTracker.Entries<User>()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity)
                .ToList(); // Collect new users into a list first

            var userRolesToAdd = new List<UserRole>(); // Create a list to hold new UserRole entries

            foreach (var user in newUsers)
            {
                userRolesToAdd.Add(new UserRole
                {
                    UserId = user.Id,
                    User = user,
                    RoleId = defaultRole.Id,
                    Role = defaultRole,
                });
            }

            if (userRolesToAdd.Count > 0)
            {
                UserRole.AddRange(userRolesToAdd); // Add all new UserRole entries at once
            }
        }
        
        
    }
}
