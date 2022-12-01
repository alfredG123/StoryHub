using Microsoft.AspNetCore.Mvc;
using Web.Data;
using Web.Data.General;
using Web.Miscellaneous;
using X.PagedList;

namespace Web.Controllers
{
    public class GeneralController : Controller
    {
        private readonly ProgramDbContext _db_context;
        
        // List
        private static SelectionList? _selection_list = null;

        // Paging variables
        private static int _page_number_for_selection_list_page = 1;

        // Previous page information
        private static string _previous_action = string.Empty;
        private static string _previous_controller = string.Empty;

        // Selection
        private static int _selected_id = -1;

        public GeneralController(ProgramDbContext db_context)
        {
            _db_context = db_context;
        }

        /// <summary>
        /// Set up the global for the selection list
        /// </summary>
        /// <param name="selection_list"></param>
        public static void SetupSelectionList(SelectionList selection_list)
        {
            _selection_list = selection_list;
        }

        #region "Selection"
        public IActionResult ViewSelection(string? previous_action, string? previous_controller, int? page_number)
        {
            // Throw errors, if the selection list is not set
            if (_selection_list == null)
            {
                ErrorData error_data = new("The selection list is not set!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Adjust the page number to fit the list, and store the page number in case the page refresh due to actions such as deletion
            _page_number_for_selection_list_page = GlobalMethods.GetValidPageNumber(page_number, _selection_list.Count);

            if (previous_action != null)
            {
                _previous_action = previous_action;
            }
            if (previous_controller != null)
            {
                _previous_controller = previous_controller;
            }

            this.ViewBag.PreviousPage = previous_action;
            this.ViewBag.PreviousController = previous_controller;

            return View(GlobalWebPages.SELECTION_LIST_PAGE, _selection_list.ToPagedList(_page_number_for_selection_list_page, GlobalMethods.PAGE_SIZE));
        }

        public IActionResult SaveSelection(int id)
        {
            _selected_id = id;

            return RedirectToAction(_previous_action, _previous_controller);
        }
        #endregion

        public IActionResult BackToPreviousPage()
        {
            return RedirectToAction(_previous_action, _previous_controller);
        }
    }
}
