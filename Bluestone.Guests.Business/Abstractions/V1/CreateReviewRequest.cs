using bluestone.guests.business.Validations;

namespace bluestone.guests.business.Abstractions.V1
  {
  public class CreateReviewRequest : RequestBase
    {
    public Guid GuestID { get; set; }

    public string GuestEmail { get; set; }


    public string Title { get; set; } = "";


    public string Body { get; set; } = "";


    public int Score { get; set; } = 0;



    public bool AllowCreateNewGuest { get; set; } = false;






    // Validate the request data

    public override void Validate(IValidationDictionary validationDictionary)
      {
      if (Title.Length == 0)
        validationDictionary.AddError("Title", "Review Title must not be empty");
      else
        {
        if (Title.Length <= 0 || Title.Length > 250)
          validationDictionary.AddError("Title", "Review Title length must be between 1 and 250 characters");
        }

      if (Body.Length == 0)
        validationDictionary.AddError("Body", "Review Body must not be empty");
      else
        {
        if (Body.Length <= 0 || Body.Length > 500)
          validationDictionary.AddError("Body", "Review Body length must be between 1 and 500 characters");
        }

      if (Score < 0 || Score > 5)
        validationDictionary.AddError("Score", "Review Score must be between 1 and 5");


      if (GuestID == Guid.Empty)
        {
        if (EmailValidator.IsValidEmailAddress(GuestEmail) == false)
          validationDictionary.AddError("Guest", "Either the Guests ID or Gmail must be specified");

        }

      }

    }
  }
