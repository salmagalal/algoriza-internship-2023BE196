using DomainLayer.Consts;
using DomainLayer.Models;
using DomainLayer.Models.Authentication.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            SeedAdmin(builder);
 
            builder.Entity<ApplicationUser>()
                .Property(u => u.FullName)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

            builder.Entity<Doctor>()
                .HasMany(d => d.Times)
                .WithMany(t => t.Doctors)
                .UsingEntity<DoctorTime>(
                    k => k
                        .HasOne(dt => dt.Time)
                        .WithMany(t => t.DoctorTimes)
                        .HasForeignKey(dt => dt.TimeId),
                    k => k
                        .HasOne(dt => dt.Doctor)
                        .WithMany(d => d.DoctorTimes)
                        .HasForeignKey(dt => dt.DoctorId),
                    k =>
                    {
                        k.HasKey(dt => new { dt.DoctorId, dt.TimeId });
                    }
                );
        }


        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Specialization> Specializations { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Time> Times { get; set; }
        
        public DbSet<DoctorTime> DoctorTimes { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Coupon> Coupons { get; set; }

        private static PasswordHasher<ApplicationUser> passwordHasher 
            = new PasswordHasher<ApplicationUser>();

        private static void SeedAdmin(ModelBuilder builder)
        {
            var adminRoleId = "1"; 
            var adminUser = new ApplicationUser
            {
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                PasswordHash = "Admin_2001",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = "Admin",
                LastName = "Admin",
                PhoneNumber = "01027485927",
                DateOfBirth = DateTime.Now,
                Gender = Gender.Male, 
                LockoutEnabled = true
            };

            var hashedPassword = passwordHasher.HashPassword(adminUser, "Admin_2001");

            adminUser.PasswordHash = hashedPassword;

            builder.Entity<ApplicationUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUser.Id, RoleId = adminRoleId }
            );
        }

    }
}
