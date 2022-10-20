using Web.Data.ID;
using Web.Models;

namespace Web.Data.Plots
{
    [Serializable()]
    public class PlotDataList
    : BaseDataList<PlotID, PlotData>
    {
        /// <summary>
        /// Retrieve all plot data from the database
        /// </summary>
        /// <param name="db_context"></param>
        public PlotDataList(ProgramDbContext db_context)
            : base(db_context)
        {
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
            List<PlotModel> plot_data_model_list = db_context.PlotData.ToList();

            foreach (PlotModel plot_data_model in plot_data_model_list)
            {
                this.Add(new PlotData(plot_data_model));
            }
        }
    }
}
