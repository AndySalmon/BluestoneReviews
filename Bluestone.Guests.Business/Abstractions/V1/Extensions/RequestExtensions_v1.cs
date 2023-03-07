using bluestone.guests.model.Entities;

namespace bluestone.guests.business.Abstractions.V1.Extensions
  {
  public static class RequestExtensions_v1
    {

    public static CreateReviewResponse ToCreateReviewResponse(this Review review)
      {
      return new CreateReviewResponse()
        {
        ID = review.ID,
        GuestID = review.GuestID,
        Title = review.Title,
        Body = review.Body,
        Score = review.Score
        };
      }





    public static string FullName(this GuestResponse guestResponse)
      {
      List<string> name = new List<string>();

      if (String.IsNullOrEmpty(guestResponse.Title) == false)
        name.Add(guestResponse.Title);

      if (String.IsNullOrEmpty(guestResponse.ForeNames) == false)
        name.Add(guestResponse.ForeNames);

      if (String.IsNullOrEmpty(guestResponse.SurName) == false)
        name.Add(guestResponse.SurName);

      return String.Join(" ", name);
      }

    }
  }
