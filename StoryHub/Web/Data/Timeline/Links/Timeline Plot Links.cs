using Web.Data.ID;
using Web.Models;

namespace Web.Data.Timeline
{
    [Serializable()]
    public class TimelinePlotLinks
        : BaseLinks<TimelineID, PlotID>
    {
        /// <summary>
        /// Retrieve all timeline plot link items from database for the specified timeline
        /// </summary>
        /// <param name="timeline_id"></param>
        /// <param name="db_context"></param>
        public TimelinePlotLinks(TimelineID timeline_id, ProgramDbContext db_context)
            : base(LinkType.ByLeft, timeline_id, db_context)
        {
        }

        /// <summary>
        /// Retrieve all timeline plot link items from database for the specified plot
        /// </summary>
        /// <param name="region_id"></param>
        /// <param name="db_context"></param>
        public TimelinePlotLinks(PlotID plot_id, ProgramDbContext db_context)
            : base(LinkType.ByRight, plot_id, db_context)
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
            // NOTE: Close the connection first by using ToList() instead iterating db_context.TimelinePlotLinkItemModel
            List<TimelinePlotLinkItemModel> timeline_plot_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                timeline_plot_link_item_model_list = db_context.TimelinePlotLinkItem.Where(list_item => list_item.TimelineID == id.Value).ToList();
            }
            else
            {
                timeline_plot_link_item_model_list = db_context.TimelinePlotLinkItem.Where(list_item => list_item.PlotID == id.Value).ToList();
            }

            foreach (TimelinePlotLinkItemModel timeline_plot_link_item_model in timeline_plot_link_item_model_list)
            {
                this.Add(new TimelinePlotLinkItem(timeline_plot_link_item_model));
            }
        }

        /// <summary>
        /// Create a link item
        /// </summary>
        /// <param name="timeline_id"></param>
        /// <param name="plot_id"></param>
        /// <returns></returns>
        protected override BaseLinkItem<TimelineID, PlotID> CreateLinkItem(TimelineID timeline_id, PlotID plot_id)
        {
            TimelinePlotLinkItemModel link_item_model = new();
            link_item_model.TimelineID = timeline_id.Value;
            link_item_model.PlotID = plot_id.Value;

            return (new TimelinePlotLinkItem(link_item_model));
        }
    }

    [Serializable()]
    public class TimelinePlotLinkItem
         : BaseLinkItem<TimelineID, PlotID>
    {
        /// <summary>
        /// Load the timeline plot link item model
        /// </summary>
        /// <param name="timeline_plot_link_item_model"></param>
        public TimelinePlotLinkItem(TimelinePlotLinkItemModel timeline_plot_link_item_model)
            : base(new TimelinePlotID(timeline_plot_link_item_model.ID), new TimelineID(timeline_plot_link_item_model.TimelineID), new PlotID(timeline_plot_link_item_model.PlotID), timeline_plot_link_item_model)
        {
        }

        /// <summary>
        /// Return or set the timeline ID
        /// </summary>
        public TimelineID TimelineID
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
            TimelinePlotLinkItemModel timeline_plot_link_item_model = (TimelinePlotLinkItemModel)this.BaseModel;
            TimelinePlotID timeline_plot_id = new(timeline_plot_link_item_model.ID);

            this.ID = timeline_plot_id;
            this.IsSet = timeline_plot_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            TimelinePlotLinkItemModel timeline_plot_link_item_model = (TimelinePlotLinkItemModel)this.BaseModel;

            timeline_plot_link_item_model.ID = this.ID.Value;
            timeline_plot_link_item_model.TimelineID = this.TimelineID.Value;
            timeline_plot_link_item_model.PlotID = this.PlotID.Value;
        }
    }
}
