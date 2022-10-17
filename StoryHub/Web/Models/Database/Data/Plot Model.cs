using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class PlotModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }

        [Required]
        public int PlotType { get; set; } = 0;

        [Required]
        public int DramaType { get; set; } = 0;

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Goal { get; set; } = string.Empty;

        [Required]
        public string Scene { get; set; } = string.Empty;

        [Required]
        public string Content { get; set; } = string.Empty;
    }
}
