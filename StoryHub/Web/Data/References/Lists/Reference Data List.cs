using Web.Data.ID;
using Web.Data.Stories;
using Web.Models;

namespace Web.Data.References
{
    [Serializable()]
    public class ReferenceDataList
    : BaseDataList<ReferenceID, ReferenceData>
    {
        private readonly StoryID _story_id = new();

        /// <summary>
        /// Retrieve all reference data from the database for the story
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="db_context"></param>
        public ReferenceDataList(StoryID story_id, ProgramDbContext db_context)
            : base()
        {
            _story_id = story_id;

            RetrieveData(db_context);
        }

        /// <summary>
        /// Return the reference data by the specified ID
        /// </summary>
        /// <param name="reference_id"></param>
        /// <returns></returns>
        public override ReferenceData? GetListItem(ReferenceID reference_id)
        {
            return (this.Where(list_item => list_item.ReferenceID == reference_id).SingleOrDefault());
        }

        /// <summary>
        /// Retrieve all reference data from database
        /// </summary>
        /// <param name="db_context"></param>
        protected override void RetrieveData(ProgramDbContext db_context)
        {
            // NOTE: Close the connection first by using ToList() instead iterating db_context.ReferenceData
            List<ReferenceDataModel> reference_data_model_list = db_context.ReferenceData.ToList();

            if (_story_id.IsSet)
            {
                StoryReferenceLinks story_reference_links = new(_story_id, db_context);

                foreach (ReferenceDataModel reference_data_model in reference_data_model_list)
                {
                    if (story_reference_links.IndexOfID(new ReferenceID(reference_data_model.ID)) != -1)
                    {
                        this.Add(new ReferenceData(reference_data_model));
                    }
                }
            }
        }
    }
}
