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
        public IActionResult ViewSelection(string previous_page, string previous_controller, int? page_number)
        {
            // Throw errors, if the selection list is not set
            if (_selection_list == null)
            {
                ErrorData error_data = new("The selection list is not set!");

                return View(GlobalWebPages.ERROR_PAGE, error_data);
            }

            // Adjust the page number to fit the list, and store the page number in case the page refresh due to actions such as deletion
            _page_number_for_selection_list_page = GlobalMethods.GetValidPageNumber(page_number, _selection_list.Count);

            this.ViewBag.PreviousPage = previous_page;
            this.ViewBag.PreviousController = previous_controller;

            return View(GlobalWebPages.SELECTION_LIST_PAGE, _selection_list.ToPagedList(_page_number_for_selection_list_page, GlobalMethods.PAGE_SIZE));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveSelection(int id)
        {
            // Return to the timeline list page
            return RedirectToAction(GlobalWebPages.INDEX_ACTION);
        }
        #endregion
    }
}
