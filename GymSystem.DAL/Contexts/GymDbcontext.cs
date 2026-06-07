using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GymSystem.DAL.Configurations;
using GymSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.DAL.Contexts
{
    public class GymDbcontext:DbContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;database=GymDb;trusted_Connection=true;trustServerCertificate=true");
        //}

        public GymDbcontext(DbContextOptions<GymDbcontext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        #region DbSets
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Member> members { get; set; }
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<Sessions> sessions { get; set; }
        public DbSet<MemberShip> memberships { get; set; }
        public DbSet<HealthRecord> healthRecords { get; set; }
        public DbSet<GymUser> gymUsers { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Booking> bookings { get; set; }
        #endregion

    }
}
