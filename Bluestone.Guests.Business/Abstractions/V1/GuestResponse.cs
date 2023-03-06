using bluestone.guests.model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bluestone.guests.business.Abstractions.V1
  {
  public class GuestResponse : ResponseBase
    {
    public Guid ID { get; set; }

    public string Title { get; set; } = "";
    public string ForeNames { get; set; } = "";
    public string SurName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
    }
  }
