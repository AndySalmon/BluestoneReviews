using bluestone.guests.model.Entities;

namespace bluestone.guests.data.Repositories.Interfaces
  {

  public interface IGuestRepository : IRepository<Guest>
    {
    Task<IEnumerable<Guest>> GetAllWithReviewsAsync();
    Task<Guest> GetWithReviewsByIdAsync(Guid id);
    }
  }
