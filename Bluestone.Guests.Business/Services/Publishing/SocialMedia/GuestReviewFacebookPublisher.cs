using bluestone.guests.model.Entities;

namespace bluestone.guests.business.Services.Publishing.SocialMedia
  {
  internal class InstagramReviewPublisher : ISocialMediaReviewPublisher
    {
    public async Task PublishAsync(Review review)
      {
      await Task.CompletedTask;

      throw new NotImplementedException();
      }
    }
  }
