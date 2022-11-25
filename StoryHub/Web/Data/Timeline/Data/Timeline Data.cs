using Web.Data.ID;
using Web.Models;

namespace Web.Data.Timeline
{
    [Serializable()]
    public class TimelineData : BaseDatabaseData
    {
        #region "Constructors"
        /// <summary>
        /// Create a new timeline data
        /// </summary>
        public TimelineData()
            : base(new TimelineID(), new TimelineDataModel())
        {
        }

        /// <summary>
        /// Retrieve the timeline data from the database by the timeline ID
        /// </summary>
        /// <param name="timeline_id"></param>
        /// <param name="db_context"></param>
        public TimelineData(TimelineID timeline_id, ProgramDbContext db_context)
            : base(timeline_id, db_context)
        {
        }

        /// <summary>
        /// Create a timeline data using the model
        /// </summary>
        /// <param name="timeline_data_model"></param>
        public TimelineData(TimelineDataModel timeline_data_model)
            : base(new TimelineID(timeline_data_model.ID), timeline_data_model)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Return the timeline ID
        /// </summary>
        public TimelineID TimelineID
        {
            get
            {
                return (TimelineID)this.ID;
            }
        }

        /// <summary>
        /// Return the time of the time
        /// </summary>
        public string Time { get; set; } = string.Empty;
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
            if (other_data is not TimelineData timeline_data)
            {
                return (false);
            }

            // Compare each property
            if (is_equal)
            {
                if (this.Time != timeline_data.Time)
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

            // Validate the timeline type
            if (is_valid)
            {
                if (string.IsNullOrEmpty(this.Time))
                {
                    is_valid = false;

                    this.ErrorMessage = "The time of the timeline is not set.";
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
            return (new TimelineDataModel());
        }

        /// <summary>
        /// Retrieve the model by the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        protected override BaseModel? RetrieveModelObject(BaseID id, ProgramDbContext db_context)
        {
            // Find the timeline from the database by the specifed ID
            return (db_context.TimelineData.Where(timeline_time => timeline_time.ID == id.Value).FirstOrDefault());
        }

        /// <summary>
        /// Set up the global variables with the data in the model
        /// </summary>
        protected override void UpdateDataObject()
        {
            TimelineDataModel timeline_data_model = (TimelineDataModel)this.BaseModel;
            TimelineID timeline_id = new(timeline_data_model.ID);

            this.ID = timeline_id;
            this.Time = timeline_data_model.Time;
            this.IsSet = timeline_id.IsSet;
        }

        /// <summary>
        /// Update the timeline model with the data from the global variables
        /// </summary>
        protected override void UpdateModelObject()
        {
            TimelineDataModel timeline_data_model = (TimelineDataModel)this.BaseModel;

            timeline_data_model.ID = this.TimelineID.Value;
            timeline_data_model.Time = this.Time;
        }
    }
}
