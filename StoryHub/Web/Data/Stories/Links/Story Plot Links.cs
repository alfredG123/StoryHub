using Web.Data.ID;
using Web.Models;

namespace Web.Data.Stories
{
    [Serializable()]
    public class StoryPlotLinks
        : BaseLinks<StoryID, PlotID>
    {
        /// <summary>
        /// Retrieve all story plot link items from database
        /// </summary>
        /// <param name="link_type"></param>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        public StoryPlotLinks(LinkType link_type, BaseID id, ProgramDbContext db_context)
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
            // NOTE: Close the connection first by using ToList() instead iterating db_context.StoryPlotLinkItemModel
            List<StoryPlotLinkItemModel> story_plot_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                story_plot_link_item_model_list = db_context.StoryPlotLinkItem.Where(list_item => list_item.StoryID == id.Value).ToList();
            }
            else
            {
                story_plot_link_item_model_list = db_context.StoryPlotLinkItem.Where(list_item => list_item.PlotID == id.Value).ToList();
            }

            foreach (StoryPlotLinkItemModel story_plot_link_item_model in story_plot_link_item_model_list)
            {
                this.Add(new StoryPlotLinkItem(story_plot_link_item_model));
            }
        }

        /// <summary>
        /// Create a link item
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="plot_id"></param>
        /// <returns></returns>
        protected override BaseLinkItem<StoryID, PlotID> CreateLinkItem(StoryID story_id, PlotID plot_id)
        {
            StoryPlotLinkItemModel link_item_model = new();
            link_item_model.StoryID = story_id.Value;
            link_item_model.PlotID = plot_id.Value;

            return (new StoryPlotLinkItem(link_item_model));
        }
    }

    [Serializable()]
    public class StoryPlotLinkItem
         : BaseLinkItem<StoryID, PlotID>
    {
        /// <summary>
        /// Load the story plot link item model
        /// </summary>
        /// <param name="story_plot_link_item_model"></param>
        public StoryPlotLinkItem(StoryPlotLinkItemModel story_plot_link_item_model)
            : base(new StoryPlotID(story_plot_link_item_model.ID), new StoryID(story_plot_link_item_model.StoryID), new PlotID(story_plot_link_item_model.PlotID), story_plot_link_item_model)
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
        /// Return or set the plot ID
        /// </summary>
        public PlotID PlotID
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
            StoryPlotLinkItemModel story_plot_link_item_model = (StoryPlotLinkItemModel)this.BaseModel;
            StoryPlotID story_plot_id = new(story_plot_link_item_model.ID);

            this.ID = story_plot_id;
            this.IsSet = story_plot_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            StoryPlotLinkItemModel story_plot_link_item_model = (StoryPlotLinkItemModel)this.BaseModel;

            story_plot_link_item_model.ID = this.ID.Value;
            story_plot_link_item_model.StoryID = this.StoryID.Value;
            story_plot_link_item_model.PlotID = this.PlotID.Value;
        }
    }
}
