using Web.Data;
using Web.Data.Timeline;
using Web.Data.ID;

namespace Web.Models
{
    public class TimelineDetailViewModel
    {
        /// <summary>
        /// Return or set the ID of the timeline
        /// </summary>
        public int TimelineID { get; set; } = 0;

        /// <summary>
        /// Return or set the time of the timeline
        /// </summary>
        public string Time { get; set; } = string.Empty;

        /// <summary>
        /// Convert the timeline data to the view model
        /// </summary>
        /// <param name="timeline_data"></param>
        /// <returns></returns>
        public static TimelineDetailViewModel ConvertToTimelineDetailViewModel(TimelineData timeline_data)
        {
            TimelineDetailViewModel timeline_detail_view_model = new();

            // Set up the properties with the timeline data
            timeline_detail_view_model.TimelineID = timeline_data.TimelineID.Value;
            timeline_detail_view_model.Time = timeline_data.Time;

            return (timeline_detail_view_model);
        }

        /// <summary>
        /// Convert the view model to the time data
        /// </summary>
        /// <param name="timeline_detail_view_model"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        public static TimelineData ConvertToTimelineData(TimelineDetailViewModel timeline_detail_view_model, ProgramDbContext db_context)
        {
            TimelineData timeline_data = new(new TimelineID(timeline_detail_view_model.TimelineID), db_context);

            // Update the variables based on the data from the web page
            timeline_data.Time = timeline_detail_view_model.Time;

            return (timeline_data);
        }
    }
}
