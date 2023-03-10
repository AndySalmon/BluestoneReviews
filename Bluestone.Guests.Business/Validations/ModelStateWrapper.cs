using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace bluestone.guests.business.Validations
  {
  public class ModelStateWrapper : IValidationDictionary
    {

    private ModelStateDictionary _modelState;

    public ModelStateWrapper(ModelStateDictionary modelState)
      {
      _modelState = modelState;
      }

    #region IValidationDictionary Members

    public void AddError(string key, string errorMessage)
      {
      _modelState.AddModelError(key, errorMessage);
      }

    public bool IsValid
      {
      get { return _modelState.IsValid; }
      }

    #endregion
    }
  }