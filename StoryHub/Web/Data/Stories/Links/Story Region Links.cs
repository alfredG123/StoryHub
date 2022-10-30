﻿using Web.Data.ID;
using Web.Models;

namespace Web.Data.Stories
{
    [Serializable()]
    public class StoryRegionLinks
        : BaseLinks<StoryID, RegionID>
    {
        /// <summary>
        /// Retrieve all story region link items from database
        /// </summary>
        /// <param name="link_type"></param>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        public StoryRegionLinks(LinkType link_type, BaseID id, ProgramDbContext db_context)
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
            // NOTE: Close the connection first by using ToList() instead iterating db_context.StoryRegionLinkItemModel
            List<StoryRegionLinkItemModel> story_region_link_item_model_list;

            if (link_type == LinkType.ByLeft)
            {
                story_region_link_item_model_list = db_context.StoryRegionLinkItem.Where(list_item => list_item.StoryID == id.Value).ToList();
            }
            else
            {
                story_region_link_item_model_list = db_context.StoryRegionLinkItem.Where(list_item => list_item.RegionID == id.Value).ToList();
            }

            foreach (StoryRegionLinkItemModel story_region_link_item_model in story_region_link_item_model_list)
            {
                this.Add(new StoryRegionLinkItem(story_region_link_item_model));
            }
        }
    }

    [Serializable()]
    public class StoryRegionLinkItem
         : BaseLinkItem<StoryID, RegionID>
    {
        /// <summary>
        /// Load the story region link item model
        /// </summary>
        /// <param name="story_region_link_item_model"></param>
        public StoryRegionLinkItem(StoryRegionLinkItemModel story_region_link_item_model)
            : base(new StoryRegionID(story_region_link_item_model.ID), story_region_link_item_model)
        {
        }

        /// <summary>
        /// Return or set the story ID
        /// </summary>
        public StoryID StoryID { get; set; } = new();

        /// <summary>
        /// Return or set the region ID
        /// </summary>
        public RegionID RegionID { get; set; } = new();

        /// <summary>
        /// Update the data object for retrieving the data from the database
        /// </summary>
        protected override void UpdateDataObject()
        {
            StoryRegionLinkItemModel story_region_link_item_model = (StoryRegionLinkItemModel)this.BaseModel;
            StoryRegionID story_region_id = new(story_region_link_item_model.ID);

            this.ID = story_region_id;
            this.StoryID = new StoryID(story_region_link_item_model.StoryID);
            this.RegionID = new RegionID(story_region_link_item_model.RegionID);
            this.IsSet = story_region_id.IsSet;
        }

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected override void UpdateModelObject()
        {
            StoryRegionLinkItemModel story_region_link_item_model = (StoryRegionLinkItemModel)this.BaseModel;

            story_region_link_item_model.ID = this.ID.Value;
            story_region_link_item_model.StoryID = this.StoryID.Value;
            story_region_link_item_model.RegionID = this.RegionID.Value;
        }
    }
}
