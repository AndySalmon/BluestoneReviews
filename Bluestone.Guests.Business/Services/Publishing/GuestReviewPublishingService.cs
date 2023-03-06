using bluestone.guests.model.Entities;



namespace bluestone.guests.business.Services.Publishing
  {
  public class GuestReviewPublishingService : IPublishReview
    {
    private List<ISocialMediaReviewPublisher> _publishers = new List<ISocialMediaReviewPublisher>();


    public void AddPublisher(ISocialMediaReviewPublisher publisher)
      {
      _publishers.Add(publisher);
      }


    public async Task PublishAsync(Review review)
      {
      foreach (ISocialMediaReviewPublisher _publisher in _publishers)
        {
        await _publisher.PublishAsync(review);
        }      
      }



    }
  }
