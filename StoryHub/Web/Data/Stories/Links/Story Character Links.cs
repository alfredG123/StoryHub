using Web.Data.ID;
using Web.Models;

namespace Web.Data.Stories
{
    [Serializable()]
    public class StoryCharacterLinks
        : BaseLinks<StoryID, CharacterID>
    {
        /// <summary>
        /// Retrieve all story character link items from database for the specified story
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="db_context"></param>
        public StoryCharacterLinks(StoryID story_id, ProgramDbContext db_context)
            : base(LinkType.ByLeft, story_id, db_context)
        {
        }

        /// <summary>
        /// Retrieve all story character link items from database for the specified character
        /// </summary>
        /// <param name="character_id"></param>
        /// <param name="db_context"></param>
        public StoryCharacterLinks(CharacterID character_id, ProgramDbContext db_context)
            : base(LinkType.ByRight, character_id, db_context)
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
            // NOTE: Close the connection first by using ToList() instead iterating db_context.StoryCharacterLinkItemModel
            List<StoryCharacterLinkItemModel> story_character_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                story_character_link_item_model_list = db_context.StoryCharacterLinkItem.Where(list_item => list_item.StoryID == id.Value).ToList();
            }
            else
            {
                story_character_link_item_model_list = db_context.StoryCharacterLinkItem.Where(list_item => list_item.CharacterID == id.Value).ToList();
            }

            foreach (StoryCharacterLinkItemModel story_character_link_item_model in story_character_link_item_model_list)
            {
                this.Add(new StoryCharacterLinkItem(story_character_link_item_model));
            }
        }

        /// <summary>
        /// Create a link item
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="character_id"></param>
        /// <returns></returns>
        protected override BaseLinkItem<StoryID, CharacterID> CreateLinkItem(StoryID story_id, CharacterID character_id)
        {
            StoryCharacterLinkItemModel link_item_model = new();
            link_item_model.StoryID = story_id.Value;
            link_item_model.CharacterID = character_id.Value;

            return (new StoryCharacterLinkItem(link_item_model));
        }
    }

    [Serializable()]
    public class StoryCharacterLinkItem
         : BaseLinkItem<StoryID, CharacterID>
    {
        /// <summary>
        /// Load the story character link item model
        /// </summary>
        /// <param name="story_character_link_item_model"></param>
        public StoryCharacterLinkItem(StoryCharacterLinkItemModel story_character_link_item_model)
            : base(new StoryCharacterID(story_character_link_item_model.ID), new StoryID(story_character_link_item_model.StoryID), new CharacterID(story_character_link_item_model.CharacterID), story_character_link_item_model)
        {
        }

        /// <summary>
        /// Return or set the story ID
        /// </summary>
        public StoryID StoryID
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
            StoryCharacterLinkItemModel story_character_link_item_model = (StoryCharacterLinkItemModel)this.BaseModel;
            StoryCharacterID story_character_id = new(story_character_link_item_model.ID);

            this.ID = story_character_id;
            this.IsSet = story_character_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            StoryCharacterLinkItemModel story_character_link_item_model = (StoryCharacterLinkItemModel)this.BaseModel;

            story_character_link_item_model.ID = this.ID.Value;
            story_character_link_item_model.StoryID = this.StoryID.Value;
            story_character_link_item_model.CharacterID = this.CharacterID.Value;
        }
    }
}
