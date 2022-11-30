using Web.Data.ID;
using Web.Models;

namespace Web.Data.Characters
{
    [Serializable()]
    public class CharacterReferenceLinks
        : BaseLinks<CharacterID, ReferenceID>
    {
        /// <summary>
        /// Retrieve all character reference link items from database for the specified character
        /// </summary>
        /// <param name="character_id"></param>
        /// <param name="db_context"></param>
        public CharacterReferenceLinks(CharacterID character_id, ProgramDbContext db_context)
            : base(LinkType.ByLeft, character_id, db_context)
        {
        }

        /// <summary>
        /// Retrieve all character reference link items from database for the specified reference
        /// </summary>
        /// <param name="reference_id"></param>
        /// <param name="db_context"></param>
        public CharacterReferenceLinks(ReferenceID reference_id, ProgramDbContext db_context)
            : base(LinkType.ByRight, reference_id, db_context)
        {
        }

        /// <summary>
        /// Retrieve all link items from database
        /// </summary>
        /// <param name="link_type"></param>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        protected override void RetrieveLinks(LinkType link_type, BaseID id, ProgramDbContext db_context)
        {
            // NOTE: Close the connection first by using ToList() instead iterating db_context.CharacterReferenceLinkItem
            List<CharacterReferenceLinkItemModel> character_reference_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                character_reference_link_item_model_list = db_context.CharacterReferenceLinkItem.Where(list_item => list_item.CharacterID == id.Value).ToList();
            }
            else
            {
                character_reference_link_item_model_list = db_context.CharacterReferenceLinkItem.Where(list_item => list_item.ReferenceID == id.Value).ToList();
            }

            foreach (CharacterReferenceLinkItemModel character_reference_link_item_model in character_reference_link_item_model_list)
            {
                this.Add(new CharacterReferenceLinkItem(character_reference_link_item_model));
            }
        }

        /// <summary>
        /// Create a link item
        /// </summary>
        /// <param name="character_id"></param>
        /// <param name="reference_id"></param>
        /// <returns></returns>
        protected override BaseLinkItem<CharacterID, ReferenceID> CreateLinkItem(CharacterID character_id, ReferenceID reference_id)
        {
            CharacterReferenceLinkItemModel link_item_model = new();
            link_item_model.CharacterID = character_id.Value;
            link_item_model.ReferenceID = reference_id.Value;

            return (new CharacterReferenceLinkItem(link_item_model));
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
        public CharacterReferenceLinkItem(CharacterReferenceLinkItemModel character_reference_link_item_model)
            : base(new CharacterReferenceID(character_reference_link_item_model.ID), new CharacterID(character_reference_link_item_model.CharacterID), new ReferenceID(character_reference_link_item_model.ReferenceID), character_reference_link_item_model)
        {
        }

        /// <summary>
        /// Return or set the character ID
        /// </summary>
        public CharacterID CharacterID
        {
            get
            {
                return (base.LeftID);
            }

            set
            {
                base.LeftID = value;
            }
        }

        /// <summary>
        /// Return or set the reference ID
        /// </summary>
        public ReferenceID ReferenceID
        {
            get
            {
                return (base.RightID);
            }

            set
            {
                base.RightID = value;
            }
        }

        /// <summary>
        /// Return or set the description of the link item
        /// </summary>
        public string Description { get; set; } = String.Empty;

        /// <summary>
        /// Update the data object for retrieving the data from the database
        /// </summary>
        protected override void UpdateDataObject()
        {
            CharacterReferenceLinkItemModel character_reference_link_item_model = (CharacterReferenceLinkItemModel)this.BaseModel;
            CharacterReferenceID character_reference_id = new(character_reference_link_item_model.ID);

            this.ID = character_reference_id;
            this.Description = character_reference_link_item_model.Description;
            this.IsSet = character_reference_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            CharacterReferenceLinkItemModel character_reference_link_item_model = (CharacterReferenceLinkItemModel)this.BaseModel;

            character_reference_link_item_model.ID = this.ID.Value;
            character_reference_link_item_model.CharacterID = this.CharacterID.Value;
            character_reference_link_item_model.ReferenceID = this.ReferenceID.Value;
            character_reference_link_item_model.Description = this.Description;
        }
    }
}
