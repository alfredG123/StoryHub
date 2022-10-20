using Web.Data.ID;
using Web.Models;

namespace Web.Data.CustomFields
{
    [Serializable()]
    public class CustomFieldDataList
    : BaseDataList<CustomFieldID, CustomFieldData>
    {
        /// <summary>
        /// Retrieve all custom field data from the database
        /// </summary>
        /// <param name="db_context"></param>
        public CustomFieldDataList(ProgramDbContext db_context)
            : base(db_context)
        {
        }

        /// <summary>
        /// Return the custom field data by the specified ID
        /// </summary>
        /// <param name="custom_field_id"></param>
        /// <returns></returns>
        public override CustomFieldData? GetListItem(CustomFieldID custom_field_id)
        {
            return (this.Where(list_item => list_item.CustomFieldID == custom_field_id).SingleOrDefault());
        }

        /// <summary>
        /// Retrieve all custom field data from database
        /// </summary>
        /// <param name="db_context"></param>
        protected override void RetrieveData(ProgramDbContext db_context)
        {
            // NOTE: Close the connection first by using ToList() instead iterating db_context.CustomFieldData
            List<CustomFieldModel> custom_field_data_model_list = db_context.CustomFieldData.ToList();

            foreach (CustomFieldModel custom_field_data_model in custom_field_data_model_list)
            {
                this.Add(new CustomFieldData(custom_field_data_model));
            }
        }
    }
}
