using bluestone.guests.model.Entities;



namespace bluestone.guests.business.Services.Publishing
  {

  /// <summary>
  /// Publish a review to social media 
  /// </summary>
  /// <remarks>
  /// </remarks>
  public class GuestReviewPublishingService : IPublishReview
    {
    private List<ISocialMediaReviewPublisher> _publishers = new List<ISocialMediaReviewPublisher>();


    public void AddPublisher(ISocialMediaReviewPublisher publisher)
      {
      _publishers.Add(publisher);
      }




    /// <summary>
    /// Publish to any social media publishers that have been added here.
    /// </summary>
    /// <param name="review"></param>
    /// <param name="cancellationToken"></param>
    public async Task PublishAsync(Review review, CancellationToken cancellationToken = default)
      {


      foreach (ISocialMediaReviewPublisher _publisher in _publishers)
        {
        await _publisher.PublishAsync(review);
        }      
      }



    }
  }
