using bluestone.guests.model.Entities;

namespace bluestone.guests.data.Repositories.Interfaces
  {
    public interface IReviewRepository : IRepository<Review>
    {
    Task<IEnumerable<Review>> GetAllWithGuestAsync();
    Task<Review> GetWithGuestByIdAsync(Guid id);
    Task<IEnumerable<Review>> GetAllWithGuestByGuestIdAsync(Guid GuestId);
    }
  }