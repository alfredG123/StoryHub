using Web.Data;
using Web.Data.ID;
using Web.Data.Plots;

namespace Web.Models
{
    public class PlotDetailViewModel
    {
        /// <summary>
        /// Return or set the ID of the plot
        /// </summary>
        public int PlotID { get; set; } = 0;

        /// <summary>
        /// Return or set the plot type of the plot
        /// </summary>
        public int PlotType { get; set; } = 0;

        /// <summary>
        /// Return or set the drama type of the plot
        /// </summary>
        public int DramaType { get; set; } = 0;

        /// <summary>
        /// Return or set the title of the plot
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Return or set the goal of the plot
        /// </summary>
        public string Goal { get; set; } = string.Empty;

        /// <summary>
        /// Return or set the scene of the plot
        /// </summary>
        public string Scene { get; set; } = string.Empty;

        /// <summary>
        /// Return or set the content of the plot
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// Convert the plot data to the view model
        /// </summary>
        /// <param name="plot_data"></param>
        /// <returns></returns>
        public static PlotDetailViewModel ConvertToPlotDetailViewModel(PlotData plot_data)
        {
            PlotDetailViewModel plot_detail_view_model = new();

            // Set up the properties with the plot data
            plot_detail_view_model.PlotID = plot_data.PlotID.Value;
            plot_detail_view_model.PlotType = plot_data.PlotType.Value;
            plot_detail_view_model.DramaType = plot_data.DramaType.Value;
            plot_detail_view_model.Title = plot_data.Title;
            plot_detail_view_model.Goal = plot_data.Goal;
            plot_detail_view_model.Scene = plot_data.Scene;
            plot_detail_view_model.Content = plot_data.Content;

            return (plot_detail_view_model);
        }

        /// <summary>
        /// Convert the view model to the plot data
        /// </summary>
        /// <param name="plot_detail_view_model"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        public static PlotData ConvertToPlotData(PlotDetailViewModel plot_detail_view_model, ProgramDbContext db_context)
        {
            PlotData plot_data = new(new PlotID(plot_detail_view_model.PlotID), db_context);

            // Update the variables based on the data from the web page
            plot_data.PlotType = Enumerations.PlotType.GetPlotType(plot_detail_view_model.PlotType);
            plot_data.DramaType = Enumerations.DramaType.GetDramaType(plot_detail_view_model.DramaType);
            plot_data.Title = plot_detail_view_model.Title;
            plot_data.Goal = plot_detail_view_model.Goal;
            plot_data.Scene = plot_detail_view_model.Scene;
            plot_data.Content = plot_detail_view_model.Content;

            return (plot_data);
        }
    }
}
