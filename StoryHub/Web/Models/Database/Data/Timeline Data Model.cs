using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class TimelineDataModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public string Time { get; set; } = string.Empty;
    }
}
