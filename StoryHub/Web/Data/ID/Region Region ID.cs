using Web.Enumerations;

namespace Web.Data.ID
{
    [Serializable()]
    public class RegionRegionID : BaseID
    {
        /// <summary>
        /// Create an ID for the region region link item
        /// </summary>
        public RegionRegionID()
               : base(DataIDType.RegionRegionLinkItem)
        {
        }

        /// <summary>
        /// Create an ID for the region region link item
        /// </summary>
        /// <param name="value"></param>
        public RegionRegionID(int value)
            : base(value, DataIDType.RegionRegionLinkItem)
        {
        }
    }
}
