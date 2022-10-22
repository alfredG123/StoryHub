using Web.Data.ID;
using Web.Models;

namespace Web.Data.Characters
{
    [Serializable()]
    public class CharacterReferenceLinks
        : BaseLinks<CharacterID, ReferenceID>
    {
        /// <summary>
        /// Retrieve all character reference link items from database
        /// </summary>
        /// <param name="db_context"></param>
        public CharacterReferenceLinks(ProgramDbContext db_context)
            : base(db_context)
        {
        }

        /// <summary>
        /// Retrieve all link items from database
        /// </summary>
        /// <param name="db_context"></param>
        protected override void RetrieveLinks(ProgramDbContext db_context)
        {
            // NOTE: Close the connection first by using ToList() instead iterating db_context.CharacterReferenceLinkItem
            List<CharacterReferenceLinkItemModel> character_reference_link_item_model_list = db_context.CharacterReferenceLinkItem.ToList();

            foreach (CharacterReferenceLinkItemModel character_reference_link_item_model in character_reference_link_item_model_list)
            {
                this.Add(new CharacterReferenceLinkItem(character_reference_link_item_model, db_context));
            }
        }
    }

    [Serializable()]
    public class CharacterReferenceLinkItem
         : BaseLinkItem<CharacterID, ReferenceID>
    {
        /// <summary>
        /// Load the character reference link item model
        /// </summary>
        /// <param name="character_reference_link_item_model"></param>
        /// <param name="db_context"></param>
        public CharacterReferenceLinkItem(CharacterReferenceLinkItemModel character_reference_link_item_model, ProgramDbContext db_context)
            : base(character_reference_link_item_model, db_context)
        {
            this.LeftID = new CharacterID(character_reference_link_item_model.CharacterID);
            this.RightID = new ReferenceID(character_reference_link_item_model.ReferenceID);
        }

        /// <summary>
        /// Return the character ID
        /// </summary>
        public CharacterID CharacterID { get => this.LeftID; }

        /// <summary>
        /// Return the reference ID
        /// </summary>
        public ReferenceID ReferenceID { get => this.RightID; }

        /// <summary>
        /// Return the character ID
        /// </summary>
        protected override CharacterID LeftID { get; }

        /// <summary>
        /// Return the reference ID
        /// </summary>
        protected override ReferenceID RightID { get; }
    }
}
