using bluestone.guests.core.Repositories;
using bluestone.guests.data.Repositories.Interfaces;
using bluestone.guests.model.Entities;
using Microsoft.EntityFrameworkCore;

namespace bluestone.guests.data.Repositories
  {
  public class GuestRepository : Repository<Guest>, IGuestRepository
    {
    public GuestRepository(GuestReviewsDbContext context)
        : base(context)
      { }


    public async Task<IEnumerable<Guest>> GetAllWithReviewsAsync()
      {
      IEnumerable<Guest> _guests = 
       await GuestReviewsDbContext.Guests
          .Include(g => g.Reviews)
          .ToListAsync();

      return _guests;
      }

    public Task<Guest> GetWithReviewsByIdAsync(Guid id)
      {
      return GuestReviewsDbContext.Guests
          .Include(g => g.Reviews)
          .SingleOrDefaultAsync(g => g.ID == id);
      }

    private GuestReviewsDbContext GuestReviewsDbContext
      {
      get { return Context as GuestReviewsDbContext; }
      }
    }

  }
