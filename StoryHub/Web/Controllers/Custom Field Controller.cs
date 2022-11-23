using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.CustomFields;
using Web.Data.ID;
using Web.Data.Stories;
using Web.Miscellaneous;
using Web.Models;
using X.PagedList;

namespace Web.Controllers
{
    public class CustomFieldController : Controller
    {
        private readonly ProgramDbContext _db_context;
        private readonly MiscellaneousController _miscellaneous_controller;

        // Related story
        private static StoryID _story_id = new();

        // Lists
        private static CustomFieldDataList? _custom_field_data_list = null;

        // Paging variables
        private static int _page_number_for_custom_field_data_list_page = 1;

        public CustomFieldController(ProgramDbContext db_context)
        {
            _db_context = db_context;
            _miscellaneous_controller = new MiscellaneousController(db_context);
        }

        #region "Custom Field List"
        public IActionResult Index(int? story_id, int? page_number)
        {
            CustomFieldDataList? custom_field_data_list = _custom_field_data_list;

            // Record the story
            if (story_id != null)
            {
                _story_id = new StoryID((int)story_id);

                // Reset the list for the new story
                custom_field_data_list = null;
            }

            // If the custom field data list is not retrieve yet, load all the custom field data from the database
            if (custom_field_data_list == null)
            {
                // Load all the custom field data from the database
                custom_field_data_list = new(_story_id, _db_context);
            }

            // If the page number is not specified, use the last stored page number
            if (page_number == null)
            {
                page_number = _page_number_for_custom_field_data_list_page;
            }

            // Adjust the page number to fit the list, and store the page number in case the page refresh due to actions such as deletion
            _page_number_for_custom_field_data_list_page = GlobalMethods.GetValidPageNumber(page_number, custom_field_data_list.Count);

            this.ViewBag.StoryID = _story_id.Value;

            return View(GlobalWebPages.CUSTOM_FIELD_LIST_PAGE, custom_field_data_list.ToPagedList(_page_number_for_custom_field_data_list_page, GlobalMethods.PAGE_SIZE));
        }

        public IActionResult Delete(string? custom_field_id)
        {
            // Retrieve the ID from the form
            CustomFieldID id = new(StringMethods.ParseTextAsInt(custom_field_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Custom Field ID is not valid!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Load the custom field
            CustomFieldData custom_field_data = new(id, _db_context);
            if (!custom_field_data.IsSet)
            {
                ErrorData error_data = new("Custom Field is not found!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Delete the custom field from the database
            custom_field_data.Delete(_db_context);

            // Reset lists
            _custom_field_data_list = null;

            // Delete the custom field from story links
            StoryCustomFieldLinks story_custom_field_links = new(_story_id, _db_context);
            int item_index = story_custom_field_links.IndexOfID(id);
            story_custom_field_links.Delete(item_index);
            story_custom_field_links.Save(_db_context);

            _miscellaneous_controller.DisplaySuccessMessage("Custom Field is deleted successfully.", TempData);

            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion

        #region "Custom Field Detail"
        public IActionResult Create()
        {
            // Create a new custom field data to set up the form
            CustomFieldData custom_field_data = new();

            return View(GlobalWebPages.CUSTOM_FIELD_DETAIL_PAGE, CustomFieldDetailViewModel.ConvertToCustomFieldDetailViewModel(custom_field_data));
        }

        public IActionResult Edit(string? custom_field_id)
        {
            // Retrieve the ID from the form
            CustomFieldID id = new(StringMethods.ParseTextAsInt(custom_field_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Custom Field ID is not invalid!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Retrieve the custom field data from the database
            CustomFieldData custom_field_data = new(id, _db_context);
            if (!custom_field_data.IsSet)
            {
                ErrorData error_data = new("Custom Field is not found!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            return View(GlobalWebPages.CUSTOM_FIELD_DETAIL_PAGE, CustomFieldDetailViewModel.ConvertToCustomFieldDetailViewModel(custom_field_data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCustomFieldDetail(CustomFieldDetailViewModel custom_field_detail_view_model)
        {
            CustomFieldData custom_field_data = CustomFieldDetailViewModel.ConvertToCustomFieldData(custom_field_detail_view_model, _db_context);

            // Verify the custom field is valid
            // If fail, reload the page
            if (!custom_field_data.ValidateData(_db_context))
            {
                _miscellaneous_controller.RaiseError(custom_field_data.ErrorMessage, ModelState, TempData);

                // Reload the detail page to enter the data again
                return View(GlobalWebPages.CUSTOM_FIELD_DETAIL_PAGE, CustomFieldDetailViewModel.ConvertToCustomFieldDetailViewModel(custom_field_data));
            }

            // Save the custom field
            custom_field_data.Save(_db_context);

            // Reset lists
            _custom_field_data_list = null;

            // Add the association between story and custom field
            StoryCustomFieldLinks story_custom_field_links = new(_story_id, _db_context);
            story_custom_field_links.Add(_story_id, custom_field_data.CustomFieldID);
            story_custom_field_links.Save(_db_context);

            // Create a pop-up message to notify the user
            _miscellaneous_controller.DisplaySuccessMessage("The custom field is saved!", TempData);

            // Return to the custom field list page
            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion
    }
}
