using Web.Enumerations;

namespace Web.Data.ID
{
    public class CustomFieldID : BaseID
    {
        /// <summary>
        /// Create an ID for the custom field data
        /// </summary>
        public CustomFieldID()
               : base(DataIDType.CustomFieldData)
        {
        }

        /// <summary>
        /// Create an ID for the custom field data
        /// </summary>
        /// <param name="value"></param>
        public CustomFieldID(int value)
            : base(value, DataIDType.CustomFieldData)
        {
        }
    }
}
