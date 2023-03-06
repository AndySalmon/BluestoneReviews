using bluestone.guests.business.Abstractions.V1;
using bluestone.guests.business.Services.Reviews.V1;
using bluestone.guests.business.Validations;
using bluestone.guests.model.Entities;
using GuestReviewServiceTests.TestData;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NUnit.Framework;



namespace GuestReviewServiceTests
{
    public class GuestReviewServiceTests 
    {
    private GuestReviewsTestData _testReviewData;

    [OneTimeSetUp]
    public async Task Init()
      {
      _testReviewData = new GuestReviewsTestData();

      await _testReviewData.CreateStableGuestReviewService();
      }
    

    [Test]
    public async Task GetAllGuests_ShouldReturnOnlyNumberStartedWith()
      {
      await Task.CompletedTask;

      // Arrange
      GuestReviewService _sut = await _testReviewData.CreateStableGuestReviewService();


      // Act
      List<Guest> _guests = _sut.GetAllGuests();


      // Assert
      Assert.AreEqual(_testReviewData.InitialGuestCount, _guests.Count());
      }






    [Test]
    public async Task GetAllGuestsAsync_ShouldReturnOnlyNumberStartedWith()
      {
      // Arrange
      GuestReviewService _sut = await _testReviewData.CreateStableGuestReviewService();


      // Act
      List<Guest> _guests = (await _sut.GetAllGuestsAsync()).ToList();


      // Assert
      Assert.AreEqual(_testReviewData.InitialGuestCount, _guests.Count());
      }







    [Test]
    public async Task GetAllGuestsAsync_ShouldReturn2ReviewsForGuest1()
      {
      // Arrange
      GuestReviewService _sut = await _testReviewData.CreateStableGuestReviewService();


      // Act
      List<Guest> _guests = (await _sut.GetAllGuestsAsync()).ToList();


      // Assert
      Assert.AreEqual(2, _guests[0].Reviews.Count());
      }







    /// <summary>
    /// Test use case where there are no reviews for a specified guest, 
    /// this is to confirm that the Reviews list isn't null
    /// </summary>
    /// <returns></returns>

    [Test]
    public async Task GetGuestByEmailAsync_ShouldReturn0Reviews_ForGuestRussPlumb()
      {
      // Arrange
      GuestReviewService _sut = await _testReviewData.CreateStableGuestReviewService();


      // Act
      Guest _guest = await _sut.GetGuestByEmailAsync("rplumbo@answers.com");


      // Assert
      Assert.NotNull(_guest);
      Assert.NotNull(_guest.Reviews);
      Assert.AreEqual(0, _guest.Reviews.Count());
      }






    [Test]
    public async Task GetAllReviewsAsync_ShouldReturnOnlyNumberStartedWith()
      {
      // Arrange
      GuestReviewService _sut = await _testReviewData.CreateStableGuestReviewService();


      // Act
      List<ReviewResponse> _reviews = (await _sut.GetAllReviewsAsync()).ToList();


      // Assert
      Assert.AreEqual(_testReviewData.InitialReviewCount, _reviews.Count());
      }






    [Test]
    public async Task GetGuestByIDAsync_ShouldReturnGuest_WhenGuestExists()
      {
      // Arrange
      GuestReviewService _sut = await _testReviewData.CreateStableGuestReviewService();
      Guid _guestID = _testReviewData.GuestData[1].ID;


      // Act
      Guest _guest = await _sut.GetGuestByIDAsync(_guestID);


      // Assert
      Assert.NotNull(_guest);
      Assert.AreEqual(_guestID, _guest.ID);
      }







    [Test]
    public async Task GetGuestByIDAsync_ShouldReturnNothing_WhenGuestDoesNotExists()
      {
      // Arrange
      GuestReviewService _sut = await _testReviewData.CreateStableGuestReviewService();


      // Act
      // Look for random ID
      Guest _guest = await _sut.GetGuestByIDAsync(Guid.NewGuid());


      // Assert
      Assert.Null(_guest);
      }









    [Test]
    public async Task CreateReviewAsync_UsingGuestID_ShouldReturnSuccess()
      {
      // Arrange
      GuestReviewService _sut = await _testReviewData.CreateStableGuestReviewService();

      int _expectedResult = _testReviewData.InitialReviewCount + 1;

      Guid _guestID = _testReviewData.GuestData[1].ID;

      CreateReviewRequest _request = new CreateReviewRequest()
        {
        GuestID = _guestID,
        Title = "Fantastic Lodge",
        Body = "We had a fantastic stay at Bluestone.",
        Score = 1,
        };

      ModelStateWrapper _modelStateWrapper = new ModelStateWrapper(new ModelStateDictionary());


      // Act
      CreateReviewResponse _response = (await _sut.CreateReview(_request, _modelStateWrapper));


      // Assert
      Assert.True(_modelStateWrapper.IsValid);
      Assert.AreEqual(_expectedResult, await _sut.CountReviews());
      }





    }
  }
