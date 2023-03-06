using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bluestone.guests.model.Entities
  {
  [Table("guests")]
  public class Guest
    {

    [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid ID { get; set; }

    [MaxLength(20)]
    public string Title { get; set; } = "";

    [MaxLength(100)]
    public string ForeNames { get; set; } = "";

    [MaxLength(100)]
    public string SurName { get; set; } = "";

    //[MaxLength(100)]
    //public string AddressLine1 { get; set; } = "";

    //[MaxLength(100)]
    //public string AddressLine2 { get; set; } = "";

    //[MaxLength(100)]
    //public string AddressLine3 { get; set; } = "";

    //[MaxLength(100)]
    //public string AddressTown { get; set; } = "";

    //[MaxLength(100)]
    //public string AddressCounty { get; set; } = "";

    //[MaxLength(20)]
    //public string AddressPostcode { get; set; } = "";

    //[MaxLength(100)]
    //public string AddressCountry { get; set; } = "";

    [MaxLength(30)]
    public string Phone { get; set; } = "";

    [MaxLength(150)]
    public string Email { get; set; } = "";

    public List<Review> Reviews { get; set; } = new List<Review>();
    }
  }