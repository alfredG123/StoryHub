using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.Characters;
using Web.Data.ID;
using Web.Miscellaneous;
using Web.Models;
using X.PagedList;

namespace Web.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ProgramDbContext _db_context;
        private readonly MiscellaneousController _miscellaneous_controller;

        // Related story
        private static int _story_id;

        // Paging variables
        private static int _page_number_for_character_data_list_page = 1;

        public CharacterController(ProgramDbContext db_context)
        {
            _db_context = db_context;
            _miscellaneous_controller = new MiscellaneousController(db_context);
        }

        #region "Character List"
        public IActionResult Index(int? story_id, int? page_number)
        {
            CharacterDataList? character_data_list = null;

            // If the character data list is not retrieve yet, load all the character data from the database
            if (character_data_list == null)
            {
                // Load all the character data from the database
                character_data_list = new(_db_context);
            }

            // If the page number is not specified, use the last stored page number
            if (page_number == null)
            {
                page_number = _page_number_for_character_data_list_page;
            }

            // Adjust the page number to fit the list, and store the page number in case the page refresh due to actions such as deletion
            _page_number_for_character_data_list_page = GlobalMethods.GetValidPageNumber(page_number, character_data_list.Count);

            // Record the story
            if (story_id != null)
            {
                _story_id = (int)story_id;
            }

            this.ViewBag.StoryID = _story_id;

            return View(GlobalWebPages.CHARACTER_LIST_PAGE, character_data_list.ToPagedList(_page_number_for_character_data_list_page, GlobalMethods.PAGE_SIZE));
        }

        public IActionResult Delete(string? character_id)
        {
            // Retrieve the ID from the form
            CharacterID id = new(StringMethods.ParseTextAsInt(character_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Character ID is not valid!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Load the character
            CharacterData character_data = new(id, _db_context);
            if (!character_data.IsSet)
            {
                ErrorData error_data = new("Character is not found!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Delete the character from the database
            character_data.Delete(_db_context);

            _miscellaneous_controller.DisplaySuccessMessage("Character is deleted successfully.", TempData);

            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion

        #region "Character Detail"
        public IActionResult Create()
        {
            // Create a new character data to set up the form
            CharacterData character_data = new();

            return View(GlobalWebPages.CHARACTER_DETAIL_PAGE, CharacterDetailViewModel.ConvertToCharacterDetailViewModel(character_data));
        }

        public IActionResult Edit(string? character_id)
        {
            // Retrieve the ID from the form
            CharacterID id = new(StringMethods.ParseTextAsInt(character_id));
            if (!id.IsSet)
            {
                ErrorData error_data = new("Character ID is not invalid!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Retrieve the character data from the database
            CharacterData character_data = new(id, _db_context);
            if (!character_data.IsSet)
            {
                ErrorData error_data = new("Character is not found!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            return View(GlobalWebPages.CHARACTER_DETAIL_PAGE, CharacterDetailViewModel.ConvertToCharacterDetailViewModel(character_data));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCharacterDetail(CharacterDetailViewModel character_detail_view_model)
        {
            CharacterData character_data = CharacterDetailViewModel.ConvertToCharacterData(character_detail_view_model, _db_context);

            // Verify the character is valid
            // If fail, reload the page
            if (!character_data.ValidateData(_db_context))
            {
                _miscellaneous_controller.RaiseError(character_data.ErrorMessage, ModelState, TempData);

                // Reload the detail page to enter the data again
                return View(GlobalWebPages.CHARACTER_DETAIL_PAGE, CharacterDetailViewModel.ConvertToCharacterDetailViewModel(character_data));
            }

            // Save the character
            character_data.Save(_db_context);

            // Create a pop-up message to notify the user
            _miscellaneous_controller.DisplaySuccessMessage("The character is saved!", TempData);

            // Return to the character list page
            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion
    }
}
