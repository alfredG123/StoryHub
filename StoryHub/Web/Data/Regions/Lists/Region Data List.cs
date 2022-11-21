using Web.Data.ID;
using Web.Models;

namespace Web.Data.Regions
{
    [Serializable()]
    public class RegionDataList
    : BaseDataList<RegionID, RegionData>
    {
        /// <summary>
        /// Retrieve all region data from the database
        /// </summary>
        /// <param name="db_context"></param>
        public RegionDataList(ProgramDbContext db_context)
            : base()
        {
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

            foreach (RegionDataModel region_data_model in region_data_model_list)
            {
                this.Add(new RegionData(region_data_model));
            }
        }
    }
}
