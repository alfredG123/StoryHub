namespace Web.Enumerations
{
    [Serializable()]
    public class DataIDType
           : BaseEnumeration
    {
        public readonly static DataIDType None = new(0, "None");

        public readonly static DataIDType StoryData = new(1, "Story Data");
        public readonly static DataIDType CharacterData = new(2, "Character Data");
        public readonly static DataIDType RegionData = new(3, "Region Data");
        public readonly static DataIDType ReferenceData = new(4, "Reference Data");
        public readonly static DataIDType PlotData = new(5, "Plot Data");
        public readonly static DataIDType CustomFieldData = new(6, "Custom Field Data");
        public readonly static DataIDType CharacterReferenceLinkItem = new(7, "Character Reference Link Item");

        /// <summary>
        /// Create a new data ID type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="text"></param>
        public DataIDType(int value, string text)
            : base(value, text)
        {
        }

        /// <summary>
        /// Return a list of available data ID type
        /// </summary>
        /// <returns></returns>
        public static List<DataIDType> GetAllDataIDTypes()
        {
            return (BaseEnumeration.GetAll<DataIDType>());
        }

        /// <summary>
        /// Return the data ID type by the specified value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataIDType GetDataIDType(int value)
        {
            DataIDType? data_id_type = GetItem<DataIDType>(value);
            if (data_id_type == null)
            {
                data_id_type = DataIDType.None;
            }

            return (data_id_type);
        }
    }
}
