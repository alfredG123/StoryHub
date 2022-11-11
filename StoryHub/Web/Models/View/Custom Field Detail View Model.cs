using Web.Data;
using Web.Data.CustomFields;
using Web.Data.ID;

namespace Web.Models
{
    public class CustomFieldDetailViewModel
    {
        /// <summary>
        /// Return or set the ID of the custom field
        /// </summary>
        public int CustomFieldID { get; set; } = 0;

        /// <summary>
        /// Return or set the name of the custom field
        /// </summary>
        public string FieldName { get; set; } = string.Empty;

        /// <summary>
        /// Return or set the content of the custom field
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Convert the custom field data to the view model
        /// </summary>
        /// <param name="custom_field_data"></param>
        /// <returns></returns>
        public static CustomFieldDetailViewModel ConvertToCustomFieldDetailViewModel(CustomFieldData custom_field_data)
        {
            CustomFieldDetailViewModel custom_field_detail_view_model = new();

            // Set up the properties with the custom field data
            custom_field_detail_view_model.CustomFieldID = custom_field_data.CustomFieldID.Value;
            custom_field_detail_view_model.FieldName = custom_field_data.FieldName;
            custom_field_detail_view_model.Content = custom_field_data.Content;

            return (custom_field_detail_view_model);
        }

        /// <summary>
        /// Convert the view model to the custom field data
        /// </summary>
        /// <param name="custom_field_detail_view_model"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        public static CustomFieldData ConvertToCustomFieldData(CustomFieldDetailViewModel custom_field_detail_view_model, ProgramDbContext db_context)
        {
            CustomFieldData custom_field_data = new(new CustomFieldID(custom_field_detail_view_model.CustomFieldID), db_context);

            // Update the variables based on the data from the web page
            custom_field_data.FieldName = custom_field_detail_view_model.FieldName;
            custom_field_data.Content = custom_field_detail_view_model.Content;

            return (custom_field_data);
        }
    }
}
