using Web.Data.ID;
using Web.Models;

namespace Web.Data.Stories
{
    [Serializable()]
    public class StoryDataList
    : BaseDataList<StoryID, StoryData>
    {
        /// <summary>
        /// Retrieve all story data from the database
        /// </summary>
        /// <param name="db_context"></param>
        public StoryDataList(ProgramDbContext db_context)
            : base(db_context)
        {
        }

        /// <summary>
        /// Return the story data by the specified ID
        /// </summary>
        /// <param name="story_id"></param>
        /// <returns></returns>
        public override StoryData? GetListItem(StoryID story_id)
        {
            return (this.Where(list_item => list_item.StoryID == story_id).SingleOrDefault());
        }

        /// <summary>
        /// Retrieve all story data from database
        /// </summary>
        /// <param name="db_context"></param>
        protected override void RetrieveData(ProgramDbContext db_context)
        {
            // NOTE: Close the connection first by using ToList() instead iterating db_context.StoryData
            List<StoryModel> story_data_model_list = db_context.StoryData.ToList();

            foreach (StoryModel story_data_model in story_data_model_list)
            {
                this.Add(new StoryData(story_data_model));
            }
        }
    }
}
