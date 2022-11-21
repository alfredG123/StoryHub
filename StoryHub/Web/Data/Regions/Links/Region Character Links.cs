using Web.Data.ID;
using Web.Models;

namespace Web.Data.Regions
{
    [Serializable()]
    public class RegionCharacterLinks
        : BaseLinks<RegionID, CharacterID>
    {
        /// <summary>
        /// Retrieve all region character link items from database
        /// </summary>
        /// <param name="link_type"></param>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        public RegionCharacterLinks(LinkType link_type, BaseID id, ProgramDbContext db_context)
            : base(link_type, id, db_context)
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
            // NOTE: Close the connection first by using ToList() instead iterating db_context.RegionCharacterLinkItemModel
            List<RegionCharacterLinkItemModel> region_character_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                region_character_link_item_model_list = db_context.RegionCharacterLinkItem.Where(list_item => list_item.RegionID == id.Value).ToList();
            }
            else
            {
                region_character_link_item_model_list = db_context.RegionCharacterLinkItem.Where(list_item => list_item.CharacterID == id.Value).ToList();
            }

            foreach (RegionCharacterLinkItemModel region_character_link_item_model in region_character_link_item_model_list)
            {
                this.Add(new RegionCharacterLinkItem(region_character_link_item_model));
            }
        }

        /// <summary>
        /// Create a link item
        /// </summary>
        /// <param name="region_id"></param>
        /// <param name="character_id"></param>
        /// <returns></returns>
        protected override BaseLinkItem<RegionID, CharacterID> CreateLinkItem(RegionID region_id, CharacterID character_id)
        {
            RegionCharacterLinkItemModel link_item_model = new();
            link_item_model.RegionID = region_id.Value;
            link_item_model.CharacterID = character_id.Value;

            return (new RegionCharacterLinkItem(link_item_model));
        }
    }

    [Serializable()]
    public class RegionCharacterLinkItem
         : BaseLinkItem<RegionID, CharacterID>
    {
        /// <summary>
        /// Load the region character link item model
        /// </summary>
        /// <param name="region_character_link_item_model"></param>
        public RegionCharacterLinkItem(RegionCharacterLinkItemModel region_character_link_item_model)
            : base(new RegionCharacterID(region_character_link_item_model.ID), new RegionID(region_character_link_item_model.RegionID), new CharacterID(region_character_link_item_model.CharacterID), region_character_link_item_model)
        {
        }

        /// <summary>
        /// Return or set the region ID
        /// </summary>
        public RegionID RegionID
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
        /// Return or set the character ID
        /// </summary>
        public CharacterID CharacterID
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
        /// Update the data object for retrieving the data from the database
        /// </summary>
        protected override void UpdateDataObject()
        {
            RegionCharacterLinkItemModel region_character_link_item_model = (RegionCharacterLinkItemModel)this.BaseModel;
            RegionCharacterID region_character_id = new(region_character_link_item_model.ID);

            this.ID = region_character_id;
            this.IsSet = region_character_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            RegionCharacterLinkItemModel region_character_link_item_model = (RegionCharacterLinkItemModel)this.BaseModel;

            region_character_link_item_model.ID = this.ID.Value;
            region_character_link_item_model.RegionID = this.RegionID.Value;
            region_character_link_item_model.CharacterID = this.CharacterID.Value;
        }
    }
}
