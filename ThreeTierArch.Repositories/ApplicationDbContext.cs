using Microsoft.EntityFrameworkCore;
using ThreeTierArch.Entities;

namespace ThreeTierArch.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //cities configuration
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<City>().Property(p => p.Name).HasColumnType("varchar(100)").IsRequired();
            modelBuilder.Entity<City>().Property(p => p.StateId).IsRequired();
            //modelBuilder.Entity<City>().HasCheckConstraint("check_stateId", "[StateId] > 0"); <- this obsolete
            modelBuilder.Entity<City>().ToTable(t => t.HasCheckConstraint("check_stateId", "[StateId] > 0"));
            modelBuilder.Entity<City>().HasIndex(p => p.Name).IsUnique();
            
            //Countries Configuration
            modelBuilder.Entity<Country>().Property(m => m.Name).HasColumnType("Varchar(100)").IsRequired();
            modelBuilder.Entity<Country>().HasIndex(p => p.Name).IsUnique();

            //State Configuration
            modelBuilder.Entity<State>().Property(p => p.Name).HasColumnType("Varchar(100)").IsRequired();
            modelBuilder.Entity<State>().Property(p => p.CountryId).IsRequired();
            modelBuilder.Entity<State>().HasIndex(m => m.Name).IsUnique();

            //UserInfos Configuration
            modelBuilder.Entity<UserInfo>().Property(m => m.Username).HasColumnType("Varchar(100)").IsRequired();
            modelBuilder.Entity<UserInfo>().HasIndex(m => m.Username).IsUnique();

            //StudentSkill Configuration
            modelBuilder.Entity<StudentSkill>().HasKey(m => new { m.StudentId, m.SkillId });
        }

    }
}
