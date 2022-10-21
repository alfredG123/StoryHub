using Web.Data.ID;
using Web.Models;

namespace Web.Data.References
{
    [Serializable()]
    public class ReferenceData : BaseDatabaseData
    {
        #region "Constructors"
        /// <summary>
        /// Create a new reference data
        /// </summary>
        public ReferenceData()
            : base(new ReferenceID(), new ReferenceDataModel())
        {
        }

        /// <summary>
        /// Retrieve the reference data from the database by the reference ID
        /// </summary>
        /// <param name="reference_id"></param>
        /// <param name="db_context"></param>
        public ReferenceData(ReferenceID reference_id, ProgramDbContext db_context)
            : base(reference_id, db_context)
        {
        }

        /// <summary>
        /// Create a reference data using the model
        /// </summary>
        /// <param name="reference_data_model"></param>
        public ReferenceData(ReferenceDataModel reference_data_model)
            : base(new ReferenceID(reference_data_model.ID), reference_data_model)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Return the reference ID
        /// </summary>
        public ReferenceID ReferenceID
        {
            get
            {
                return (ReferenceID)this.ID;
            }
        }

        /// <summary>
        /// Return the title of the reference
        /// </summary>
        public string Title { get; set; } = string.Empty;
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
            if (other_data is not ReferenceData reference_data)
            {
                return (false);
            }

            // Compare each property
            if (is_equal)
            {
                if (this.Title != reference_data.Title)
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

            // Validate the title
            if (is_valid)
            {
                if (string.IsNullOrEmpty(this.Title))
                {
                    is_valid = false;

                    this.ErrorMessage = "The title of the reference is not set.";
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
            return (new ReferenceDataModel());
        }

        /// <summary>
        /// Retrieve the model by the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        protected override BaseModel? RetrieveModelObject(BaseID id, ProgramDbContext db_context)
        {
            // Find the reference from the database by the specifed ID
            return (db_context.ReferenceData.Where(reference_item => reference_item.ID == id.Value).FirstOrDefault());
        }

        /// <summary>
        /// Set up the global variables with the data in the model
        /// </summary>
        protected override void UpdateDataObject()
        {
            ReferenceDataModel reference_data_model = (ReferenceDataModel)this.BaseModel;
            ReferenceID reference_id = new(reference_data_model.ID);

            this.ID = reference_id;
            this.Title = reference_data_model.Title;
            this.IsSet = reference_id.IsSet;
        }

        /// <summary>
        /// Update the reference model with the data from the global variables
        /// </summary>
        protected override void UpdateModelObject()
        {
            ReferenceDataModel reference_data_model = (ReferenceDataModel)this.BaseModel;

            reference_data_model.ID = this.ReferenceID.Value;
            reference_data_model.Title = this.Title;
        }
    }
}
