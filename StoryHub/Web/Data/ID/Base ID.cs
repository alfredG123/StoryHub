using Web.Enumerations;

namespace Web.Data.ID
{
    public abstract class BaseID
    {
        private static readonly int _no_id = 0;

        /// <summary>
        /// Create a new ID
        /// </summary>
        /// <param name="data_id_type"></param>
        public BaseID(DataIDType data_id_type)
            : this(_no_id, data_id_type)
        {
        }

        /// <summary>
        /// Create a new ID
        /// </summary>
        /// <param name="value"></param>
        /// <param name="data_id_type"></param>
        public BaseID(int value, DataIDType data_id_type)
        {
            // Set up the data ID type as the specified type
            this.DataIDType = data_id_type;

            // If the value is passed, set up the value as the specified value
            this.Value = value;

            // If the value is not set, set up the flag to indicate the ID is not valid
            if (this.Value <= _no_id)
            {
                this.IsSet = false;
            }

            // If the value is set, set up the flag to indicate the ID is valid
            else
            {
                this.IsSet = true;
            }
        }

        #region Properties
        /// <summary>
        /// Return the data type
        /// </summary>
        public DataIDType DataIDType { get; private set; }

        /// <summary>
        /// Return the flag if the ID is set
        /// </summary>
        public bool IsSet { get; }

        /// <summary>
        /// Return the value of the ID
        /// </summary>
        public int Value { get; }
        #endregion

        /// <summary>
        /// Check if the specified object is same as the current object
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(object? other)
        {
            // Default the value as the 0 to indicate the passed object is same as the current object
            int result_value = 0;

            // If the passed object is BaseID, check its components
            if (other is BaseID base_id)
            {
                // Check the ID types
                if (result_value == 0)
                {
                    result_value = DataIDType.CompareTo(base_id.DataIDType);
                }

                // Check the ID values
                if (result_value == 0)
                {
                    result_value = Value.CompareTo(base_id.Value);
                }
            }

            return result_value;
        }

        /// <summary>
        /// Check if the specified object is same as the current object
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? other)
        {
            // Default the flag as false to indicate the passed object is not the same as the current object
            bool is_equal = false;

            // If the passed object is not set, return the flag to indicate it is not the same as the current object
            if (other is null)
            {
                return is_equal;
            }

            // If the passed object is not BaseID, return the flag to indicate is is not the same as the current object
            if (other is not BaseID base_id)
            {
                return is_equal;
            }

            // Check the types
            bool type_matches = this.GetType().Equals(base_id.GetType());

            // Check the ID types
            bool data_ID_type_matches = this.DataIDType.Equals(base_id.DataIDType);

            // Check the ID values
            bool value_matches = this.Value.Equals(base_id.Value);

            // If all component are the same, update the flag to indicate the passed object is the same as the current object
            if (type_matches && data_ID_type_matches && value_matches)
            {
                is_equal = true;
            }

            return is_equal;
        }

        /// <summary>
        /// Return the value for hashing
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (base.GetHashCode() ^ this.Value ^ this.DataIDType.Value);
        }

        /// <summary>
        /// Check if the specified object is different from the current object
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator !=(BaseID? lhs, BaseID? rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Check if the specified object is same as the current object
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator ==(BaseID? lhs, BaseID? rhs)
        {
            // If both BaseIDs are not set, return equal
            if (lhs is null && rhs is null)
            {
                return true;
            }

            // If one of the objects is not set, return not equal
            else if (lhs is null)
            {
                return false;
            }
            else if (rhs is null)
            {
                return false;
            }

            // If both BaseIDs are set, compare those two objects
            return lhs.Equals(rhs);
        }
    }
}
