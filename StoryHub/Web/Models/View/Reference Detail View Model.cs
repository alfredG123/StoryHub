using Web.Data;
using Web.Data.References;
using Web.Data.ID;

namespace Web.Models
{
    public class ReferenceDetailViewModel
    {
        /// <summary>
        /// Return or set the ID of the reference
        /// </summary>
        public int ReferenceID { get; set; } = 0;

        /// <summary>
        /// Return or set the title of the reference
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Return or set the description of the reference
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Return or set the related URL of the reference
        /// </summary>
        public string RelatedURL { get; set; } = string.Empty;

        /// <summary>
        /// Convert the reference data to the view model
        /// </summary>
        /// <param name="reference_data"></param>
        /// <returns></returns>
        public static ReferenceDetailViewModel ConvertToReferencenDetailViewModel(ReferenceData reference_data)
        {
            ReferenceDetailViewModel reference_detail_view_model = new();

            // Set up the properties with the reference data
            reference_detail_view_model.ReferenceID = reference_data.ReferenceID.Value;
            reference_detail_view_model.Title = reference_data.Title;
            reference_detail_view_model.Description = reference_data.Description;
            reference_detail_view_model.RelatedURL = reference_data.RelatedURL;

            return (reference_detail_view_model);
        }

        /// <summary>
        /// Convert the view model to the reference data
        /// </summary>
        /// <param name="reference_detail_view_model"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        public static ReferenceData ConvertToReferenceData(ReferenceDetailViewModel reference_detail_view_model, ProgramDbContext db_context)
        {
            ReferenceData reference_data = new(new ReferenceID(reference_detail_view_model.ReferenceID), db_context);

            // Update the variables based on the data from the web page
            reference_data.Title = reference_detail_view_model.Title;
            reference_data.Description = reference_detail_view_model.Description;
            reference_data.RelatedURL = reference_detail_view_model.RelatedURL;

            return (reference_data);
        }
    }
}
