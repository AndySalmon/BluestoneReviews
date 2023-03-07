using bluestone.guests.core.Repositories;
using bluestone.guests.data.Repositories.Interfaces;
using bluestone.guests.data.Repositories.Support;
using bluestone.guests.model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

    public async Task<IEnumerable<ReviewStarRating>> GetReviewStarRatings()
      {
      IQueryable<ReviewStarRating> _query = GuestReviewsDbContext.Reviews
          .AsNoTracking()
          .GroupBy(r => r.Score)
          .Select(r => new ReviewStarRating()
            {
            Name = $"{r.Key} Star",
            Score = r.Key,
            Reviews = r.Count(),
            Percentage = 0
            }
           )
          .OrderByDescending(sr => sr.Score);

      return await _query.ToListAsync();
      }

    private GuestReviewsDbContext GuestReviewsDbContext
      {
      get { return Context as GuestReviewsDbContext; }
      }
    }
  }