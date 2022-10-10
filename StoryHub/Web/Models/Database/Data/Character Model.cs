using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CharacterModel : BaseModel
    {
        [Key]
        public override int ID { get; set; }
    }
}
