using Web.Data.ID;
using Web.Models;

namespace Web.Data.Plots
{
    [Serializable()]
    public class PlotPlotLinks
        : BaseLinks<PlotID, PlotID>
    {
        /// <summary>
        /// Retrieve all plot plot link items from database
        /// </summary>
        /// <param name="link_type"></param>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        public PlotPlotLinks(LinkType link_type, BaseID id, ProgramDbContext db_context)
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
            // NOTE: Close the connection first by using ToList() instead iterating db_context.PlotPlotLinkItemModel
            List<PlotPlotLinkItemModel> plot_plot_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                plot_plot_link_item_model_list = db_context.PlotPlotLinkItem.Where(list_item => list_item.PlotID == id.Value).ToList();
            }
            else
            {
                plot_plot_link_item_model_list = db_context.PlotPlotLinkItem.Where(list_item => list_item.SubPlotID == id.Value).ToList();
            }

            foreach (PlotPlotLinkItemModel plot_plot_link_item_model in plot_plot_link_item_model_list)
            {
                this.Add(new PlotPlotLinkItem(plot_plot_link_item_model));
            }
        }

        /// <summary>
        /// Create a link item
        /// </summary>
        /// <param name="plot_id"></param>
        /// <param name="sub_plot_id"></param>
        /// <returns></returns>
        protected override BaseLinkItem<PlotID, PlotID> CreateLinkItem(PlotID plot_id, PlotID sub_plot_id)
        {
            PlotPlotLinkItemModel link_item_model = new();
            link_item_model.PlotID = plot_id.Value;
            link_item_model.SubPlotID = sub_plot_id.Value;

            return (new PlotPlotLinkItem(link_item_model));
        }
    }

    [Serializable()]
    public class PlotPlotLinkItem
         : BaseLinkItem<PlotID, PlotID>
    {
        /// <summary>
        /// Load the plot plot link item model
        /// </summary>
        /// <param name="plot_plot_link_item_model"></param>
        public PlotPlotLinkItem(PlotPlotLinkItemModel plot_plot_link_item_model)
            : base(new PlotPlotID(plot_plot_link_item_model.ID), new PlotID(plot_plot_link_item_model.PlotID), new PlotID(plot_plot_link_item_model.SubPlotID), plot_plot_link_item_model)
        {
        }

        /// <summary>
        /// Return or set the plot ID
        /// </summary>
        public PlotID PlotID
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
        /// Return or set the plot ID for sub-plot
        /// </summary>
        public PlotID SubPlotID
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
            PlotPlotLinkItemModel plot_plot_link_item_model = (PlotPlotLinkItemModel)this.BaseModel;
            PlotPlotID plot_plot_id = new(plot_plot_link_item_model.ID);

            this.ID = plot_plot_id;
            this.IsSet = plot_plot_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            PlotPlotLinkItemModel plot_plot_link_item_model = (PlotPlotLinkItemModel)this.BaseModel;

            plot_plot_link_item_model.ID = this.ID.Value;
            plot_plot_link_item_model.PlotID = this.PlotID.Value;
            plot_plot_link_item_model.SubPlotID = this.SubPlotID.Value;
        }
    }
}
