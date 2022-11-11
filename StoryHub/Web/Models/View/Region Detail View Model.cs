using Web.Data;
using Web.Data.Regions;
using Web.Data.ID;

namespace Web.Models
{
    public class RegionDetailViewModel
    {
        /// <summary>
        /// Return or set the ID of the region
        /// </summary>
        public int RegionID { get; set; } = 0;

        /// <summary>
        /// Return or set the name of the region
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Return or set the description of the region
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Convert the region data to the view model
        /// </summary>
        /// <param name="region_data"></param>
        /// <returns></returns>
        public static RegionDetailViewModel ConvertToRegionDetailViewModel(RegionData region_data)
        {
            RegionDetailViewModel region_detail_view_model = new();

            // Set up the properties with the region data
            region_detail_view_model.RegionID = region_data.RegionID.Value;
            region_detail_view_model.Name = region_data.Name;
            region_detail_view_model.Description = region_data.Description;

            return (region_detail_view_model);
        }

        /// <summary>
        /// Convert the view model to the region data
        /// </summary>
        /// <param name="region_detail_view_model"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        public static RegionData ConvertToRegionData(RegionDetailViewModel region_detail_view_model, ProgramDbContext db_context)
        {
            RegionData region_data = new(new RegionID(region_detail_view_model.RegionID), db_context);

            // Update the variables based on the data from the web page
            region_data.Name = region_detail_view_model.Name;
            region_data.Description = region_detail_view_model.Description;

            return (region_data);
        }
    }
}
