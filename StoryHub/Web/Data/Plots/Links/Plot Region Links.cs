using Web.Data.ID;
using Web.Models;

namespace Web.Data.Plots
{
    [Serializable()]
    public class PlotRegionLinks
        : BaseLinks<PlotID, RegionID>
    {
        /// <summary>
        /// Retrieve all plot region link items from database for the specified plot
        /// </summary>
        /// <param name="plot_id"></param>
        /// <param name="db_context"></param>
        public PlotRegionLinks(PlotID plot_id, ProgramDbContext db_context)
            : base(LinkType.ByLeft, plot_id, db_context)
        {
        }

        /// <summary>
        /// Retrieve all plot region link items from database for the specified region
        /// </summary>
        /// <param name="region_id"></param>
        /// <param name="db_context"></param>
        public PlotRegionLinks(RegionID region_id, ProgramDbContext db_context)
            : base(LinkType.ByRight, region_id, db_context)
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
            // NOTE: Close the connection first by using ToList() instead iterating db_context.PlotRegionLinkItemModel
            List<PlotRegionLinkItemModel> plot_region_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                plot_region_link_item_model_list = db_context.PlotRegionLinkItem.Where(list_item => list_item.PlotID == id.Value).ToList();
            }
            else
            {
                plot_region_link_item_model_list = db_context.PlotRegionLinkItem.Where(list_item => list_item.RegionID == id.Value).ToList();
            }

            foreach (PlotRegionLinkItemModel plot_region_link_item_model in plot_region_link_item_model_list)
            {
                this.Add(new PlotRegionLinkItem(plot_region_link_item_model));
            }
        }

        /// <summary>
        /// Create a link item
        /// </summary>
        /// <param name="plot_id"></param>
        /// <param name="region_id"></param>
        /// <returns></returns>
        protected override BaseLinkItem<PlotID, RegionID> CreateLinkItem(PlotID plot_id, RegionID region_id)
        {
            PlotRegionLinkItemModel link_item_model = new();
            link_item_model.PlotID = plot_id.Value;
            link_item_model.RegionID = region_id.Value;

            return (new PlotRegionLinkItem(link_item_model));
        }
    }

    [Serializable()]
    public class PlotRegionLinkItem
         : BaseLinkItem<PlotID, RegionID>
    {
        /// <summary>
        /// Load the plot region link item model
        /// </summary>
        /// <param name="plot_region_link_item_model"></param>
        public PlotRegionLinkItem(PlotRegionLinkItemModel plot_region_link_item_model)
            : base(new PlotRegionID(plot_region_link_item_model.ID), new PlotID(plot_region_link_item_model.PlotID), new RegionID(plot_region_link_item_model.RegionID), plot_region_link_item_model)
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
        /// Return or set the region ID
        /// </summary>
        public RegionID RegionID
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
        /// Return or set the description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Update the data object for retrieving the data from the database
        /// </summary>
        protected override void UpdateDataObject()
        {
            PlotRegionLinkItemModel plot_region_link_item_model = (PlotRegionLinkItemModel)this.BaseModel;
            PlotRegionID plot_region_id = new(plot_region_link_item_model.ID);

            this.ID = plot_region_id;
            this.Description = plot_region_link_item_model.Description;
            this.IsSet = plot_region_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            PlotRegionLinkItemModel plot_region_link_item_model = (PlotRegionLinkItemModel)this.BaseModel;

            plot_region_link_item_model.ID = this.ID.Value;
            plot_region_link_item_model.PlotID = this.PlotID.Value;
            plot_region_link_item_model.RegionID = this.RegionID.Value;
            plot_region_link_item_model.Description = this.Description;
        }
    }
}
