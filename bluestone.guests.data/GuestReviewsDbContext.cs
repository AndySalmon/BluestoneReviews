using bluestone.guests.model.Entities;
using Microsoft.EntityFrameworkCore;

// Migrations - via package manager
// 22-2-2023  add-migration guest-reviews-initial -context GuestReviewsDbContext


// https://medium.com/swlh/building-a-nice-multi-layer-net-core-3-api-c68a9ef16368



namespace bluestone.guests.data
  {
  public class GuestReviewsDbContext : DbContext
    {
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<Guest> Guests { get; set; }

    public GuestReviewsDbContext() : base()
      {
      }


    public GuestReviewsDbContext(DbContextOptions<GuestReviewsDbContext> options)
        : base(options)
      { }

    protected override void OnModelCreating(ModelBuilder builder)
      {

      builder
          .Entity<Guest>()
          .HasMany(g => g.Reviews);

      }
    }
  }
