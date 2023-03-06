using bluestone.guests.business.Services.Publishing;
using bluestone.guests.business.Services.Reviews.V1;
using bluestone.guests.business.Validations;
using bluestone.guests.data;
using bluestone.guests.data.Repositories;
using bluestone.guests.data.Repositories.Interfaces;
using bluestone.guests.model.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace GuestReviewServiceTests.TestData
{
    public class GuestReviewsTestData : IDisposable
    {

    public GuestReviewsTestData()
      {
      }

    public void Dispose()
      {
      // ... clean up test data from the database ...
      }




    public List<Guest> GuestData { get => _guestData; }
    public List<Review> ReviewData { get => _reviewData; }
    public int InitialGuestCount { get => _initialGuestCount; }
    public int InitialReviewCount { get => _initialReviewCount; }



    private List<Guest> _guestData = null;
    private List<Review> _reviewData  = null;

    private int _initialGuestCount = 0;
    private int _initialReviewCount = 0;

    ////  Data generated from https://mockaroo.com/

    private const String TestDataFileName = "TestData.json";




    public async Task<GuestReviewService> CreateStableGuestReviewService()
      {
      var options = new DbContextOptionsBuilder<GuestReviewsDbContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;

      var reviewsContext = new GuestReviewsDbContext(options);

      reviewsContext.Database.EnsureCreated();

      await LoadGuestDataAsync();

      IUnitOfWork _workUnit = new UnitOfWork(reviewsContext);

      _guestData.ForEach(g => _workUnit.Guests.AddAsync(new Guest
        {
        ID = g.ID,
        Email = g.Email,
        ForeNames = g.ForeNames,
        SurName = g.SurName,
        Phone = g.Phone,
        Title = g.Title,
        Reviews = new List<Review>(g.Reviews ?? new List<Review>())
        }));

      await reviewsContext.SaveChangesAsync();

      IPublishReview _reviewPublisher = new GuestReviewPublishingService();


      return new GuestReviewService(_workUnit, _reviewPublisher);
      }





    /// <summary>
    /// Create in memory data for guest and review table by reading a hirarchical JSON file
    /// </summary>
    /// <returns></returns>
    public async Task LoadGuestDataAsync()
      {
      if (_guestData == null)
        {
        string _path = Path.Combine(AppContext.BaseDirectory, TestDataFileName);
        string _content = await File.ReadAllTextAsync(_path);

        List<Guest> _data = JsonSerializer.Deserialize<List<Guest>>(_content);


        // Flatten Guests data. 
        _guestData = _data.Select(g => new Guest
          {
          ID = g.ID,
          Email = g.Email,
          ForeNames = g.ForeNames,
          SurName = g.SurName,
          Phone = g.Phone,
          Title = g.Title,
          Reviews = g.Reviews
          }).ToList();

        _reviewData = new List<Review>();

        // Flatten Review data. 
        _data.ForEach(g =>
        {
          if (g.Reviews != null)
            {
            _reviewData.AddRange(g.Reviews.Select(r => new Review
              {
              ID = r.ID,
              GuestID = g.ID,
              Title = r.Title,
              Body = r.Body,
              Score = r.Score,
              Guest = g
              }));
            }
        });


        _initialGuestCount = _guestData.Count; 
        _initialReviewCount = _reviewData.Count;
        }
      }




    }
  }
