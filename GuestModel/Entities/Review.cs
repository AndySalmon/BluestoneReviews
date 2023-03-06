using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bluestone.guests.model.Entities
  {
  [Table("reviews")]
  public class Review
    {

    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID { get; set; }

    [ForeignKey("Guest")]
    public Guid GuestID { get; set; }
    public Guest Guest { get; set; }

    [MaxLength(250)]
    public string Title { get; set; } = "";

    [MaxLength(500)]
    public string Body { get; set; } = "";

    public int Score { get; set; } = 0;

    }
  }


