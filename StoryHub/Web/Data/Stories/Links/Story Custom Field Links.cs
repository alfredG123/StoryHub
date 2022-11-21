using Web.Data.ID;
using Web.Models;

namespace Web.Data.Stories
{
    [Serializable()]
    public class StoryCustomFieldLinks
        : BaseLinks<StoryID, CustomFieldID>
    {
        /// <summary>
        /// Retrieve all story custom field link items from database
        /// </summary>
        /// <param name="link_type"></param>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        public StoryCustomFieldLinks(LinkType link_type, BaseID id, ProgramDbContext db_context)
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
            // NOTE: Close the connection first by using ToList() instead iterating db_context.StoryCustomFieldLinkItemModel
            List<StoryCustomFieldLinkItemModel> story_custom_field_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                story_custom_field_link_item_model_list = db_context.StoryCustomFieldLinkItem.Where(list_item => list_item.StoryID == id.Value).ToList();
            }
            else
            {
                story_custom_field_link_item_model_list = db_context.StoryCustomFieldLinkItem.Where(list_item => list_item.CustomFieldID == id.Value).ToList();
            }

            foreach (StoryCustomFieldLinkItemModel story_custom_field_link_item_model in story_custom_field_link_item_model_list)
            {
                this.Add(new StoryCustomFieldLinkItem(story_custom_field_link_item_model));
            }
        }

        /// <summary>
        /// Create a link item
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="custom_field_id"></param>
        /// <returns></returns>
        protected override BaseLinkItem<StoryID, CustomFieldID> CreateLinkItem(StoryID story_id, CustomFieldID custom_field_id)
        {
            StoryCustomFieldLinkItemModel link_item_model = new();
            link_item_model.StoryID = story_id.Value;
            link_item_model.CustomFieldID = custom_field_id.Value;

            return (new StoryCustomFieldLinkItem(link_item_model));
        }
    }

    [Serializable()]
    public class StoryCustomFieldLinkItem
         : BaseLinkItem<StoryID, CustomFieldID>
    {
        /// <summary>
        /// Load the story custom field link item model
        /// </summary>
        /// <param name="story_custom_field_link_item_model"></param>
        public StoryCustomFieldLinkItem(StoryCustomFieldLinkItemModel story_custom_field_link_item_model)
            : base(new StoryCustomFieldID(story_custom_field_link_item_model.ID), new StoryID(story_custom_field_link_item_model.StoryID), new CustomFieldID(story_custom_field_link_item_model.CustomFieldID), story_custom_field_link_item_model)
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
        /// Return or set the custom field ID
        /// </summary>
        public CustomFieldID CustomFieldID
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
            StoryCustomFieldLinkItemModel story_custom_field_link_item_model = (StoryCustomFieldLinkItemModel)this.BaseModel;
            StoryCustomFieldID story_custom_field_id = new(story_custom_field_link_item_model.ID);

            this.ID = story_custom_field_id;
            this.IsSet = story_custom_field_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            StoryCustomFieldLinkItemModel story_custom_field_link_item_model = (StoryCustomFieldLinkItemModel)this.BaseModel;

            story_custom_field_link_item_model.ID = this.ID.Value;
            story_custom_field_link_item_model.StoryID = this.StoryID.Value;
            story_custom_field_link_item_model.CustomFieldID = this.CustomFieldID.Value;
        }
    }
}
