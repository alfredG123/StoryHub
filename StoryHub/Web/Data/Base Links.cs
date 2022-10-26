using Web.Data.ID;
using Web.Models;

namespace Web.Data
{
    [Serializable()]
    public abstract class BaseLinks<TLeftID, TRightID>
        : List<BaseLinkItem<TLeftID, TRightID>>
        where TLeftID : BaseID
        where TRightID : BaseID
    {
        /// <summary>
        /// Types of retrieval for the link items
        /// </summary>
        public enum LinkType
        {
            ByLeft,
            ByRight,
        }

        /// <summary>
        /// Retrieve all link items from database
        /// </summary>
        /// <param name="link_type"></param>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        protected abstract void RetrieveLinks(LinkType link_type, BaseID id, ProgramDbContext db_context);

        /// <summary>
        /// Retrieve all link items from database
        /// </summary>
        /// <param name="link_type"></param>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        public BaseLinks(LinkType link_type, BaseID id, ProgramDbContext db_context)
        {
            RetrieveLinks(link_type, id, db_context);
        }
    }

    [Serializable()]
    public abstract class BaseLinkItem<TLeftID, TRightID>
        where TLeftID : BaseID
        where TRightID : BaseID
    {
        /// <summary>
        /// Update the data object for retrieving the data from the database
        /// </summary>
        protected abstract void UpdateDataObject();

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected abstract void UpdateModelObject();

        /// <summary>
        /// Load the link item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="left_id"></param>
        /// <param name="right_id"></param>
        /// <param name="base_model"></param>
        public BaseLinkItem(BaseID id, BaseModel base_model)
        {
            this.ID = id;
            this.BaseModel = base_model;
        }

        /// <summary>
        /// Return the ID of the model
        /// </summary>
        protected BaseID ID { get; set; }

        /// <summary>
        /// Return the model
        /// </summary>
        protected BaseModel BaseModel { get; }

        /// <summary>
        /// Return the message for the error in the data
        /// </summary>
        public string ErrorMessage { get; protected set; } = string.Empty;

        /// <summary>
        /// Return the flag to indicate whether the data object is in the database
        /// </summary>
        public bool IsSet { get; protected set; }

        /// <summary>
        /// Delete the current data entry from the database
        /// </summary>
        /// <param name="db_context"></param>
        public void Delete(ProgramDbContext db_context)
        {
            // Delete the item from database
            db_context.Remove(this.BaseModel);
            db_context.SaveChanges();

            // Update the ID
            UpdateDataObject();

            this.IsSet = false;
        }

        /// <summary>
        /// Create/Update the data entry in the database
        /// </summary>
        /// <param name="db_context"></param>
        public void Save(ProgramDbContext db_context)
        {
            // Fill in the data
            UpdateModelObject();

            // If the chararcter exists in the database, update it
            if (this.IsSet)
            {
                db_context.Update(this.BaseModel);
            }

            // Otherwise, create a new entry
            else
            {
                // Reset the ID to 0 in order to save
                this.BaseModel.ID = 0;

                db_context.Add(this.BaseModel);
            }

            db_context.SaveChanges();

            // Update the ID
            UpdateDataObject();

            this.IsSet = true;
        }

        /// <summary>
        /// Check if the specified object is same as the current object
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(object? other)
        {
            // Default the value as the 0 to indicate the passed object is same as the current object
            int result_value = 0;

            // If the passed object is BaseLinkItem, check its components
            if (other is BaseLinkItem<TLeftID, TRightID> base_link_item)
            {
                // Check the ID
                if (result_value == 0)
                {
                    result_value = this.ID.CompareTo(base_link_item.ID);
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

            // If the passed object is not BaseLinkItem, return the flag to indicate is is not the same as the current object
            if (other is not BaseLinkItem<TLeftID, TRightID> base_link_item)
            {
                return is_equal;
            }

            // Check the types
            bool type_matches = this.GetType().Equals(base_link_item.GetType());

            // Check the ID
            bool value_matches = this.ID.Equals(base_link_item.ID);

            // If all component are the same, update the flag to indicate the passed object is the same as the current object
            if (type_matches && value_matches)
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
            return (base.GetHashCode() ^ this.ID.Value);
        }

        /// <summary>
        /// Check if the specified object is different from the current object
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator !=(BaseLinkItem<TLeftID, TRightID>? lhs, BaseLinkItem<TLeftID, TRightID>? rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Check if the specified object is same as the current object
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator ==(BaseLinkItem<TLeftID, TRightID>? lhs, BaseLinkItem<TLeftID, TRightID>? rhs)
        {
            // If both BaseLinkItem are not set, return equal
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

            // If both BaseLinkItem are set, compare those two objects
            return lhs.Equals(rhs);
        }
    }
}
