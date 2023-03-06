using bluestone.guests.model.Entities;

namespace bluestone.guests.business.Services.Publishing
  {
  public interface ISocialMediaReviewPublisher
    {
    Task PublishAsync(Review review);
    }
  }
