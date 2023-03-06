using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bluestone.guests.business.Abstractions.V1
  {
  public class CreateReviewResponse : ResponseBase
    {
    public Guid ID { get; set; }
    public Guid GuestID { get; set; }

    public string Title { get; set; } = "";
    public string Body { get; set; } = "";
    public int Score { get; set; } = 0;
    }
  }
