using Web.Data.ID;
using Web.Models;

namespace Web.Data.Timeline
{
    [Serializable()]
    public class TimelineDataLine
    : BaseDataList<TimelineID, TimelineData>
    {
        /// <summary>
        /// Retrieve all timeline data from the database
        /// </summary>
        /// <param name="db_context"></param>
        public TimelineDataLine(ProgramDbContext db_context)
            : base()
        {
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

            foreach (TimelineDataModel timeline_data_model in timeline_data_model_list)
            {
                this.Add(new TimelineData(timeline_data_model));
            }
        }
    }
}
