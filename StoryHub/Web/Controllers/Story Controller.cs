using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.ID;
using Web.Data.Stories;
using Web.Miscellaneous;
using Web.Models;
using X.PagedList;

namespace Web.Controllers
{
    public class StoryController : Controller
    {
        private readonly ProgramDbContext _db_context;
        private readonly MiscellaneousController _miscellaneous_controller;

        // Paging variables
        private static int _page_number_for_story_data_list_page = 1;

        public StoryController(ProgramDbContext db_context)
        {
            _db_context = db_context;
            _miscellaneous_controller = new MiscellaneousController(db_context);
        }

        #region "Story List"
        public IActionResult Index(int? page_number)
        {
            StoryDataList? story_data_list = null;

            // If the story data list is not retrieve yet, load all the story data from the database
            if (story_data_list == null)
            {
                // Load all the story data from the database
                story_data_list = new(_db_context);
            }

            // If the page number is not specified, use the last stored page number
            if (page_number == null)
            {
                page_number = _page_number_for_story_data_list_page;
            }

            // Adjust the page number to fit the list, and store the page number in case the page refresh due to actions such as deletion
            _page_number_for_story_data_list_page = GlobalMethods.GetValidPageNumber(page_number, story_data_list.Count);

            return View("StoryList", story_data_list.ToPagedList(_page_number_for_story_data_list_page, GlobalMethods.PAGE_SIZE));
        }

        public IActionResult Delete(string? story_id)
        {
            // Retrieve the ID from the form
            StoryID id = new(StringMethods.ParseTextAsInt(story_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Story ID is not valid!");

                return View("Error", error_data);
            }

            // Load the story
            StoryData story_data = new(id, _db_context);
            if (!story_data.IsSet)
            {
                ErrorData error_data = new("Story is not found!");

                return View("Error", error_data);
            }

            // Delete the story from the database
            story_data.Delete(_db_context);

            _miscellaneous_controller.DisplaySuccessMessage("story is deleted successfully.", TempData);

            return RedirectToAction("Index");
        }
        #endregion

        #region "Story Detail"
        public IActionResult Create()
        {
            // Create a new story data to set up the form
            StoryData story_data = new();

            return View("StoryDetail", StoryDetailViewModel.ConvertToStoryDetailViewModel(story_data));
        }

        public IActionResult Edit(string? story_id)
        {
            // Retrieve the ID from the form
            StoryID id = new(StringMethods.ParseTextAsInt(story_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Story ID is not invalid!");

                return View("Error", error_data);
            }

            // Retrieve the story data from the database
            StoryData story_data = new(id, _db_context);
            if (!story_data.IsSet)
            {
                ErrorData error_data = new("Story is not found!");

                return View("Error", error_data);
            }

            return View("StoryDetail", StoryDetailViewModel.ConvertToStoryDetailViewModel(story_data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveStoryDetail(StoryDetailViewModel story_detail_view_model)
        {
            StoryData story_data = StoryDetailViewModel.ConvertToStoryData(story_detail_view_model, _db_context);

            // Verify the story is valid
            // If fail, reload the page
            if (!story_data.ValidateData(_db_context))
            {
                _miscellaneous_controller.RaiseError(story_data.ErrorMessage, ModelState, TempData);

                // Reload the detail page to enter the data again
                return View("StoryDetail", StoryDetailViewModel.ConvertToStoryDetailViewModel(story_data));
            }

            // Save the story
            story_data.Save(_db_context);

            // Create a pop-up message to notify the user
            _miscellaneous_controller.DisplaySuccessMessage("The story is saved!", TempData);

            // Return to the story list page
            return RedirectToAction("Index");
        }
        #endregion
    }
}
