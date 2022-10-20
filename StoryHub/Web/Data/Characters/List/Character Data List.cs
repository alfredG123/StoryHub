using Web.Data.ID;
using Web.Models;

namespace Web.Data.Characters
{
    [Serializable()]
    public class CharacterDataList
    : BaseDataList<CharacterID, CharacterData>
    {
        /// <summary>
        /// Retrieve all character data from the database
        /// </summary>
        /// <param name="db_context"></param>
        public CharacterDataList(ProgramDbContext db_context)
            : base(db_context)
        {
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
            List<CharacterModel> character_data_model_list = db_context.CharacterData.ToList();

            foreach (CharacterModel character_data_model in character_data_model_list)
            {
                this.Add(new CharacterData(character_data_model));
            }
        }
    }
}
