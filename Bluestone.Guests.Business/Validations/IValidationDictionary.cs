namespace bluestone.guests.business.Validations
  {

  // see: https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/models-data/validating-with-a-service-layer-cs

  public interface IValidationDictionary
    {
    void AddError(string key, string errorMessage);
    bool IsValid { get; }
    }
  }