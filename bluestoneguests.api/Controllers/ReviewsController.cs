using bluestone.guests.business.Abstractions.V1;
using bluestone.guests.business.Services.Reviews.V1;
using bluestone.guests.business.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace bluestoneguests.api.Controllers
{
    [ApiController]
  [Route("/api/v1/[controller]")]
  public class ReviewsController : ControllerBase
    {
    private readonly ILogger<ReviewsController> _logger;
    private readonly IGuestReviewService _reviewService;

    public ReviewsController(ILogger<ReviewsController> logger, IGuestReviewService reviewService)
      {
      _logger = logger;
      _reviewService = reviewService;
      }

    [HttpGet(Name = "reviews")]
    public async Task<IEnumerable<ReviewResponse>> Get()
      {
      return await _reviewService.GetAllReviewsAsync();
      }

    [HttpPost(Name = "review")]
    public async Task<ActionResult<CreateReviewResponse>> CreateReview([FromBody] CreateReviewRequest newReview)
      {
      ModelStateDictionary _modelState = new ModelStateDictionary();
      ModelStateWrapper _modelStateWrapper = new ModelStateWrapper(_modelState);

      CreateReviewResponse _response = await _reviewService.CreateReview(newReview, _modelStateWrapper);

      if (_modelStateWrapper.IsValid == false)
        return ValidationProblem(_modelState);

      return Ok(_response);
      }
    }
  }