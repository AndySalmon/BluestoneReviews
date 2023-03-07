using bluestone.guests.data.Repositories.Support;
using System.Text.Json.Serialization;

namespace bluestone.guests.business.Abstractions.V1
  {
  public class ReviewsOverviewResponse
    {
    public List<ReviewStarRating> StarRatings { get; set; } = new List<ReviewStarRating>();

    public int ReviewCount { get; set; }
    public double AverageStarRating { get; set; }
    }
  }