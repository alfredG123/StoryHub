using Web.Data.ID;
using Web.Data.Stories;
using Web.Models;

namespace Web.Data.Characters
{
    [Serializable()]
    public class CharacterDataList
    : BaseDataList<CharacterID, CharacterData>
    {
        private readonly StoryID _story_id = new();

        /// <summary>
        /// Retrieve all character data from the database for the story
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="db_context"></param>
        public CharacterDataList(StoryID story_id, ProgramDbContext db_context)
            : base()
        {
            _story_id = story_id;

            RetrieveData(db_context);
        }

        /// <summary>
        /// Return the character data by the specified ID
        /// </summary>
        /// <param name="character_id"></param>
        /// <returns></returns>
        public override CharacterData? GetListItem(CharacterID character_id)
        {
            return (this.Where(list_item => list_item.CharacterID == character_id).SingleOrDefault());
        }

        /// <summary>
        /// Retrieve all character data from database
        /// </summary>
        /// <param name="db_context"></param>
        protected override void RetrieveData(ProgramDbContext db_context)
        {
            // NOTE: Close the connection first by using ToList() instead iterating db_context.CharacterData
            List<CharacterDataModel> character_data_model_list = db_context.CharacterData.ToList();

            if (_story_id.IsSet)
            {
                StoryCharacterLinks story_character_links = new(_story_id, db_context);

                foreach (CharacterDataModel character_data_model in character_data_model_list)
                {
                    if (story_character_links.IndexOfID(new CharacterID(character_data_model.ID)) != -1)
                    {
                        this.Add(new CharacterData(character_data_model));
                    }
                }
            }
        }
    }
}
