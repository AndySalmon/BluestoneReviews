using bluestone.guests.model.Entities;

namespace bluestone.guests.business.Services.Publishing
  {
  public interface IPublishReview
    {
    Task PublishAsync(Review review);
    }
  }
