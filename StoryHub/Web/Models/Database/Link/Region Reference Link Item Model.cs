using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegionReferenceLinkItemModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public int RegionID { get; set; } = 0;

        [Required]
        public int ReferenceID { get; set; } = 0;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
