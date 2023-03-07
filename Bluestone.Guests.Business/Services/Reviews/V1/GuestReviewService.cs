using bluestone.guests.business.Abstractions.V1;
using bluestone.guests.business.Abstractions.V1.Extensions;
using bluestone.guests.business.Services.Publishing;
using bluestone.guests.business.Validations;
using bluestone.guests.data.Repositories.Interfaces;
using bluestone.guests.model.Entities;

namespace bluestone.guests.business.Services.Reviews.V1
  {

  public class GuestReviewService : IGuestReviewService
    {
    private readonly IUnitOfWork _unitOfWork;

    private readonly IPublishReview _reviewPublishingService;

    public GuestReviewService(IUnitOfWork unitOfWork, IPublishReview reviewPublishingService)
      {
      _unitOfWork = unitOfWork;
      _reviewPublishingService = reviewPublishingService;
      }


    public async Task<Guest> GetGuestByIDAsync(Guid id)
      {
      //      return await UnitOfWork.Guests.GetByIdAsync(id);
      return await _unitOfWork.Guests.FirstOrDefaultAsync(g => g.ID == id);
      }


    public async Task<Guest> GetGuestByEmailAsync(string email)
      {
      string _emailTest = email.ToLower().Trim();

      return await _unitOfWork.Guests.FirstOrDefaultAsync(g => g.Email.ToLower() == _emailTest);
      }




    public List<Guest> GetAllGuests()
      {
      return _unitOfWork.Guests.GetAll();
      }



    public async Task<IEnumerable<Guest>> GetAllGuestsAsync()
      {
      return await _unitOfWork.Guests.GetAllWithReviewsAsync();
      }




    public async Task<IEnumerable<ReviewResponse>> GetAllReviewsAsync()
      {
      IEnumerable<Review> _results = await _unitOfWork.Reviews.GetAllWithGuestAsync();

      return _results.Select(r => new ReviewResponse()
        {
        ID = r.ID,
        Guest = new GuestResponse()
          {
          ID = r.Guest.ID,
          Title = r.Guest.Title,
          ForeNames = r.Guest.ForeNames,
          SurName = r.Guest.SurName,
          Phone = r.Guest.Phone,
          Email = r.Guest.Email,
          },
        Title = r.Title,
        Body = r.Body,
        Score = r.Score
        });
      }



    public async Task<int> CountGuests()
      {
      return await _unitOfWork.Guests.Count();
      }



    public async Task<int> CountReviews()
      {
      return await _unitOfWork.Reviews.Count();
      }






    /// <summary>
    /// Adds a new review to the database
    /// </summary>
    /// 
    /// <param name="reviewRequest">Abstracted review object</param>
    /// <returns></returns>
    public async Task<CreateReviewResponse> CreateReview(CreateReviewRequest reviewRequest, IValidationDictionary validationState)
      {
      CreateReviewResponse _response = null;

      reviewRequest.Validate(validationState);

      if (validationState.IsValid == true)    // Validation was successful
        {
        Guest _guest = null;


        // First find the guest record - from the ID or from the Email Address

        if (reviewRequest.GuestID != Guid.Empty)
          {
          _guest = await GetGuestByIDAsync(reviewRequest.GuestID);
          }
        else
          {
          _guest = await GetGuestByEmailAsync(reviewRequest.GuestEmail);


          // Create a new guest if one not found and the AllowCreateNewGuest is true.
          // Note: this is only allowed if a valid email has been provided.

          if (_guest == null && reviewRequest.AllowCreateNewGuest == true)
            {
            _guest = new Guest()
              {
              Email = reviewRequest.GuestEmail
              };

            await _unitOfWork.Guests.AddAsync(_guest);
            }
          }





        if (_guest != null)
          {
          Review _newReview = new Review()
            {
            GuestID = _guest.ID,
            Title = reviewRequest.Title,
            Body = reviewRequest.Body,
            Score = reviewRequest.Score,
            };



          await _unitOfWork.Reviews.AddAsync(_newReview);
          await _unitOfWork.CommitAsync();


          await PublishToSocialsIfGoodScoreAsync(_newReview);

          _response = _newReview.ToCreateReviewResponse();
          }
        else
          validationState.AddError("Guest", "The specified guest does not exist");


        }

      return _response;
      }






    private async Task PublishToSocialsIfGoodScoreAsync(Review review, CancellationToken cancellationToken = default)
      {
      if(review.Score >= 4)
        await _reviewPublishingService.PublishAsync(review, cancellationToken);
      }






    public async Task<ReviewsOverviewResponse> ReviewsOverview()
      {
      ReviewsOverviewResponse _response = new ReviewsOverviewResponse();

      _response.StarRatings.AddRange(await _unitOfWork.Reviews.GetReviewStarRatings());

      _response.ReviewCount = _response.StarRatings.Sum(rating => rating.Reviews);

      int _totalScore = _response.StarRatings.Sum(rating => rating.Score * rating.Reviews);


      _response.StarRatings.ForEach(rating => rating.Percentage = (_response.ReviewCount > 0) ? (((double)rating.Reviews * 100.0 / (double)_response.ReviewCount) ) : 0);


      _response.AverageStarRating = (_response.ReviewCount != 0) ? (double)_totalScore / (double)_response.ReviewCount : 0;

      return _response;
      }
    }



  }
