using bluestone.guests.model.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace bluestone.guests.business.Abstractions.V1
  {
  public class ReviewResponse : ResponseBase
    {
    public Guid ID { get; set; }

    public GuestResponse Guest { get; set; }
    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public int Score { get; set; } = 0;
    }
  }
