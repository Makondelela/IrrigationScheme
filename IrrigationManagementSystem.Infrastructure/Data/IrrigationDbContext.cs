using IrrigationManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IrrigationManagementSystem.Infrastructure.Data;

public class IrrigationDbContext(DbContextOptions<IrrigationDbContext> options) : DbContext(options)
{
    public DbSet<IrrigationScheme> IrrigationSchemes => Set<IrrigationScheme>();

    public DbSet<Village> Villages => Set<Village>();

    public DbSet<SubCanal> SubCanals => Set<SubCanal>();

    public DbSet<Farmer> Farmers => Set<Farmer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IrrigationScheme>(entity =>
        {
            entity.ToTable("irrigation_schemes");
            entity.HasKey(scheme => scheme.Id).HasName("pk_irrigation_schemes");
            entity.Property(scheme => scheme.Id).HasColumnName("id");
            entity.Property(scheme => scheme.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            entity.Property(scheme => scheme.RiverName).HasColumnName("river_name").HasMaxLength(200).IsRequired();
            entity.Property(scheme => scheme.Description).HasColumnName("description").HasMaxLength(1000).IsRequired();
        });

        modelBuilder.Entity<Village>(entity =>
        {
            entity.ToTable("villages");
            entity.HasKey(village => village.Id);
            entity.Property(village => village.Id).HasColumnName("id");
            entity.Property(village => village.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            entity.Property(village => village.Order).HasColumnName("Order").IsRequired();
            entity.Property(village => village.IrrigationSchemeId).HasColumnName("irrigation_scheme_id");
            entity.HasOne<IrrigationScheme>()
                .WithMany()
                .HasForeignKey(village => village.IrrigationSchemeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SubCanal>(entity =>
        {
            entity.ToTable("sub_canals");
            entity.HasKey(subCanal => subCanal.Id);
            entity.Property(subCanal => subCanal.Id).HasColumnName("id");
            entity.Property(subCanal => subCanal.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            entity.Property(subCanal => subCanal.VillageId).HasColumnName("village_id");
            entity.Property(subCanal => subCanal.NumberOfFarmers).HasColumnName("number_of_farmers").IsRequired();
            entity.HasOne<Village>()
                .WithMany()
                .HasForeignKey(subCanal => subCanal.VillageId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Farmer>(entity =>
        {
            entity.ToTable("farmers");
            entity.HasKey(farmer => farmer.Id);
            entity.Property(farmer => farmer.Id).HasColumnName("id");
            entity.Property(farmer => farmer.Name).HasColumnName("name").HasMaxLength(200).IsRequired();
            entity.Property(farmer => farmer.VillageId).HasColumnName("village_id");
            entity.Property(farmer => farmer.SubCanalId).HasColumnName("sub_canal_id");
            entity.HasOne<Village>()
                .WithMany()
                .HasForeignKey(farmer => farmer.VillageId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne<SubCanal>()
                .WithMany()
                .HasForeignKey(farmer => farmer.SubCanalId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
