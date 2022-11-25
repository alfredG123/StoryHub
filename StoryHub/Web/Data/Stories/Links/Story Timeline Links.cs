using Web.Data.ID;
using Web.Models;

namespace Web.Data.Stories
{
    [Serializable()]
    public class StoryTimelineLinks
        : BaseLinks<StoryID, TimelineID>
    {
        /// <summary>
        /// Retrieve all story timeline link items from database for the specified story
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="db_context"></param>
        public StoryTimelineLinks(StoryID story_id, ProgramDbContext db_context)
            : base(LinkType.ByLeft, story_id, db_context)
        {
        }

        /// <summary>
        /// Retrieve all story timeline link items from database for the specified timeline
        /// </summary>
        /// <param name="timeline_id"></param>
        /// <param name="db_context"></param>
        public StoryTimelineLinks(TimelineID timeline_id, ProgramDbContext db_context)
            : base(LinkType.ByRight, timeline_id, db_context)
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
            // NOTE: Close the connection first by using ToList() instead iterating db_context.StoryTimelineLinkItemModel
            List<StoryTimelineLinkItemModel> story_timeline_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                story_timeline_link_item_model_list = db_context.StoryTimelineLinkItem.Where(list_item => list_item.StoryID == id.Value).ToList();
            }
            else
            {
                story_timeline_link_item_model_list = db_context.StoryTimelineLinkItem.Where(list_item => list_item.TimelineID == id.Value).ToList();
            }

            foreach (StoryTimelineLinkItemModel story_timeline_link_item_model in story_timeline_link_item_model_list)
            {
                this.Add(new StoryTimelineLinkItem(story_timeline_link_item_model));
            }
        }

        /// <summary>
        /// Create a link item
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="timeline_id"></param>
        /// <returns></returns>
        protected override BaseLinkItem<StoryID, TimelineID> CreateLinkItem(StoryID story_id, TimelineID timeline_id)
        {
            StoryTimelineLinkItemModel link_item_model = new();
            link_item_model.StoryID = story_id.Value;
            link_item_model.TimelineID = timeline_id.Value;

            return (new StoryTimelineLinkItem(link_item_model));
        }
    }

    [Serializable()]
    public class StoryTimelineLinkItem
         : BaseLinkItem<StoryID, TimelineID>
    {
        /// <summary>
        /// Load the story timeline link item model
        /// </summary>
        /// <param name="story_timeline_link_item_model"></param>
        public StoryTimelineLinkItem(StoryTimelineLinkItemModel story_timeline_link_item_model)
            : base(new StoryTimelineID(story_timeline_link_item_model.ID), new StoryID(story_timeline_link_item_model.StoryID), new TimelineID(story_timeline_link_item_model.TimelineID), story_timeline_link_item_model)
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
        /// Return or set the timeline ID
        /// </summary>
        public TimelineID TimelineID
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
            StoryTimelineLinkItemModel story_timeline_link_item_model = (StoryTimelineLinkItemModel)this.BaseModel;
            StoryTimelineID story_timeline_id = new(story_timeline_link_item_model.ID);

            this.ID = story_timeline_id;
            this.IsSet = story_timeline_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            StoryTimelineLinkItemModel story_timeline_link_item_model = (StoryTimelineLinkItemModel)this.BaseModel;

            story_timeline_link_item_model.ID = this.ID.Value;
            story_timeline_link_item_model.StoryID = this.StoryID.Value;
            story_timeline_link_item_model.TimelineID = this.TimelineID.Value;
        }
    }
}
