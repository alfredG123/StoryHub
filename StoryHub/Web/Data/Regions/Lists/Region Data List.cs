using Web.Data.ID;
using Web.Data.Stories;
using Web.Models;

namespace Web.Data.Regions
{
    [Serializable()]
    public class RegionDataList
    : BaseDataList<RegionID, RegionData>
    {
        private readonly StoryID _story_id = new();

        /// <summary>
        /// Retrieve all region data from the database for the story
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="db_context"></param>
        public RegionDataList(StoryID story_id, ProgramDbContext db_context)
            : base()
        {
            _story_id = story_id;

            RetrieveData(db_context);
        }

        /// <summary>
        /// Return the region data by the specified ID
        /// </summary>
        /// <param name="region_id"></param>
        /// <returns></returns>
        public override RegionData? GetListItem(RegionID region_id)
        {
            return (this.Where(list_item => list_item.RegionID == region_id).SingleOrDefault());
        }

        /// <summary>
        /// Retrieve all region data from database
        /// </summary>
        /// <param name="db_context"></param>
        protected override void RetrieveData(ProgramDbContext db_context)
        {
            // NOTE: Close the connection first by using ToList() instead iterating db_context.RegionData
            List<RegionDataModel> region_data_model_list = db_context.RegionData.ToList();

            if (_story_id.IsSet)
            {
                StoryRegionLinks story_region_links = new(_story_id, db_context);

                foreach (RegionDataModel region_data_model in region_data_model_list)
                {
                    if (story_region_links.IndexOfID(new RegionID(region_data_model.ID)) != -1)
                    {
                        this.Add(new RegionData(region_data_model));
                    }
                }
            }
        }
    }
}
