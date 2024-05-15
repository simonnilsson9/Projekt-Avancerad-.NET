using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Projekt_Avancerad_.NET.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<LoginInfo> LoginInfos { get; set; }
        public DbSet<AppointmentHistory> AppointmentHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Test Data

            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 1,
                Name = "Simon Nilsson",
                Phone = "0702611037",
                Email = "simon.nilsson.10@gmail.com",                
            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 2,
                Name = "Anton Nilsson",
                Phone = "0481771495",
                Email = "antonnilsson@gmail.com",                
            });
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                CustomerId = 3,
                Name = "Emm Södergren",
                Phone = "0730351076",
                Email = "sodergrenemm@gmail.com",               
            });

            modelBuilder.Entity<Company>().HasData(new Company
            {
                CompanyId = 1,
                Name = "Ringhals",
                  
            });
            modelBuilder.Entity<Company>().HasData(new Company
            {
                CompanyId = 2,
                Name = "Derome",
                               
            });

            modelBuilder.Entity<LoginInfo>().HasData(new LoginInfo 
            {
                LoginId = 1,
                EMail = "admin@admin.se",
                Password = "1234",
                Role = "admin"
            });
            
            modelBuilder.Entity<LoginInfo>().HasData(new LoginInfo 
            {
                LoginId = 2,
                EMail = "simon.nilsson.10@gmail.com",
                Password = "1234",
                Role = "customer",
                CustomerId = 1
            });
            
            modelBuilder.Entity<LoginInfo>().HasData(new LoginInfo 
            {
                LoginId = 3,
                EMail = "sodergrenemm@gmail.com",
                Password = "1234",
                Role = "customer",
                CustomerId = 3

            });
            
            modelBuilder.Entity<LoginInfo>().HasData(new LoginInfo 
            {
                LoginId = 4,
                EMail = "antonnilsson@gmail.com",
                Password = "1234",
                Role = "customer",
                CustomerId = 2
            });
            
            modelBuilder.Entity<LoginInfo>().HasData(new LoginInfo 
            {
                LoginId = 5,
                EMail = "ringhals@varberg.se",
                Password = "1234",
                Role = "company",
                CompanyId = 1
            });
            
            modelBuilder.Entity<LoginInfo>().HasData(new LoginInfo 
            {
                LoginId = 6,
                EMail = "derome@varberg.se",
                Password = "1234",
                Role = "company",
                CompanyId= 2
            });
            

            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = 1,
                Date = new DateTime(2024, 06, 30),
                CustomerId = 1,
                CompanyId = 1,
            });
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = 2,
                Date = new DateTime(2024, 05, 28),
                CustomerId = 1,
                CompanyId = 2,
            });
            modelBuilder.Entity<Appointment>().HasData(new Appointment
            {
                AppointmentId = 3,
                Date = new DateTime(2024, 06, 17),
                CustomerId = 2,
                CompanyId = 2,
            });

            
        }
    }
}
