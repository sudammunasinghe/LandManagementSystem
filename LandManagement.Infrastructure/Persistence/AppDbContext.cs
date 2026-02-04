using LandManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LandManagement.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Land> Lands { get; set; }
        public DbSet<Crop> Crops { get; set; }
        public DbSet<LandCrop> LandCrop { get; set; }
        public DbSet<Fertilizer> Fertilizers { get; set; }
        public DbSet<LandInput> LandInputs { get; set; }
        public DbSet<Harvest> Harvests { get; set; }
        public DbSet<LaborCost> LaborCosts { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedDateTime = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(token);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>()
                .HasMany(o => o.Lands)
                .WithOne(l => l.Owner)
                .HasForeignKey(l => l.OwnerId);

            modelBuilder.Entity<Fertilizer>()
                .HasMany(f => f.LandInputs)
                .WithOne(li => li.Fertilizer)
                .HasForeignKey(li => li.FertilizerId);

            modelBuilder.Entity<LaborCost>()
                .HasOne(lbc => lbc.LandCrop)
                .WithMany(lc => lc.LaborCosts)
                .HasForeignKey(lbc => lbc.LandCropId);

            modelBuilder.Entity<Crop>()
                .HasMany(c => c.LandCrops)
                .WithOne(lc => lc.Crop)
                .HasForeignKey(lc => lc.CropId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Harvest)
                .WithMany(h => h.Sales)
                .HasForeignKey(s => s.HarvestId);

            modelBuilder.Entity<Land>(entity =>
            {
                //entity.HasOne(l => l.Owner)
                //      .WithMany(o => o.Lands)
                //      .HasForeignKey(l => l.OwnerId);

                entity.HasMany(l => l.LandCrops)
                      .WithOne(lc => lc.Land)
                      .HasForeignKey(lc => lc.LandId);
            });

            modelBuilder.Entity<Harvest>(entity =>
            {
                //entity.HasMany(h => h.Sales)
                //      .WithOne(s => s.Harvest)
                //      .HasForeignKey(s => s.HarvestId);

                entity.HasOne(h => h.LandCrop)
                      .WithMany(lc => lc.Harvests)
                      .HasForeignKey(h => h.LandCropId);
            });

            modelBuilder.Entity<LandInput>(entity =>
            {
                entity.HasOne(li => li.LandCrop)
                      .WithMany(lc => lc.LandInputs)
                      .HasForeignKey(li => li.LandCropId);

                //entity.HasOne(li => li.Fertilizer)
                //      .WithMany(f => f.LandInputs)
                //      .HasForeignKey(li => li.FertilizerId);
            });

            modelBuilder.Entity<LandCrop>(entity =>
            {
                //entity.HasOne(lc => lc.Land)
                //      .WithMany(l => l.LandCrops)
                //      .HasForeignKey(lc => lc.LandId);

                //entity.HasOne(lc => lc.Crop)
                //      .WithMany(c => c.LandCrops)
                //      .HasForeignKey(lc => lc.CropId);

                //entity.HasMany(lc => lc.LandInputs)
                //      .WithOne(li => li.LandCrop)
                //      .HasForeignKey(li => li.LandCropId);

                //entity.HasMany(lc => lc.LaborCosts)
                //      .WithOne(lbc => lbc.LandCrop)
                //      .HasForeignKey(lbc => lbc.LandCropId);

                //entity.HasMany(lc => lc.Harvests)
                //      .WithOne(h => h.LandCrop)
                //      .HasForeignKey(h => h.LandCropId);
            });

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.IsActive)).HasDefaultValueSql("1");
                    modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.CreatedDateTime)).HasDefaultValueSql("GETUTCDATE()");
                    modelBuilder.Entity(entityType.ClrType).Property(nameof(BaseEntity.LastModifiedDateTime)).HasDefaultValueSql("GETUTCDATE()");
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
