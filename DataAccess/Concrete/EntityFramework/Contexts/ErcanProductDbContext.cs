using Entities.ErcanProduct.Concrete.CallCenter;
using Entities.ErcanProduct.Concrete.Contact;
using Entities.ErcanProduct.Concrete.Hotel;
using Entities.ErcanProduct.Concrete.System;
using Entities.ErcanProduct.Concrete.Tour;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class ErcanProductDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<TourPeriod> TourPeriods { get; set; }
        public DbSet<RequestChannel> RequestChannels { get; set; }
        public DbSet<NumberRange> NumberRanges { get; set; }
        public DbSet<HotelPermaLink> HotelPermaLinks { get; set; }
        public DbSet<TourPermaLink> TourPermaLinks { get; set; }
        public DbSet<CallCenterPersonel> CallCenterPersonels { get; set; }
        protected IConfiguration Configuration { get; }

        public ErcanProductDbContext()
        {
        }

        public ErcanProductDbContext(DbContextOptions<ErcanProductDbContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected ErcanProductDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.ChangedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreditLimit).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsLimitControl).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsNotUseRisk).HasDefaultValueSql("((0))");

                entity.Property(e => e.Score).HasDefaultValueSql("((0))");

                entity.Property(e => e.Title).HasComment("Tabela adı sadece şirketler için");


            });

             

            modelBuilder.Entity<ContactAddress>(entity =>
            {
                entity.Property(e => e.ChangedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Latitude).IsUnicode(false);

                entity.Property(e => e.Longtitude).IsUnicode(false);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactAddress_Contact");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactAddress_Country");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.ChangedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PhoneCode).IsFixedLength(true);

                entity.Property(e => e.UnitIdMoney).HasComment("Sadece parayla");

            });



             

             

            modelBuilder.Entity<ContactBank>(entity =>
            {
                entity.Property(e => e.ChangedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.ContactBanks)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("FK_ContactBank_Contact");
            });

             

            modelBuilder.Entity<ContactEmail>(entity =>
            {
                entity.Property(e => e.ChangedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Emails)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactEmail_Contact");
            });

            

            modelBuilder.Entity<ContactPhone>(entity =>
            {
                entity.Property(e => e.ChangedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CountryPhoneCode).HasDefaultValueSql("((90))");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsApprovedForMasterPass).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Phones)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContactPhone_Contact");
            });

            modelBuilder.Entity<ContactRelation>(entity =>
            {
                entity.Property(e => e.ChangedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.FromContact)
                    .WithMany(p => p.ContactRelations)
                    .HasForeignKey(d => d.FromContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Relation_Contact");

                

                entity.HasOne(d => d.ToContact)
                    .WithMany(p => p.ToContactRelations)
                    .HasForeignKey(d => d.ToContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Relation_Contact1");

                
            });

             

             

            modelBuilder.Entity<ContactSegment>(entity =>
            {
                entity.HasIndex(e => new { e.ContactId, e.SegmentTypeId }, "ClusteredIndex-20190711-140444")
                    .IsClustered();

                entity.Property(e => e.ChangedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ContactSegmentId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                 

            });

            modelBuilder.Entity<ContactSegmentType>(entity =>
            {
                entity.Property(e => e.ChangedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ColorCode).IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SegmentName).IsUnicode(false);

                entity.Property(e => e.SegmentTypeId).ValueGeneratedOnAdd();


            });

             
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ErcanProductDbContext")));
            }
        }
    }
}
