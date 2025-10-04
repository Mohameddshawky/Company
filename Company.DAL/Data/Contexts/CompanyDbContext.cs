using Company.DAL.Models;
using Company.DAL.Models.Identitymodule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Data.Contexts
{
    public class CompanyDbContext:IdentityDbContext<AppUser>
    {
        public CompanyDbContext(DbContextOptions<CompanyDbContext>option):base(option)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
            
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server = SHAWKY\\MSQLSERVER ;Database= project1 ; Trusted_Connection =True ; TrustServerCertificate=true");
        //}
        
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
