using bluestone.guests.business.Abstractions.V1;
using bluestone.guests.business.Services.Publishing;
using bluestone.guests.business.Validations;
using bluestone.guests.data.Repositories.Interfaces;
using bluestone.guests.model.Entities;
using Microsoft.EntityFrameworkCore;

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
        // First find the guest record

        Guest _guest = await GetGuestByIDAsync(reviewRequest.GuestID);

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


          _response = new CreateReviewResponse()
            {
            ID = _newReview.ID,
            GuestID = _newReview.GuestID,
            Title = reviewRequest.Title,
            Body = reviewRequest.Body,
            Score = reviewRequest.Score
            };
          }
        else
          validationState.AddError("Guest", "The specified guest does not exist");


        }

      return _response;
      }

    }



  }
