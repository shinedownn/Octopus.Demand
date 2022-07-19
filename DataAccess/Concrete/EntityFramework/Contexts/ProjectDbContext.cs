using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;
using Action = Entities.Concrete.Action;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    /// <summary>
    /// Because this context is followed by migration for more than one provider
    /// works on PostGreSql db by default. If you want to pass sql
    /// When adding AddDbContext, use MsDbContext derived from it.
    /// </summary>
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext()
        {
             
        }
        /// <summary>
        /// in constructor we get IConfiguration, parallel to more than one db
        /// we can create migration.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options, IConfiguration configuration): base(options)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Let's also implement the general version.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        protected ProjectDbContext(DbContextOptions options, IConfiguration configuration)
                                                                        : base(options)
        {
            Configuration = configuration;  
        } 
         
        public DbSet<Log> Logs { get; set; }
        public DbSet<MainDemand> MainDemands { get; set; } 
        public DbSet<HotelDemand> HotelDemands { get; set; }
        public DbSet<TourDemand> TourDemands { get; set; }
        public DbSet<Action> Actions { get; set; } 
        public DbSet<MainDemandAction> MainDemandActions { get; set; }
        public DbSet<HotelDemandAction> HotelDemandActions { get; set;}
        public DbSet<TourDemandAction> TourDemandActions { get; set;}
        public DbSet<HotelDemandOnRequest> HotelDemandOnRequests { get; set; }  
        public DbSet<TourDemandOnRequest> TourDemandOnRequests { get; set; }  
        public DbSet<OnRequest> OnRequests { get; set; }  
        public DbSet<Department> Departments { get; set; }
        public DbSet<OnRequestApprovement> OnRequestApprovements { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<RequestChannel> RequestChannels { get; set; }
        protected IConfiguration Configuration { get; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //nextval('"Actions_ActionId_seq"'::regclass)
            //modelBuilder.UseSerialColumns();

            modelBuilder.HasSequence("MainDemands_MainDemandId_seq", "public").StartsAt(900000).IncrementsBy(1);
            modelBuilder.HasSequence("Actions_ActionId_seq", "public");
            modelBuilder.HasSequence("Departments_DepartmentId_seq", "public");
            modelBuilder.HasSequence("HotelDemandActions_HotelDemandActionId_seq", "public");
            modelBuilder.HasSequence("HotelDemandOnRequests_HotelDemandOnRequestId_seq", "public");
            modelBuilder.HasSequence("HotelDemands_HotelDemandId_seq", "public");
            modelBuilder.HasSequence("Logs_Id_seq", "public");
            modelBuilder.HasSequence("MainDemandActions_MainDemandActionId_seq", "public");
            modelBuilder.HasSequence("OnRequestApprovements_OnRequestApprovementId_seq", "public");
            modelBuilder.HasSequence("OnRequests_OnRequestId_seq", "public");
            modelBuilder.HasSequence("Reminders_ReminderId_seq", "public");
            modelBuilder.HasSequence("TourDemandActions_TourDemandActionId_seq", "public");
            modelBuilder.HasSequence("TourDemandOnRequests_TourDemandOnRequestId_seq", "public");
            modelBuilder.HasSequence("TourDemands_TourDemandId_seq", "public");

            modelBuilder.Entity<MainDemand>().Property(x => x.MainDemandId).HasDefaultValueSql("nextval('\"MainDemands_MainDemandId_seq\"'::regclass)");

            modelBuilder.Entity<Action>().Property(x => x.ActionId).HasDefaultValueSql("nextval('\"Actions_ActionId_seq\"'::regclass)");
            modelBuilder.Entity<Department>().Property(x => x.DepartmentId).HasDefaultValueSql("nextval('\"Departments_DepartmentId_seq\"'::regclass)");
            modelBuilder.Entity<HotelDemandAction>().Property(x => x.HotelDemandActionId).HasDefaultValueSql("nextval('\"HotelDemandActions_HotelDemandActionId_seq\"'::regclass)");
            modelBuilder.Entity<HotelDemandOnRequest>().Property(x => x.HotelDemandOnRequestId).HasDefaultValueSql("nextval('\"HotelDemandOnRequests_HotelDemandOnRequestId_seq\"'::regclass)");
            modelBuilder.Entity<HotelDemand>().Property(x => x.HotelDemandId).HasDefaultValueSql("nextval('\"HotelDemands_HotelDemandId_seq\"'::regclass)");
            modelBuilder.Entity<Log>().Property(x => x.Id).HasDefaultValueSql("nextval('\"Logs_Id_seq\"'::regclass)");
            modelBuilder.Entity<MainDemandAction>().Property(x => x.MainDemandActionId).HasDefaultValueSql("nextval('\"MainDemandActions_MainDemandActionId_seq\"'::regclass)");
            modelBuilder.Entity<OnRequestApprovement>().Property(x => x.OnRequestApprovementId).HasDefaultValueSql("nextval('\"OnRequestApprovements_OnRequestApprovementId_seq\"'::regclass)");
            modelBuilder.Entity<OnRequest>().Property(x => x.OnRequestId).HasDefaultValueSql("nextval('\"OnRequests_OnRequestId_seq\"'::regclass)");
            modelBuilder.Entity<Reminder>().Property(x => x.ReminderId).HasDefaultValueSql("nextval('\"Reminders_ReminderId_seq\"'::regclass)");
            modelBuilder.Entity<TourDemandAction>().Property(x => x.TourDemandActionId).HasDefaultValueSql("nextval('\"TourDemandActions_TourDemandActionId_seq\"'::regclass)");
            modelBuilder.Entity<TourDemandOnRequest>().Property(x => x.TourDemandOnRequestId).HasDefaultValueSql("nextval('\"TourDemandOnRequests_TourDemandOnRequestId_seq\"'::regclass)");
            modelBuilder.Entity<TourDemand>().Property(x => x.TourDemandId).HasDefaultValueSql("nextval('\"TourDemands_TourDemandId_seq\"'::regclass)");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                base.OnConfiguring(optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DArchPgContext")).EnableSensitiveDataLogging().UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
                 
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            }
        } 
    }
}
