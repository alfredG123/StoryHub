namespace Web.Enumerations
{
    [Serializable()]
    public class PlotType
        : BaseEnumeration
    {
        public readonly static PlotType None = new(1, "Undefine");

        public readonly static PlotType Volume = new(1, "Volume");
        public readonly static PlotType Chapter = new(2, "Chapter");

        /// <summary>
        /// Create a new plot type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="text"></param>
        public PlotType(int value, string text)
            : base(value, text)
        {
        }

        /// <summary>
        /// Return a list of available plot types
        /// </summary>
        /// <returns></returns>
        public static List<PlotType> GetAllPlotTypes()
        {
            return (BaseEnumeration.GetAll<PlotType>());
        }

        /// <summary>
        /// Return the plot type by the specified value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static PlotType GetPlotType(int value)
        {
            PlotType? plot_type = GetItem<PlotType>(value);
            if (plot_type == null)
            {
                plot_type = PlotType.None;
            }

            return (plot_type);
        }
    }
}
