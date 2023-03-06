using bluestone.guests.business.Abstractions.V1;
using bluestone.guests.business.Validations;
using bluestone.guests.model.Entities;

namespace bluestone.guests.business.Services.Reviews.V1
  {
  public interface IGuestReviewService
    {
    public Task<Guest> GetGuestByIDAsync(Guid id);
    public Task<Guest> GetGuestByEmailAsync(string email);
    public List<Guest> GetAllGuests();
    public Task<int> CountGuests();
    public Task<IEnumerable<Guest>> GetAllGuestsAsync();
    public Task<IEnumerable<ReviewResponse>> GetAllReviewsAsync();
    public Task<CreateReviewResponse> CreateReview(CreateReviewRequest reviewRequest, IValidationDictionary validationState);
    public Task<int> CountReviews();
    }
  }