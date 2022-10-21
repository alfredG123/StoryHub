using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class RegionID : BaseID
    {
        /// <summary>
        /// Create an ID for the region data
        /// </summary>
        public RegionID()
               : base(DataIDType.RegionData)
        {
        }

        /// <summary>
        /// Create an ID for the region data
        /// </summary>
        /// <param name="value"></param>
        public RegionID(int value)
            : base(value, DataIDType.RegionData)
        {
        }
    }
}
