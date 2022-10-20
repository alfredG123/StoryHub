using Web.Data.ID;
using Web.Models;

namespace Web.Data.Regions
{
    public class RegionData : BaseDatabaseData
    {
        #region "Constructors"
        /// <summary>
        /// Create a new region data
        /// </summary>
        public RegionData()
            : base(new RegionID(), new RegionDataModel())
        {
        }

        /// <summary>
        /// Retrieve the region data from the database by the region ID
        /// </summary>
        /// <param name="region_id"></param>
        /// <param name="db_context"></param>
        public RegionData(RegionID region_id, ProgramDbContext db_context)
            : base(region_id, db_context)
        {
        }

        /// <summary>
        /// Create a region data using the model
        /// </summary>
        /// <param name="region_data_model"></param>
        public RegionData(RegionDataModel region_data_model)
            : base(new RegionID(region_data_model.ID), region_data_model)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Return the region ID
        /// </summary>
        public RegionID RegionID
        {
            get
            {
                return (RegionID)this.ID;
            }
        }

        /// <summary>
        /// Return the name of the region
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Return the description of the region
        /// </summary>
        public string Description { get; set; } = string.Empty;
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
            if (other_data is not RegionData region_data)
            {
                return (false);
            }

            // Compare each property
            if (is_equal)
            {
                if (this.Name != region_data.Name)
                {
                    is_equal = false;
                }
            }
            if (is_equal)
            {
                if (this.Description != region_data.Description)
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
                if (string.IsNullOrEmpty(this.Name))
                {
                    is_valid = false;

                    this.ErrorMessage = "The name of the region is not set.";
                }
            }
            if (is_valid)
            {
                if (string.IsNullOrEmpty(this.Description))
                {
                    is_valid = false;

                    this.ErrorMessage = "The description of the region is not set.";
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
            return (new RegionDataModel());
        }

        /// <summary>
        /// Retrieve the model by the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        protected override BaseModel? RetrieveModelObject(BaseID id, ProgramDbContext db_context)
        {
            // Find the region from the database by the specifed ID
            return (db_context.RegionData.Where(region_item => region_item.ID == id.Value).FirstOrDefault());
        }

        /// <summary>
        /// Set up the global variables with the data in the model
        /// </summary>
        protected override void UpdateDataObject()
        {
            RegionDataModel region_data_model = (RegionDataModel)this.BaseModel;
            RegionID region_id = new(region_data_model.ID);

            this.ID = region_id;
            this.Name = region_data_model.Name;
            this.Description = region_data_model.Description;
            this.IsSet = region_id.IsSet;
        }

        /// <summary>
        /// Update the region model with the data from the global variables
        /// </summary>
        protected override void UpdateModelObject()
        {
            RegionDataModel region_data_model = (RegionDataModel)this.BaseModel;

            region_data_model.ID = this.RegionID.Value;
            region_data_model.Name = this.Name;
            region_data_model.Description = this.Description;
        }
    }
}
