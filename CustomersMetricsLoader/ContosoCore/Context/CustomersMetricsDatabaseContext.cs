using System.IO;
using ContosoCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContosoCore.Context
{
    public partial class CustomersMetricsDatabaseContext : DbContext
    {
        public CustomersMetricsDatabaseContext()
        {
        }

        public CustomersMetricsDatabaseContext(DbContextOptions<CustomersMetricsDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Metrics> Metrics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("Default");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.RowId);

                entity.Property(e => e.id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.representative)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("representative");

                entity.Property(e => e.representative_email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("representative_email");

                entity.Property(e => e.representative_phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("representative_phone");
            });

            modelBuilder.Entity<Metrics>(entity =>
            {
                entity.HasKey(e => e.RowId);

                entity.Property(e => e.id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.customer_id)
                    .HasColumnName("customer_id");

                entity.Property(e => e.expression)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("expression");

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
