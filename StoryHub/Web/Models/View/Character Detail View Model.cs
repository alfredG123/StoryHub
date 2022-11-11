using Web.Data;
using Web.Data.Characters;
using Web.Data.ID;

namespace Web.Models
{
    public class CharacterDetailViewModel
    {
        /// <summary>
        /// Return or set the ID of the character
        /// </summary>
        public int CharacterID { get; set; } = 0;

        /// <summary>
        /// Return or set the name of the character
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Return or set the description of the character
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Convert the character data to the view model
        /// </summary>
        /// <param name="character_data"></param>
        /// <returns></returns>
        public static CharacterDetailViewModel ConvertToCharacterDetailViewModel(CharacterData character_data)
        {
            CharacterDetailViewModel character_detail_view_model = new();

            // Set up the properties with the character data
            character_detail_view_model.CharacterID = character_data.CharacterID.Value;
            character_detail_view_model.Name = character_data.Name;
            character_detail_view_model.Description = character_data.Description;

            return (character_detail_view_model);
        }

        /// <summary>
        /// Convert the view model to the character data
        /// </summary>
        /// <param name="character_detail_view_model"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        public static CharacterData ConvertToCharacterData(CharacterDetailViewModel character_detail_view_model, ProgramDbContext db_context)
        {
            CharacterData character_data = new(new CharacterID(character_detail_view_model.CharacterID), db_context);

            // Update the variables based on the data from the web page
            character_data.Name = character_detail_view_model.Name;
            character_data.Description = character_detail_view_model.Description;

            return (character_data);
        }
    }
}
