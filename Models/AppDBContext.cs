using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Resources.Models;


namespace Resources.Models
{
    public class AppDBContext : IdentityDbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<AddTypes> AddTypess { get; set; }
        public DbSet <ApplicationUser> users{ get; set; }

        public DbSet<Task> Tasks { get; set; } = null;
  
        public DbSet<Link> Links { get; set; } = null;


        public DbSet<Project> Projects { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Crashing> Crashings { get; set; }

        public DbSet<FileOnFileSystemModel> FilesOnFileSystem { get; set; }

        public DbSet<CalendarEvent> Events { get; set; }

        public DbSet<Issues> Issues { get; set; }
        public DbSet<Lag> Lags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<IdentityUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

            //Project
            modelBuilder.Entity<Task>().Property(p => p.EndDate).HasComputedColumnSql("DATEADD(day,[Duration], [StartDate])");



            //Resources
     /*       modelBuilder.Entity<Resource>().HasMany(p => p.Tasks);   */ //relation
            modelBuilder.Entity<Resource>()
               .Property(p => p.TotalCost)
               .HasComputedColumnSql("[CostPerDay] *[UnitsPerDay]* [Duration]");


            //Crashings
            //modelBuilder.Entity<Crashing>()
            //    .HasMany(e => e.CriticalActivities).WithMany(c => c.Crashes);

            modelBuilder.Entity<Crashing>()
                .Property(e => e.ExpectedTime)
                .HasComputedColumnSql("([OptimisticDuration]+4*[MostLikelyDuration]+[PessimesticDuration])/6");

            modelBuilder.Entity<Crashing>()
                .Property(e => e.Segma)
                .HasComputedColumnSql("([PessimesticDuration]-[OptimisticDuration])/6");




        }


    }
    }
