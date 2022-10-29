﻿using Web.Data.ID;
using Web.Models;

namespace Web.Data.Plots
{
    [Serializable()]
    public class RegionReferenceLinks
        : BaseLinks<RegionID, ReferenceID>
    {
        /// <summary>
        /// Retrieve all region reference link items from database
        /// </summary>
        /// <param name="link_type"></param>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        public RegionReferenceLinks(LinkType link_type, BaseID id, ProgramDbContext db_context)
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
            // NOTE: Close the connection first by using ToList() instead iterating db_context.RegionReferenceLinkItemModel
            List<RegionReferenceLinkItemModel> region_reference_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                region_reference_link_item_model_list = db_context.RegionReferenceLinkItem.Where(list_item => list_item.RegionID == id.Value).ToList();
            }
            else
            {
                region_reference_link_item_model_list = db_context.RegionReferenceLinkItem.Where(list_item => list_item.ReferenceID == id.Value).ToList();
            }

            foreach (RegionReferenceLinkItemModel region_reference_link_item_model in region_reference_link_item_model_list)
            {
                this.Add(new RegionReferenceLinkItem(region_reference_link_item_model));
            }
        }
    }

    [Serializable()]
    public class RegionReferenceLinkItem
         : BaseLinkItem<RegionID, ReferenceID>
    {
        /// <summary>
        /// Load the region reference link item model
        /// </summary>
        /// <param name="region_reference_link_item_model"></param>
        public RegionReferenceLinkItem(RegionReferenceLinkItemModel region_reference_link_item_model)
            : base(new RegionReferenceID(region_reference_link_item_model.ID), region_reference_link_item_model)
        {
        }

        /// <summary>
        /// Return or set the region ID
        /// </summary>
        public RegionID RegionID { get; set; } = new();

        /// <summary>
        /// Return or set the reference ID
        /// </summary>
        public ReferenceID ReferenceID { get; set; } = new();

        /// <summary>
        /// Return or set the description
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Update the data object for retrieving the data from the database
        /// </summary>
        protected override void UpdateDataObject()
        {
            RegionReferenceLinkItemModel region_reference_link_item_model = (RegionReferenceLinkItemModel)this.BaseModel;
            RegionReferenceID region_reference_id = new(region_reference_link_item_model.ID);

            this.ID = region_reference_id;
            this.RegionID = new RegionID(region_reference_link_item_model.RegionID);
            this.ReferenceID = new ReferenceID(region_reference_link_item_model.ReferenceID);
            this.Description = region_reference_link_item_model.Description;
            this.IsSet = region_reference_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            RegionReferenceLinkItemModel region_reference_link_item_model = (RegionReferenceLinkItemModel)this.BaseModel;

            region_reference_link_item_model.ID = this.ID.Value;
            region_reference_link_item_model.RegionID = this.RegionID.Value;
            region_reference_link_item_model.ReferenceID = this.ReferenceID.Value;
            region_reference_link_item_model.Description = this.Description;
        }
    }
}
