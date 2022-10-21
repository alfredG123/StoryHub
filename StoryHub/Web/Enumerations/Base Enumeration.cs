using System.Reflection;

namespace Web.Enumerations
{
    [Serializable()]
    public class BaseEnumeration
        : IComparable
    {
        #region Constructor
        /// <summary>
        /// Create a new enumeration object
        /// </summary>
        /// <param name="value"></param>
        /// <param name="text"></param>
        protected BaseEnumeration(int value, string text)
        {
            this.Value = value;
            this.Text = text;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Return the text of the enumeration object
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Return the value of the enumeration object
        /// </summary>
        public int Value { get; private set; }
        #endregion

        /// <summary>
        /// Check if the specified object is same as the current object
        /// </summary>
        /// <param name="other_object"></param>
        /// <returns></returns>
        public int CompareTo(object? other_object)
        {
            int result_value = 0;

            // If the specified object is BaseEnumeration, return compare the base enumeration
            if (other_object is BaseEnumeration base_enumeration)
            {
                result_value = this.Value.CompareTo(base_enumeration.Value);
            }

            return (result_value);
        }

        /// <summary>
        /// Check if the specified object is same as the current object
        /// </summary>
        /// <param name="other_object"></param>
        /// <returns>True, if they are the same. Otherwise, false</returns>
        public override bool Equals(object? other_object)
        {
            bool is_equal = false;

            // If the specified object is not set, return false
            if (other_object is null)
            {
                return (false);
            }

            // If the specified object is not BaseEnumeration, return false
            if (other_object is not BaseEnumeration other_enumeration)
            {
                return (false);
            }

            // Compare the components
            bool type_matches = this.GetType().Equals(other_enumeration.GetType());
            bool value_matches = this.Value.Equals(other_enumeration.Value);

            if (type_matches && value_matches)
            {
                is_equal = true;
            }

            return (is_equal);
        }

        /// <summary>
        /// Return the value for hashing
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return (base.GetHashCode() ^ this.Value.GetHashCode() ^ this.Text.GetHashCode());
        }

        /// <summary>
        /// Return the text of the enumeration object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return (this.Text);
        }

        #region Operators
        /// <summary>
        /// Check if the specified object is different from the current object
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator !=(BaseEnumeration? lhs, BaseEnumeration? rhs)
        {
            return (!(lhs == rhs));
        }

        /// <summary>
        /// Check if the specified object is same as the current object
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator ==(BaseEnumeration? lhs, BaseEnumeration? rhs)
        {
            // If the both objects are same, return true
            if (lhs is null && rhs is null)
            {
                return (true);
            }

            // If one of the object is not set, return false
            else if (lhs is null)
            {
                return (false);
            }
            else if (rhs is null)
            {
                return (false);
            }

            // Complete two objects
            return (lhs.Equals(rhs));
        }
        #endregion

        /// <summary>
        /// Return a list of available enumeration objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected static List<T> GetAll<T>() where T : BaseEnumeration
        {
            return (typeof(T).GetFields(BindingFlags.Public |
                                BindingFlags.Static |
                                BindingFlags.DeclaredOnly)
                     .Select(f => f.GetValue(null))
                     .Cast<T>().ToList<T>());
        }

        /// <summary>
        /// Return the enumeration object by the specified value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static T? GetItem<T>(int value) where T : BaseEnumeration
        {
            return (GetAll<T>().SingleOrDefault<T>(list_item => list_item.Value == value));
        }
    }
}
