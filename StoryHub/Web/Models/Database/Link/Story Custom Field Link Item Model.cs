using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class StoryCustomFieldModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public int StoryID { get; set; } = 0;

        [Required]
        public int CustomFieldID { get; set; } = 0;
    }
}
