using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Web.Data;

namespace Web.Controllers
{
    public class MiscellaneousController : Controller
    {
        private readonly ProgramDbContext _db_context;

        public MiscellaneousController(ProgramDbContext db_context)
        {
            _db_context = db_context;
        }

        /// <summary>
        /// Display the message using the styled box for success
        /// </summary>
        /// <param name="success_message"></param>
        /// <param name="temp_data"></param>
        public void DisplaySuccessMessage(string success_message, ITempDataDictionary temp_data)
        {
            // Set up the message in the web page using 'success' style
            temp_data["success"] = success_message;
        }

        /// <summary>
        /// Set up the error flag, and display the error message
        /// </summary>
        /// <param name="error_message"></param>
        public void RaiseError(string error_message, ModelStateDictionary model_state_dicitionary, ITempDataDictionary temp_data)
        {
            string guid = Guid.NewGuid().ToString();

            // Change the model state to indicate the data is not valid
            model_state_dicitionary.AddModelError(guid, error_message);

            // Update the error message in the web page
            temp_data["error"] = error_message;
        }
    }
}
