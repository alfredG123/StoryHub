using Web.Data.ID;
using Web.Data.Stories;
using Web.Models;

namespace Web.Data.Plots
{
    [Serializable()]
    public class PlotDataList
    : BaseDataList<PlotID, PlotData>
    {
        private readonly StoryID _story_id = new();

        /// <summary>
        /// Retrieve all plot data from the database for the story
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="db_context"></param>
        public PlotDataList(StoryID story_id, ProgramDbContext db_context)
            : base()
        {
            _story_id = story_id;

            RetrieveData(db_context);
        }

        /// <summary>
        /// Return the plot data by the specified ID
        /// </summary>
        /// <param name="plot_id"></param>
        /// <returns></returns>
        public override PlotData? GetListItem(PlotID plot_id)
        {
            return (this.Where(list_item => list_item.PlotID == plot_id).SingleOrDefault());
        }

        /// <summary>
        /// Retrieve all plot data from database
        /// </summary>
        /// <param name="db_context"></param>
        protected override void RetrieveData(ProgramDbContext db_context)
        {
            // NOTE: Close the connection first by using ToList() instead iterating db_context.PlotData
            List<PlotDataModel> plot_data_model_list = db_context.PlotData.ToList();

            if (_story_id.IsSet)
            {
                StoryPlotLinks story_plot_links = new(_story_id, db_context);

                foreach (PlotDataModel plot_data_model in plot_data_model_list)
                {
                    if (story_plot_links.IndexOfID(new PlotID(plot_data_model.ID)) != -1)
                    {
                        this.Add(new PlotData(plot_data_model));
                    }
                }
            }
        }
    }
}
