using System.ComponentModel.DataAnnotations;

namespace bluestoneguests.web.ViewModels
  {
  public class AddReviewViewModel
    {
    [MaxLength(150)]
    public string Email { get; set; }

    [MaxLength(250)]
    public string Title { get; set; }

    [MaxLength(500)]
    public string Body { get; set; }

    public int Score { get; set; } = 0;

    }
  }
