using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.ID;
using Web.Data.References;
using Web.Data.Stories;
using Web.Miscellaneous;
using Web.Models;
using X.PagedList;

namespace Web.Controllers
{
    public class ReferenceController : Controller
    {
        private readonly ProgramDbContext _db_context;
        private readonly MiscellaneousController _miscellaneous_controller;

        // Related story
        private static StoryID _story_id = new();

        // Lists
        private static ReferenceDataList? _reference_data_list = null;

        // Paging variables
        private static int _page_number_for_reference_data_list_page = 1;

        public ReferenceController(ProgramDbContext db_context)
        {
            _db_context = db_context;
            _miscellaneous_controller = new MiscellaneousController(db_context);
        }

        #region "Reference List"
        public IActionResult Index(int? story_id, int? page_number)
        {
            ReferenceDataList? reference_data_list = _reference_data_list;

            // Record the story
            if (story_id != null)
            {
                _story_id = new StoryID((int)story_id);

                // Reset the list for the new story
                reference_data_list = null;
            }

            // If the reference data list is not retrieve yet, load all the reference data from the database
            if (reference_data_list == null)
            {
                // Load all the reference data from the database
                reference_data_list = new(_story_id, _db_context);
            }

            // If the page number is not specified, use the last stored page number
            if (page_number == null)
            {
                page_number = _page_number_for_reference_data_list_page;
            }

            // Adjust the page number to fit the list, and store the page number in case the page refresh due to actions such as deletion
            _page_number_for_reference_data_list_page = GlobalMethods.GetValidPageNumber(page_number, reference_data_list.Count);

            this.ViewBag.StoryID = _story_id.Value;

            return View(GlobalWebPages.REFERENCE_LIST_PAGE, reference_data_list.ToPagedList(_page_number_for_reference_data_list_page, GlobalMethods.PAGE_SIZE));
        }

        public IActionResult Delete(string? reference_id)
        {
            // Retrieve the ID from the form
            ReferenceID id = new(StringMethods.ParseTextAsInt(reference_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Reference ID is not valid!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Load the reference
            ReferenceData reference_data = new(id, _db_context);
            if (!reference_data.IsSet)
            {
                ErrorData error_data = new("Reference is not found!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Delete the reference from story links
            StoryReferenceLinks story_reference_links = new(_story_id, _db_context);
            int item_index = story_reference_links.IndexOfID(id);
            story_reference_links.Delete(item_index);
            story_reference_links.Save(_db_context);

            // Delete the reference from the database
            reference_data.Delete(_db_context);

            // Reset lists
            _reference_data_list = null;

            _miscellaneous_controller.DisplaySuccessMessage("Reference is deleted successfully.", TempData);

            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion

        #region "Reference Detail"
        public IActionResult Create()
        {
            // Create a new reference data to set up the form
            ReferenceData reference_data = new();

            return View(GlobalWebPages.REFERENCE_DETAIL_PAGE, ReferenceDetailViewModel.ConvertToReferencenDetailViewModel(reference_data));
        }

        public IActionResult Edit(string? reference_id)
        {
            // Retrieve the ID from the form
            ReferenceID id = new(StringMethods.ParseTextAsInt(reference_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Reference ID is not invalid!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Retrieve the reference data from the database
            ReferenceData reference_data = new(id, _db_context);
            if (!reference_data.IsSet)
            {
                ErrorData error_data = new("Reference is not found!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            return View(GlobalWebPages.REFERENCE_DETAIL_PAGE, ReferenceDetailViewModel.ConvertToReferencenDetailViewModel(reference_data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveReferenceDetail(ReferenceDetailViewModel reference_detail_view_model)
        {
            ReferenceData reference_data = ReferenceDetailViewModel.ConvertToReferenceData(reference_detail_view_model, _db_context);

            // Verify the reference is valid
            // If fail, reload the page
            if (!reference_data.ValidateData(_db_context))
            {
                _miscellaneous_controller.RaiseError(reference_data.ErrorMessage, ModelState, TempData);

                // Reload the detail page to enter the data again
                return View(GlobalWebPages.REFERENCE_DETAIL_PAGE, ReferenceDetailViewModel.ConvertToReferencenDetailViewModel(reference_data));
            }

            // Save the reference
            reference_data.Save(_db_context);

            // Reset lists
            _reference_data_list = null;

            // Add the association between story and reference
            StoryReferenceLinks story_reference_links = new(_story_id, _db_context);
            story_reference_links.Add(_story_id, reference_data.ReferenceID);
            story_reference_links.Save(_db_context);

            // Create a pop-up message to notify the user
            _miscellaneous_controller.DisplaySuccessMessage("The reference is saved!", TempData);

            // Return to the reference list page
            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion
    }
}
