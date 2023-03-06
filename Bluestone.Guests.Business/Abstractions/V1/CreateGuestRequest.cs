using bluestone.guests.business.Validations;

namespace bluestone.guests.business.Abstractions.V1
  {
  public class CreateGuestRequest : RequestBase
    {
    public string Title { get; set; } = "";
    public string ForeNames { get; set; } = "";
    public string SurName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";





    // Validate the request data

    public override void Validate(IValidationDictionary validationDictionary)
      {
      ResponseBase _response = new CreateGuestResponse();

      if (Title.Length > 20)
        validationDictionary.AddError("Title", "Guest Title length must be less than 20 characters");

      if (ForeNames.Length > 100)
        validationDictionary.AddError("ForeNames", "Guest ForeNames length must be less than 100 characters");

      if (SurName.Length > 100)
        validationDictionary.AddError("SurName", "Guest SurName length must be less than 100 characters");

      if (Phone.Length > 100)
        validationDictionary.AddError("Phone", "Guest Phone length must be less than 100 characters");

      if (Email.Length == 0)
        validationDictionary.AddError("Email", "Guest Email must not be empty");
      else
        {
        if (Email.Length <= 0 || Email.Length > 500)
          validationDictionary.AddError("Email", "Guest Email length must be between 1 and 150 characters");
        else
          if (EmailValidator.IsValidEmailAddress(Email) == false)
            validationDictionary.AddError("Email", "Guest Email address is not valid");
        }


      }

    }
  }
