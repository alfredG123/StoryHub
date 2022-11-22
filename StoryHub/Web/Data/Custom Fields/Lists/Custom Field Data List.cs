using Web.Data.ID;
using Web.Data.Stories;
using Web.Models;

namespace Web.Data.CustomFields
{
    [Serializable()]
    public class CustomFieldDataList
    : BaseDataList<CustomFieldID, CustomFieldData>
    {
        private readonly StoryID _story_id = new();

        /// <summary>
        /// Retrieve all custom field data from the database for the story
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="db_context"></param>
        public CustomFieldDataList(StoryID story_id, ProgramDbContext db_context)
            : base()
        {
            _story_id = story_id;

            RetrieveData(db_context);
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
            List<CustomFieldDataModel> custom_field_data_model_list = db_context.CustomFieldData.ToList();

            if (_story_id.IsSet)
            {
                StoryCustomFieldLinks story_custom_field_links = new(_story_id, db_context);

                foreach (CustomFieldDataModel custom_field_data_model in custom_field_data_model_list)
                {
                    if (story_custom_field_links.IndexOfID(new CustomFieldID(custom_field_data_model.ID)) != -1)
                    {
                        this.Add(new CustomFieldData(custom_field_data_model));
                    }
                }
            }
        }
    }
}
