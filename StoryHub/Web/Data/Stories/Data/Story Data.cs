using Web.Data.ID;
using Web.Enumerations;
using Web.Models;

namespace Web.Data.Stories
{
    public class StoryData : BaseDatabaseData
    {
        #region "Constructors"
        /// <summary>
        /// Create a new story data
        /// </summary>
        public StoryData()
            : base(new StoryID(), new StoryDataModel())
        {
        }

        /// <summary>
        /// Retrieve the story data from the database by the story ID
        /// </summary>
        /// <param name="story_id"></param>
        /// <param name="db_context"></param>
        public StoryData(StoryID story_id, ProgramDbContext db_context)
            : base(story_id, db_context)
        {
        }

        /// <summary>
        /// Create a story data using the model
        /// </summary>
        /// <param name="story_data_model"></param>
        public StoryData(StoryDataModel story_data_model)
            : base(new StoryID(story_data_model.ID), story_data_model)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Return the story ID
        /// </summary>
        public StoryID StoryID
        {
            get
            {
                return (StoryID)this.ID;
            }
        }

        /// <summary>
        /// Return the title of the story
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Return the creation date of the story
        /// </summary>
        public DateTime CreationDate { get; set; } = DateTime.Now;
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
            if (other_data is not StoryData story_data)
            {
                return (false);
            }

            // Compare each property
            if (is_equal)
            {
                if (this.Title != story_data.Title)
                {
                    is_equal = false;
                }
            }
            if (is_equal)
            {
                if (this.CreationDate != story_data.CreationDate)
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

            // Validate the story type
            if (is_valid)
            {
                if (string.IsNullOrEmpty(this.Title))
                {
                    is_valid = false;

                    this.ErrorMessage = "The title of the story is not set.";
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
            return (new StoryDataModel());
        }

        /// <summary>
        /// Retrieve the model by the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        protected override BaseModel? RetrieveModelObject(BaseID id, ProgramDbContext db_context)
        {
            // Find the story from the database by the specifed ID
            return (db_context.StoryData.Where(story_item => story_item.ID == id.Value).FirstOrDefault());
        }

        /// <summary>
        /// Set up the global variables with the data in the model
        /// </summary>
        protected override void UpdateDataObject()
        {
            StoryDataModel story_data_model = (StoryDataModel)this.BaseModel;
            StoryID story_id = new(story_data_model.ID);

            this.ID = story_id;
            this.Title = story_data_model.Title;
            this.CreationDate = story_data_model.CreationDate;
        }

        /// <summary>
        /// Update the story model with the data from the global variables
        /// </summary>
        protected override void UpdateModelObject()
        {
            StoryDataModel story_data_model = (StoryDataModel)this.BaseModel;

            story_data_model.ID = this.StoryID.Value;
            story_data_model.Title = this.Title;
            story_data_model.CreationDate = this.CreationDate;
        }
    }
}
