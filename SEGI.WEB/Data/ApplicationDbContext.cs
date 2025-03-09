using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using SEGI.Data;
using System.Reflection;

namespace SEGI.WEB.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<SectorItem>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Sector>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<WhoWeAre>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<CoreValue>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Analitic>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<BannerAboutUs>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<LegacyAboutUs>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<OurVisionAboutUs>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<OurMissionAboutUs>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<OurCoreValue>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<OurCoreValueItem>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<OurHistoryAboutUs>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Team>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Article>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<GeneralEngineeringServicesItem>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<PMCServicesItem>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<ProcessSafetyStudies>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<ProcessSafetyStudiesItem>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Category>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Project>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<Career>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<New>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<ContactUs>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<BannerMain>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<History>().HasQueryFilter(x => !x.IsDelete);
            builder.Entity<ApplyCareerFormModel>().HasQueryFilter(x => !x.IsDelete);

            builder.Entity<ProjectCategory>()
                        .HasKey(sc => new { sc.ProjectId, sc.CategoryId });

            builder.Entity<ProjectCategory>()
                .HasOne(sc => sc.Project)
                .WithMany(s => s.ProjectCategories)
                .HasForeignKey(sc => sc.ProjectId);

            builder.Entity<ProjectCategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.ProjectCategories)
                .HasForeignKey(sc => sc.CategoryId);



            builder.Entity<ProjectService>()
                      .HasKey(sc => new { sc.ProjectId, sc.GeneralEngineeringServicesItemId });


            builder.Entity<ProjectService>()
                .HasOne(sc => sc.Project)
                .WithMany(s => s.ProjectServices)
                .HasForeignKey(sc => sc.ProjectId);

            builder.Entity<ProjectService>()
                .HasOne(sc => sc.GeneralEngineeringServicesItem)
                .WithMany(c => c.ProjectServices)
                .HasForeignKey(sc => sc.GeneralEngineeringServicesItemId);




            builder.Entity<CareerCategory>()
         .HasKey(sc => new { sc.CareerId, sc.CategoryId });

            builder.Entity<CareerCategory>()
                .HasOne(sc => sc.Career)
                .WithMany(s => s.CareerCategories)
                .HasForeignKey(sc => sc.CareerId);

            builder.Entity<CareerCategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.CareerCategories)
                .HasForeignKey(sc => sc.CategoryId);



            builder.Entity<NewCategory>()
         .HasKey(sc => new { sc.NewId, sc.CategoryId });

            builder.Entity<NewCategory>()
                .HasOne(sc => sc.New)
                .WithMany(s => s.NewCategories)
                .HasForeignKey(sc => sc.NewId);

            builder.Entity<NewCategory>()
                .HasOne(sc => sc.Category)
                .WithMany(c => c.NewCategories)
                .HasForeignKey(sc => sc.CategoryId);
        }

        public DbSet<WhoWeAre> WhoWeAres { get; set; }
        public DbSet<CoreValue> CoreValues { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<SectorItem> SectorItems { get; set; }
        public DbSet<Analitic> Analitics { get; set; }
        public DbSet<BannerAboutUs> BannerAboutUss { get; set; }
        public DbSet<LegacyAboutUs> LegacyAboutUss { get; set; }
        public DbSet<OurVisionAboutUs> OurVisionAboutUss { get; set; }
        public DbSet<OurMissionAboutUs> OurMissionAboutUss { get; set; }
        public DbSet<OurCoreValue> OurCoreValues { get; set; }
        public DbSet<OurCoreValueItem> OurCoreValueItems { get; set; }
        public DbSet<OurHistoryAboutUs> OurHistoryAboutUss { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<GeneralEngineeringServicesItem> GeneralEngineeringServicesItems { get; set; }
        public DbSet<PMCServicesItem> PMCServicesItems { get; set; }
        public DbSet<ProcessSafetyStudies> ProcessSafetyStudiess { get; set; }
        public DbSet<ProcessSafetyStudiesItem> ProcessSafetyStudiesItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectCategory> ProjectCategories { get; set; }
        public DbSet<ProjectService> ProjectServices { get; set; }
        public DbSet<Career> Careers { get; set; }
        public DbSet<CareerCategory> CareerCategories { get; set; }
        public DbSet<New> News { get; set; }
        public DbSet<NewCategory> NewCategories { get; set; }
        public DbSet<ContactUs> ContactUss { get; set; }
        public DbSet<BannerMain> BannerMains { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<FixedItem> FixedItems { get; set; }
        public DbSet<ApplyCareerFormModel> ApplyCareerFormModels { get; set; }

    }


}
