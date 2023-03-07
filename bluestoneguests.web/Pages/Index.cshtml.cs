using Azure;
using bluestone.guests.business.Abstractions.V1;
using bluestone.guests.data.Repositories.Support;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System;
using System.IO;
using bluestoneguests.web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace bluestoneguests.web.Pages
  {

  public class IndexModel : PageModel
    {
    public ReviewsOverviewResponse ReviewsOverview { get => _reviewsOverview; }
    public List<ReviewResponse> Reviews { get => _reviews; }

    public List<SelectListItem> Scores;



    [BindProperty]
    public AddReviewViewModel AddReviewViewModel { get; set; }







    private ReviewsOverviewResponse _reviewsOverview;
    private List<ReviewResponse> _reviews;

    private IHttpClientFactory _clientFactory { get; set; }
    private readonly ILogger<IndexModel> _logger;





    public IndexModel(IHttpClientFactory clientFactory, ILogger<IndexModel> logger)
      {
      _logger = logger;
      _clientFactory = clientFactory;
      }



    public async Task<IActionResult> OnGet()
      {
      AddReviewViewModel = new AddReviewViewModel();

      await PreparePageAsync();

      return Page();
      }



    public async Task<IActionResult> OnPost()
      {
      if (ModelState.IsValid == true)
        {
        if(await SaveReviewAsync()  == true)
          return Redirect("/");                     // Just redirect to home page after save.
        }

      await PreparePageAsync();

      return Page();
      }


    public async Task PreparePageAsync()
      {

      await PrepareOverviewAsync();
      await PrepareReviewsAsync();

      Scores = Enumerable.Range(1, 5).Reverse().Select(s =>
                new SelectListItem
                  {
                  Value = s.ToString(),
                  Text = s.ToString()
                  }).ToList();
      }


    public async Task PrepareOverviewAsync()
      {
      HttpClient _client = _clientFactory.CreateClient("GuestReviewServices");        // Todo: replace name with constant

      try
        {
        HttpResponseMessage _httpResponse = await _client.GetAsync($"api/v1/reviews/overview");

        if (_httpResponse.IsSuccessStatusCode == true)
          {
          using (Stream _stream = await _httpResponse.Content.ReadAsStreamAsync())
            {
            _reviewsOverview = await JsonSerializer.DeserializeAsync<ReviewsOverviewResponse>(_stream);
            }
          }
        }
      catch (Exception ex)
        {
        _logger.LogError(ex, $"Error Sending request for overview to ('{_client.BaseAddress}')");
        }

      }




    public async Task PrepareReviewsAsync()
      {
      HttpClient _client = _clientFactory.CreateClient("GuestReviewServices");        // Todo: replace name with constant

      try
        {
        HttpResponseMessage _httpResponse = await _client.GetAsync($"api/v1/reviews/all");

        if (_httpResponse.IsSuccessStatusCode == true)
          {
          using (Stream _stream = await _httpResponse.Content.ReadAsStreamAsync())
            {
            _reviews = await JsonSerializer.DeserializeAsync<List<ReviewResponse>>(_stream);
            }
          }
        }
      catch (Exception ex)
        {
        _logger.LogError(ex, $"Error Sending request for overview to ('{_client.BaseAddress}')");
        }
      }




    public async Task<bool> SaveReviewAsync()
      {
      bool _success = false;


      CreateReviewRequest _request = new CreateReviewRequest()
        {
        GuestEmail = AddReviewViewModel.Email,
        Title = AddReviewViewModel.Title,
        Body = AddReviewViewModel.Body,
        Score = AddReviewViewModel.Score,
        AllowCreateNewGuest = true
        };

       // { "GuestID":"00000000-0000-0000-0000-000000000000","GuestEmail":"andrew@apxsolutions.co.uk","Title":"aaa","Body":"aaa","Score":2,"AllowCreateNewGuest":false}

      HttpClient _client = _clientFactory.CreateClient("GuestReviewServices");        // Todo: replace name with constant

      try
        {
        string _requestContent = JsonSerializer.Serialize<CreateReviewRequest>(_request);

        HttpResponseMessage _httpResponse = await _client.PostAsync($"api/v1/reviews/review", new StringContent(_requestContent, UnicodeEncoding.UTF8, "application/json"));

        _success = _httpResponse.IsSuccessStatusCode;
        }
      catch (Exception ex)
        {
        _logger.LogError(ex, $"Error Sending request for overview to ('{_client.BaseAddress}')");
        }


      return _success;
      }





    }


  }