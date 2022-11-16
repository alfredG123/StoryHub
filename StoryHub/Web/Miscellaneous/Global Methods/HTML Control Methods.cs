using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Enumerations;

namespace Web.Miscellaneous
{
    public static class HTMLControlMethods
    {
        /// <summary>
        /// Create a selection list based on the drama type list
        /// </summary>
        /// <param name="selected_drama_type"></param>
        /// <returns></returns>
        public static List<SelectListItem> CreateDramaTypeList(DramaType selected_drama_type)
        {
            // Create an empty list to store the items to select
            List<SelectListItem> selection_list = new();

            // For each drama type, create a list item
            foreach (DramaType drama_type in DramaType.GetAllDramaTypes())
            {
                if (drama_type != DramaType.None)
                {
                    selection_list.Add(new SelectListItem(text: drama_type.Text, value: drama_type.Value.ToString(), selected: drama_type == selected_drama_type));
                }
            }

            return (selection_list);
        }

        /// <summary>
        /// Create a selection list based on the plot type list
        /// </summary>
        /// <param name="selected_plot_type"></param>
        /// <returns></returns>
        public static List<SelectListItem> CreatePlotTypeList(PlotType selected_plot_type)
        {
            // Create an empty list to store the items to select
            List<SelectListItem> selection_list = new();

            // For each plot type for sorting the artifact combinations, create a list item excepting none
            foreach (PlotType plot_type in PlotType.GetAllPlotTypes())
            {
                if (plot_type != PlotType.None)
                {
                    selection_list.Add(new SelectListItem(text: plot_type.Text, value: plot_type.Value.ToString(), selected: plot_type == selected_plot_type));
                }
            }

            return (selection_list);
        }
    }
}
