using Web.Data.ID;
using Web.Models;

namespace Web.Data
{
    public abstract class BaseDatabaseData
    {
        /// <summary>
        /// Compare the types and proprties except ID
        /// Return true, if the properties are equal
        /// Otherwise, return false
        /// </summary>
        /// <param name="other_data"></param>
        /// <returns></returns>
        public abstract bool CompareContent(BaseDatabaseData other_data);

        /// <summary>
        /// Create a model object
        /// </summary>
        /// <returns></returns>
        protected abstract BaseModel CreateEmptyModelObject();

        /// <summary>
        /// Retrieve the model object from the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        protected abstract BaseModel? RetrieveModelObject(BaseID id, ProgramDbContext db_context);

        /// <summary>
        /// Update the data object for retrieving the data from the database
        /// </summary>
        protected abstract void UpdateDataObject();

        /// <summary>
        /// Update the model object for saving the data in database
        /// </summary>
        protected abstract void UpdateModelObject();

        #region "Constructors"
        /// <summary>
        /// Set up the data object with the data from the specified model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="base_model"></param>
        public BaseDatabaseData(BaseID id, BaseModel base_model)
        {
            this.ID = id;
            this.BaseModel = base_model;

            // Set up the global variables with the specified model
            UpdateDataObject();

            // If the ID is not set, update the global variables
            if (!id.IsSet)
            {
                SetUpDefaultValueForDataObject();
            }
        }

        /// <summary>
        /// Retrieve the data from the database by the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        public BaseDatabaseData(BaseID id, ProgramDbContext db_context)
        {
            BaseModel? retrieved_model = null;
            bool is_model_retrieved = false;

            this.ID = id;

            // Try to retrieve the model from the database, if the id is set
            if (id.IsSet)
            {
                retrieved_model = RetrieveModelObject(id, db_context);
            }

            // Create a model object
            if (retrieved_model == null)
            {
                this.BaseModel = CreateEmptyModelObject();
            }

            // Use the retrieved the model
            else
            {
                this.BaseModel = retrieved_model;

                is_model_retrieved = true;
            }

            // Set up the global variables with the retrieved model
            UpdateDataObject();

            // Set up the global variables with the default values
            if (!is_model_retrieved)
            {
                SetUpDefaultValueForDataObject();
            }
        }
        #endregion

        #region "Properties"
        /// <summary>
        /// Return the message for the error in the data
        /// </summary>
        public string ErrorMessage { get; protected set; } = string.Empty;

        /// <summary>
        /// Return the flag to indicate whether the data object is in the database
        /// </summary>
        public bool IsSet { get; protected set; }

        /// <summary>
        /// Return the model
        /// </summary>
        protected BaseModel BaseModel { get; }

        /// <summary>
        /// Return the ID of the model
        /// </summary>
        protected BaseID ID { get; set; }
        #endregion

        /// <summary>
        /// Validate the data is valid to save
        /// </summary>
        /// <param name="db_context"></param>
        /// <returns></returns>
        public virtual bool CheckIfUsed(ProgramDbContext db_context)
        {
            return false;
        }

        /// <summary>
        /// Delete the current data entry from the database
        /// </summary>
        /// <param name="db_context"></param>
        public void Delete(ProgramDbContext db_context)
        {
            // If the item is not saved yet, return
            if (!this.IsSet)
            {
                return;
            }

            // Delete the item from database
            db_context.Remove(this.BaseModel);
            db_context.SaveChanges();

            // Update the ID
            UpdateDataObject();

            // Clean up the global list in case the item is in it
            ResetGlobalData();

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

            // Clean up the global list to force other to reload data
            ResetGlobalData();

            this.IsSet = true;
        }

        /// <summary>
        /// Validate the data is valid to save
        /// </summary>
        /// <param name="db_context"></param>
        /// <returns></returns>
        public virtual bool ValidateData(ProgramDbContext db_context)
        {
            return true;
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

            // If the passed object is BaseDatabaseData, check its components
            if (other is BaseDatabaseData base_database_data)
            {
                // Check the ID
                if (result_value == 0)
                {
                    result_value = this.ID.CompareTo(base_database_data.ID);
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

            // If the passed object is not BaseDatabaseData, return the flag to indicate is is not the same as the current object
            if (other is not BaseDatabaseData base_database_data)
            {
                return is_equal;
            }

            // Check the types
            bool type_matches = this.GetType().Equals(base_database_data.GetType());

            // Check the ID
            bool value_matches = this.ID.Equals(base_database_data.ID);

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
        public static bool operator !=(BaseDatabaseData? lhs, BaseDatabaseData? rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Check if the specified object is same as the current object
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static bool operator ==(BaseDatabaseData? lhs, BaseDatabaseData? rhs)
        {
            // If both BaseDatabaseData are not set, return equal
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

            // If both BaseDatabaseData are set, compare those two objects
            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Reset the global list to clean up old data
        /// </summary>
        protected virtual void ResetGlobalData()
        {
            // Do nothing
        }

        /// <summary>
        /// Set up the default value for global variables for creating a new object
        /// </summary>
        protected virtual void SetUpDefaultValueForDataObject()
        {
            // Do nothing
        }
    }
}
