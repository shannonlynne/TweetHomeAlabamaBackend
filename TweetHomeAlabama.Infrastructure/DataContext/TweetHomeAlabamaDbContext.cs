using Microsoft.EntityFrameworkCore;
using TweetHomeAlabama.Data.Entities;

namespace TweetHomeAlabama.Data.DataContext
{
    public class TweetHomeAlabamaDbContext : DbContext
    {
        public DbSet<BirdEntity> Birds => Set<BirdEntity>();
        public DbSet<ColorEntity> Colors => Set<ColorEntity>();
        public DbSet<ShapeEntity> Shapes => Set<ShapeEntity>();
        public DbSet<SizeEntity> Sizes => Set<SizeEntity>();
        public DbSet<HabitatEntity> Habitats => Set<HabitatEntity>();

        public TweetHomeAlabamaDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            {
                modelBuilder.Entity<BirdEntity>(entity =>
                {
                    entity.ToTable("Bird", "Bird");

                    entity.HasKey(bird => new
                    {
                        bird.Id
                    });

                    entity.Property(x => x.Id).HasColumnName("BirdId");
                    entity.Property(x => x.Name).HasColumnName("Name");
                    entity.Property(x => x.ImageUrl).HasColumnName("ImageUrl");
                    entity.Property(x => x.ShortDescription).HasColumnName("ShortDescription");
                });

                modelBuilder.Entity<ColorEntity>(entity =>
                {
                    entity.ToTable("BirdColor", "Bird");

                    entity.HasKey(color => new
                    {
                        color.Id
                    });

                    entity.Property(x => x.Id).HasColumnName("BirdColorId");
                    entity.Property(x => x.Name).HasColumnName("Name");

                });

                modelBuilder.Entity<ShapeEntity>(entity =>
                {
                    entity.ToTable("BirdShape", "Bird");

                    entity.HasKey(shape => new
                    {
                        shape.Id
                    });

                    entity.Property(x => x.Id).HasColumnName("BirdShapeId");
                    entity.Property(x => x.Name).HasColumnName("Name");

                });

                modelBuilder.Entity<HabitatEntity>(entity =>
                {
                    entity.ToTable("BirdHabitat", "Bird");

                    entity.HasKey(habitat => new
                    {
                        habitat.Id
                    });

                    entity.Property(x => x.Id).HasColumnName("BirdHabitatId");
                    entity.Property(x => x.Name).HasColumnName("Name");

                });

                modelBuilder.Entity<SizeEntity>(entity =>
                {
                    entity.ToTable("BirdSize", "Bird");

                    entity.HasKey(size => new
                    {
                        size.Id
                    });

                    entity.Property(x => x.Id).HasColumnName("BirdSizeId");
                    entity.Property(x => x.Name).HasColumnName("Name");

                });

                base.OnModelCreating(modelBuilder);
            }
        }
    }
}


