using bluestone.guests.data;
using bluestone.guests.model.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace bluestoneguests.api.Pages.Migrate
  {
  public class MigrateModel : PageModel
    {
    private readonly IServiceProvider _serviceProvider;
    private readonly GuestReviewsDbContext _guestReviewsDbContext;

    public MigrateModel(IServiceProvider serviceProvider, GuestReviewsDbContext guestReviewsDbContext)
      {
      _serviceProvider = serviceProvider;
      _guestReviewsDbContext = guestReviewsDbContext;
      }

    private const String TestDataFileName = "TestData\\TestData.json";

    public String ErrorMessage { get; set; }

    public async Task OnGet()
      {
      try
        {
        Console.WriteLine($"Migrating: {nameof(GuestReviewsDbContext)}");
        using GuestReviewsDbContext _ctxBase = _serviceProvider.GetRequiredService<GuestReviewsDbContext>();

        await _ctxBase.Database.MigrateAsync();

        await LoadGuestDataAsync();
        }
      catch (Exception ex)
        {
        ErrorMessage = ex.Message ?? "";
        }



      }






    public async Task LoadGuestDataAsync()
      {
      if (_guestReviewsDbContext.Guests.Count() == 0)
        {
        string _path = Path.Combine(AppContext.BaseDirectory, TestDataFileName);
        string _content = await System.IO.File.ReadAllTextAsync(_path);

        List<Guest> _data = JsonSerializer.Deserialize<List<Guest>>(_content);


        // Flatten Guests data. 
        await _guestReviewsDbContext.Guests.AddRangeAsync(_data.Select(g => new Guest
          {
          ID = g.ID,
          Email = g.Email,
          ForeNames = g.ForeNames,
          SurName = g.SurName,
          Phone = g.Phone,
          Title = g.Title,
          Reviews = g.Reviews   
          }).ToList());

        await _guestReviewsDbContext.SaveChangesAsync();

        //ReviewData = new List<Review>();

        //// Flatten Review data. 
        //_data.ForEach(g =>
        //{
        //  if (g.Reviews != null)
        //    {
        //    ReviewData.AddRange(g.Reviews.Select(r => new Review
        //      {
        //      ID = r.ID,
        //      GuestID = g.ID,
        //      Title = r.Title,
        //      Body = r.Body,
        //      Score = r.Score
        //      }));
        //    }
        //});

        }
      }


    }
  }
