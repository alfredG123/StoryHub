using Web.Data.ID;
using Web.Models;

namespace Web.Data.Characters
{
    public class CharacterData : BaseDatabaseData
    {
        #region "Constructors"
        /// <summary>
        /// Create a new character data
        /// </summary>
        public CharacterData()
            : base(new CharacterID(), new CharacterDataModel())
        {
        }

        /// <summary>
        /// Retrieve the character data from the database by the character ID
        /// </summary>
        /// <param name="character_id"></param>
        /// <param name="db_context"></param>
        public CharacterData(CharacterID character_id, ProgramDbContext db_context)
            : base(character_id, db_context)
        {
        }

        /// <summary>
        /// Create a character data using the model
        /// </summary>
        /// <param name="character_data_model"></param>
        public CharacterData(CharacterDataModel character_data_model)
            : base(new CharacterID(character_data_model.ID), character_data_model)
        {
        }
        #endregion

        #region Properties
        /// <summary>
        /// Return the character ID
        /// </summary>
        public CharacterID CharacterID
        {
            get
            {
                return (CharacterID)this.ID;
            }
        }

        /// <summary>
        /// Return or Set the name of the character
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Return or Set the description of the character
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
            if (other_data is not CharacterData character_data)
            {
                return (false);
            }

            // Compare each property
            if (is_equal)
            {
                if (this.Name != character_data.Name)
                {
                    is_equal = false;
                }
            }
            if (is_equal)
            {
                if (this.Description != character_data.Description)
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
                if (string.IsNullOrEmpty(this.Name))
                {
                    is_valid = false;

                    this.ErrorMessage = "The name of the character is not set.";
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
            return (new CharacterDataModel());
        }

        /// <summary>
        /// Retrieve the model by the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="db_context"></param>
        /// <returns></returns>
        protected override BaseModel? RetrieveModelObject(BaseID id, ProgramDbContext db_context)
        {
            // Find the character from the database by the specifed ID
            return (db_context.CharacterData.Where(character_item => character_item.ID == id.Value).FirstOrDefault());
        }

        /// <summary>
        /// Set up the global variables with the data in the model
        /// </summary>
        protected override void UpdateDataObject()
        {
            CharacterDataModel character_data_model = (CharacterDataModel)this.BaseModel;
            CharacterID character_id = new(character_data_model.ID);

            this.ID = character_id;
            this.Name = character_data_model.Name;
            this.Description = character_data_model.Description;
            this.IsSet = character_id.IsSet;
        }

        /// <summary>
        /// Update the character model with the data from the global variables
        /// </summary>
        protected override void UpdateModelObject()
        {
            CharacterDataModel character_data_model = (CharacterDataModel)this.BaseModel;

            character_data_model.ID = this.CharacterID.Value;
            character_data_model.Name = this.Name;
            character_data_model.Description = this.Description;
        }
    }
}
