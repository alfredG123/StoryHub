using Web.Data.ID;
using Web.Models;

namespace Web.Data.CustomFields
{
    [Serializable()]
    public class CustomFieldData : BaseDatabaseData
    {
        #region "Constructors"
        /// <summary>
        /// Create a new custom field data
        /// </summary>
        public CustomFieldData()
            : base(new CustomFieldID(), new CustomFieldDataModel())
        {
        }

        /// <summary>
        /// Retrieve the custom field data from the database by the custom field ID
        /// </summary>
        /// <param name="custom_field_id"></param>
        /// <param name="db_context"></param>
        public CustomFieldData(CustomFieldID custom_field_id, ProgramDbContext db_context)
            : base(custom_field_id, db_context)
        {
        }

        /// <summary>
        /// Create a custom field data using the model
        /// </summary>
        /// <param name="custom_field_data_model"></param>
        public CustomFieldData(CustomFieldDataModel custom_field_data_model)
            : base(new CustomFieldID(custom_field_data_model.ID), custom_field_data_model)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Return the Custom Field ID
        /// </summary>
        public CustomFieldID CustomFieldID
        {
            get
            {
                return (CustomFieldID)this.ID;
            }
        }

        /// <summary>
        /// Return or Set the name of the custom field
        /// </summary>
        public string FieldName { get; set; } = string.Empty;

        /// <summary>
        /// Return or Set the name of the custom field
        /// </summary>
        public string Content { get; set; } = string.Empty;
        #endregion

        /// <summary>
        /// Compare the types and proprties except ID
        /// Return true, if the properties are equal
        /// Otherwise, return false
        /// </summary>
        /// <param name="other_data"></param>
        /// <returns></returns>
        public override bool CompareContent(BaseDatabaseData other_data)
        {
            bool is_equal = true;

            // If the passed object is not set, return the flag to indicate it is not the same as the current object
            if (other_data is null)
            {
                return (false);
            }

            // If the passed object is not BaseID, return the flag to indicate is is not the same as the current object
            if (other_data is not CustomFieldData custom_field_data)
            {
                return (false);
            }

            // Compare each property
            if (is_equal)
            {
                if (this.FieldName != custom_field_data.FieldName)
                {
                    is_equal = false;
                }
            }
            if (is_equal)
            {
                if (this.Content != custom_field_data.Content)
                {
                    is_equal = false;
                }
            }

            return (is_equal);
        }

        /// <summary>
        /// Validate the data is valid to save
        /// </summary>
        /// <param name="db_context"></param>
        /// <returns></returns>
        public override bool ValidateData(ProgramDbContext db_context)
        {
            bool is_valid = true;

            this.ErrorMessage = String.Empty;

            // Validate the name
            if (is_valid)
            {
                if (string.IsNullOrEmpty(this.FieldName))
                {
                    is_valid = false;

                    this.ErrorMessage = "The name of the custom field is not set.";
                }
            }

            return is_valid;
        }

        /// <summary>
        /// Create a model object
        /// </summary>
        /// <returns></returns>
        protected override BaseModel CreateEmptyModelObject()
        {
            return (new CustomFieldDataModel());
        }

        /// <summary>
        /// Retrieve the model by the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        protected override BaseModel? RetrieveModelObject(BaseID id, ProgramDbContext db_context)
        {
            // Find the custom field from the database by the specifed ID
            return (db_context.CustomFieldData.Where(custom_field_item => custom_field_item.ID == id.Value).FirstOrDefault());
        }

        /// <summary>
        /// Set up the global variables with the data in the model
        /// </summary>
        protected override void UpdateDataObject()
        {
            CustomFieldDataModel custom_field_data_model = (CustomFieldDataModel)this.BaseModel;
            CustomFieldID custom_field_id = new(custom_field_data_model.ID);

            this.ID = custom_field_id;
            this.FieldName = custom_field_data_model.FieldName;
            this.Content = custom_field_data_model.Content;
            this.IsSet = custom_field_id.IsSet;
        }

        /// <summary>
        /// Update the custom field model with the data from the global variables
        /// </summary>
        protected override void UpdateModelObject()
        {
            CustomFieldDataModel custom_field_data_model = (CustomFieldDataModel)this.BaseModel;

            custom_field_data_model.ID = this.CustomFieldID.Value;
            custom_field_data_model.FieldName = this.FieldName;
            custom_field_data_model.Content = this.Content;
        }
    }
}
