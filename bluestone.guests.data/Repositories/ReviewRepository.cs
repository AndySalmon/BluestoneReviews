using bluestone.guests.core.Repositories;
using bluestone.guests.data.Repositories.Interfaces;
using bluestone.guests.model.Entities;
using Microsoft.EntityFrameworkCore;

namespace bluestone.guests.data.Repositories
  {
  public class ReviewRepository : Repository<Review>, IReviewRepository
    {
    public ReviewRepository(GuestReviewsDbContext context)
        : base(context)
      { }

    public async Task<IEnumerable<Review>> GetAllWithGuestAsync()
      {
      return await GuestReviewsDbContext.Reviews
          .Include(m => m.Guest)
          .ToListAsync();
      }

    public async Task<Review> GetWithGuestByIdAsync(Guid id)
      {
      return await GuestReviewsDbContext.Reviews
          .Include(m => m.Guest)
          .SingleOrDefaultAsync(m => m.ID == id); ;
      }

    public async Task<IEnumerable<Review>> GetAllWithGuestByGuestIdAsync(Guid GuestID)
      {
      return await GuestReviewsDbContext.Reviews
          .Include(r => r.Guest)
          .Where(r => r.GuestID == GuestID)
          .ToListAsync();
      }

    private GuestReviewsDbContext GuestReviewsDbContext
      {
      get { return Context as GuestReviewsDbContext; }
      }
    }
  }