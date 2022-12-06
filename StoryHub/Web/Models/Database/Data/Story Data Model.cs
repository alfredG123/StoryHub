using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class StoryDataModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;

        [Required]
        public string Introduction { get; set; } = string.Empty;
    }
}
