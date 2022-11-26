using Web.Data.ID;
using Web.Data.Stories;
using Web.Models;

namespace Web.Data.Timeline
{
    [Serializable()]
    public class TimelineDataList
    : BaseDataList<TimelineID, TimelineData>
    {
        private readonly StoryID _story_id = new();

        /// <summary>
        /// Retrieve all timeline data from the database for the story
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="db_context"></param>
        public TimelineDataList(StoryID story_id, ProgramDbContext db_context)
            : base()
        {
            _story_id = story_id;

            RetrieveData(db_context);
        }

        /// <summary>
        /// Return the timeline data by the specified ID
        /// </summary>
        /// <param name="timeline_id"></param>
        /// <returns></returns>
        public override TimelineData? GetListItem(TimelineID timeline_id)
        {
            return (this.Where(list_item => list_item.TimelineID == timeline_id).SingleOrDefault());
        }

        /// <summary>
        /// Retrieve all timeline data from database
        /// </summary>
        /// <param name="db_context"></param>
        protected override void RetrieveData(ProgramDbContext db_context)
        {
            // NOTE: Close the connection first by using ToList() instead iterating db_context.TimelineData
            List<TimelineDataModel> timeline_data_model_list = db_context.TimelineData.ToList();

            if (_story_id.IsSet)
            {
                StoryTimelineLinks story_timeline_links = new(_story_id, db_context);

                foreach (TimelineDataModel timeline_data_model in timeline_data_model_list)
                {
                    if (story_timeline_links.IndexOfID(new TimelineID(timeline_data_model.ID)) != -1)
                    {
                        this.Add(new TimelineData(timeline_data_model));
                    }
                }
            }
        }
    }
}
