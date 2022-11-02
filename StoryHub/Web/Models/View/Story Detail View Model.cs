using Web.Data;
using Web.Data.Stories;
using Web.Data.ID;

namespace Web.Models
{
    public class StoryDetailViewModel
    {
        /// <summary>
        /// Return or set the ID of the story
        /// </summary>
        public int StoryID { get; set; } = 0;

        /// <summary>
        /// Return or set the title of the story
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Convert the story data to the view model
        /// </summary>
        /// <param name="story_data"></param>
        /// <returns></returns>
        public static StoryDetailViewModel ConvertToStoryDetailViewModel(StoryData story_data)
        {
            StoryDetailViewModel story_detail_view_model = new();

            // Set up the properties with the story data
            story_detail_view_model.StoryID = story_data.StoryID.Value;
            story_detail_view_model.Title = story_data.Title;

            return (story_detail_view_model);
        }

        /// <summary>
        /// Convert the view model to the story data
        /// </summary>
        /// <param name="story_detail_view_model"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        public static StoryData ConvertToStoryData(StoryDetailViewModel story_detail_view_model, ProgramDbContext db_context)
        {
            StoryData story_data = new(new StoryID(story_detail_view_model.StoryID), db_context);

            // Update the variables based on the data from the web page
            story_data.Title = story_detail_view_model.Title;

            return (story_data);
        }
    }
}
