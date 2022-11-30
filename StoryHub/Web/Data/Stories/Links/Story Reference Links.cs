using Web.Data.ID;
using Web.Models;

namespace Web.Data.Stories
{
    [Serializable()]
    public class StoryReferenceLinks
        : BaseLinks<StoryID, ReferenceID>
    {
        /// <summary>
        /// Retrieve all story reference link items from database for the specified story
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="db_context"></param>
        public StoryReferenceLinks(StoryID story_id, ProgramDbContext db_context)
            : base(LinkType.ByLeft, story_id, db_context)
        {
        }

        /// <summary>
        /// Retrieve all story reference link items from database for the specified reference
        /// </summary>
        /// <param name="reference_id"></param>
        /// <param name="db_context"></param>
        public StoryReferenceLinks(ReferenceID reference_id, ProgramDbContext db_context)
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
            // NOTE: Close the connection first by using ToList() instead iterating db_context.StoryReferenceLinkItemModel
            List<StoryReferenceLinkItemModel> story_reference_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                story_reference_link_item_model_list = db_context.StoryReferenceLinkItem.Where(list_item => list_item.StoryID == id.Value).ToList();
            }
            else
            {
                story_reference_link_item_model_list = db_context.StoryReferenceLinkItem.Where(list_item => list_item.ReferenceID == id.Value).ToList();
            }

            foreach (StoryReferenceLinkItemModel story_reference_link_item_model in story_reference_link_item_model_list)
            {
                this.Add(new StoryReferenceLinkItem(story_reference_link_item_model));
            }
        }

        /// <summary>
        /// Create a link item
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="reference_id"></param>
        /// <returns></returns>
        protected override BaseLinkItem<StoryID, ReferenceID> CreateLinkItem(StoryID story_id, ReferenceID reference_id)
        {
            StoryReferenceLinkItemModel link_item_model = new();
            link_item_model.StoryID = story_id.Value;
            link_item_model.ReferenceID = reference_id.Value;

            return (new StoryReferenceLinkItem(link_item_model));
        }
    }

    [Serializable()]
    public class StoryReferenceLinkItem
         : BaseLinkItem<StoryID, ReferenceID>
    {
        /// <summary>
        /// Load the story reference link item model
        /// </summary>
        /// <param name="story_reference_link_item_model"></param>
        public StoryReferenceLinkItem(StoryReferenceLinkItemModel story_reference_link_item_model)
            : base(new StoryReferenceID(story_reference_link_item_model.ID), new StoryID(story_reference_link_item_model.StoryID), new ReferenceID(story_reference_link_item_model.ReferenceID), story_reference_link_item_model)
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
        /// Update the data object for retrieving the data from the database
        /// </summary>
        protected override void UpdateDataObject()
        {
            StoryReferenceLinkItemModel story_reference_link_item_model = (StoryReferenceLinkItemModel)this.BaseModel;
            StoryReferenceID story_reference_id = new(story_reference_link_item_model.ID);

            this.ID = story_reference_id;
            this.IsSet = story_reference_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            StoryReferenceLinkItemModel story_reference_link_item_model = (StoryReferenceLinkItemModel)this.BaseModel;

            story_reference_link_item_model.ID = this.ID.Value;
            story_reference_link_item_model.StoryID = this.StoryID.Value;
            story_reference_link_item_model.ReferenceID = this.ReferenceID.Value;
        }
    }
}
