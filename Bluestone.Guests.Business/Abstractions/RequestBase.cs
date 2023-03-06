using bluestone.guests.business.Validations;

namespace bluestone.guests.business.Abstractions
  {
  public abstract class RequestBase
    {

    public abstract void Validate(IValidationDictionary validationDictionary);
    }
  }
