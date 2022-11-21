using Web.Data.ID;
using Web.Models;

namespace Web.Data.Regions
{
    [Serializable()]
    public class RegionRegionLinks
        : BaseLinks<RegionID, RegionID>
    {
        /// <summary>
        /// Retrieve all region region link items from database
        /// </summary>
        /// <param name="link_type"></param>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        public RegionRegionLinks(LinkType link_type, BaseID id, ProgramDbContext db_context)
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
            // NOTE: Close the connection first by using ToList() instead iterating db_context.RegionRegionLinkItemModel
            List<RegionRegionLinkItemModel> region_region_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                region_region_link_item_model_list = db_context.RegionRegionLinkItem.Where(list_item => list_item.RegionID == id.Value).ToList();
            }
            else
            {
                region_region_link_item_model_list = db_context.RegionRegionLinkItem.Where(list_item => list_item.SubRegionID == id.Value).ToList();
            }

            foreach (RegionRegionLinkItemModel region_region_link_item_model in region_region_link_item_model_list)
            {
                this.Add(new RegionRegionLinkItem(region_region_link_item_model));
            }
        }

        /// <summary>
        /// Create a link item
        /// </summary>
        /// <param name="region_id"></param>
        /// <param name="sub_region_id"></param>
        /// <returns></returns>
        protected override BaseLinkItem<RegionID, RegionID> CreateLinkItem(RegionID region_id, RegionID sub_region_id)
        {
            RegionRegionLinkItemModel link_item_model = new();
            link_item_model.RegionID = region_id.Value;
            link_item_model.SubRegionID = sub_region_id.Value;

            return (new RegionRegionLinkItem(link_item_model));
        }
    }

    [Serializable()]
    public class RegionRegionLinkItem
         : BaseLinkItem<RegionID, RegionID>
    {
        /// <summary>
        /// Load the region region link item model
        /// </summary>
        /// <param name="region_region_link_item_model"></param>
        public RegionRegionLinkItem(RegionRegionLinkItemModel region_region_link_item_model)
            : base(new RegionRegionID(region_region_link_item_model.ID), new RegionID(region_region_link_item_model.RegionID), new RegionID(region_region_link_item_model.SubRegionID), region_region_link_item_model)
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
        /// Return or set the sub-region ID
        /// </summary>
        public RegionID SubRegionID
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
            RegionRegionLinkItemModel region_region_link_item_model = (RegionRegionLinkItemModel)this.BaseModel;
            RegionRegionID region_region_id = new(region_region_link_item_model.ID);

            this.ID = region_region_id;
            this.IsSet = region_region_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            RegionRegionLinkItemModel region_region_link_item_model = (RegionRegionLinkItemModel)this.BaseModel;

            region_region_link_item_model.ID = this.ID.Value;
            region_region_link_item_model.RegionID = this.RegionID.Value;
            region_region_link_item_model.SubRegionID = this.SubRegionID.Value;
        }
    }
}
