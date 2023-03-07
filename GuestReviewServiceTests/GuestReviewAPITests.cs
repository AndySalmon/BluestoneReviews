using bluestone.guests.business.Abstractions.V1;
using bluestoneguests.api.Controllers;
using GuestReviewServiceTests.TestData;
using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

namespace GuestReviewServiceTests
  {
  public class GuestReviewAPITests
    {
    private GuestReviewsTestData _testReviewData;

    [OneTimeSetUp]
    public async Task Init()
      {
      _testReviewData = new GuestReviewsTestData();

      await _testReviewData.CreateStableGuestReviewService();
      }
    


    [Test]
    public async Task GetAllReviewsAsync_ShouldReturnOnlyNumberStartedWith()
      {
      ReviewsController _sut = await _testReviewData.CreateStableReviewsAPI();


      // Act
      List<ReviewResponse> _reviews = (await _sut.GetAllReviews()).ToList();


      // Assert
      Assert.AreEqual(_testReviewData.InitialReviewCount, _reviews.Count());
      }




    [Test]
    public async Task CreateReviewAsync_UsingGuestID_ShouldCreateNewReview()
      {
      // Arrange
      ReviewsController _sut = await _testReviewData.CreateStableReviewsAPI();

      int _expectedReviewCount = _testReviewData.InitialReviewCount + 1;

      Guid _guestID = _testReviewData.GuestData[1].ID;

      CreateReviewRequest _request = new CreateReviewRequest()
        {
        GuestID = _guestID,
        Title = "Fantastic Lodge",
        Body = "We had a fantastic stay at Bluestone.",
        Score = 5,
        };


      // Act

      ActionResult <CreateReviewResponse> _response = await _sut.CreateReview(_request);

      CreateReviewResponse _responseReview = _response.Value;

      List<ReviewResponse> _reviews = (await _sut.GetAllReviews()).ToList();      // Shouldn't test 2 cases in the same test.




      // Assert
      Assert.AreEqual(_expectedReviewCount, _reviews.Count());
      }









    [Test]
    public async Task CreateReviewAsync_UsingGuestEmail_ShouldCreateNewReview()
      {
      // Arrange
      ReviewsController _sut = await _testReviewData.CreateStableReviewsAPI();

      int _expectedReviewCount = _testReviewData.InitialReviewCount + 1;

      string _guestEmail = _testReviewData.GuestData[1].Email;

      CreateReviewRequest _request = new CreateReviewRequest()
        {
        GuestEmail = _guestEmail,
        Title = "Fantastic Lodge",
        Body = "We had a fantastic stay at Bluestone.",
        Score = 5,
        };



      // Act
      ActionResult<CreateReviewResponse> _response = await _sut.CreateReview(_request);
      CreateReviewResponse _responseReview = _response.Value;

      List<ReviewResponse> _reviews = (await _sut.GetAllReviews()).ToList();      // Shouldn't test 2 cases in the same test.




      // Assert
      Assert.AreEqual(_expectedReviewCount, _reviews.Count());
      }









    /// <summary>
    /// This test is expecting to fail by providing an unknown email address with AllowCreateNewGuest == false 
    /// </summary>

    [Test]
    public async Task CreateReviewAsync_UsingUnknownEmail_ShouldFailToCreateNewGuestAndReview()
      {
      // Arrange
      ReviewsController _sut = await _testReviewData.CreateStableReviewsAPI();

      // There should be no change in the expected results as the method is expected to fail.

      int _expectedGuestCount = _testReviewData.InitialGuestCount;
      int _expectedReviewCount = _testReviewData.InitialReviewCount;

      string _guestEmail = "ttennanta@seesaa.net";

      CreateReviewRequest _request = new CreateReviewRequest()
        {
        GuestEmail = _guestEmail,
        Title = "Fantastic Lodge",
        Body = "We had a fantastic stay at Bluestone.",
        Score = 5,
        AllowCreateNewGuest = false
        };




      // Act
      ActionResult<CreateReviewResponse> _response = await _sut.CreateReview(_request);
      CreateReviewResponse _responseReview = _response.Value;

      List<ReviewResponse> _reviews = (await _sut.GetAllReviews()).ToList();      // Shouldn't test 2 cases in the same test.




      // Assert

//      Assert.AreEqual(_expectedGuestCount, await _sut.CountGuests());
      Assert.AreEqual(_expectedReviewCount, _reviews.Count());
      }











    [Test]
    public async Task CreateReviewAsync_UsingUnknownEmail_ShouldCreateNewGuestAndReview()
      {
      // Arrange
      ReviewsController _sut = await _testReviewData.CreateStableReviewsAPI();

      int _expectedGuestCount = _testReviewData.InitialGuestCount + 1;
      int _expectedReviewCount = _testReviewData.InitialReviewCount + 1;

      string _guestEmail = "ttennanta@seesaa.net";

      CreateReviewRequest _request = new CreateReviewRequest()
        {
        GuestEmail = _guestEmail,
        Title = "Fantastic Lodge",
        Body = "We had a fantastic stay at Bluestone.",
        Score = 4,
        AllowCreateNewGuest = true
        };



      // Act
      ActionResult<CreateReviewResponse> _response = await _sut.CreateReview(_request);
      CreateReviewResponse _responseReview = _response.Value;

      List<ReviewResponse> _reviews = (await _sut.GetAllReviews()).ToList();      // Shouldn't test 2 cases in the same test.



      // Assert
//      Assert.AreEqual(_expectedGuestCount, await _sut.CountGuests());
      Assert.AreEqual(_expectedReviewCount, _reviews.Count());
      }


    }
  }
