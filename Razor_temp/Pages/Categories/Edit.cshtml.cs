using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor_temp.Data;
using Razor_temp.Models;

namespace Razor_temp.Pages.Categories
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult OnGet(int id)
        {
            // Use the 'id' parameter to fetch and display the resource with that ID
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            Category = categoryFromDb;
            return Page();
        }

        public IActionResult OnPost()
        {
            
            if (Category == null)
            {
                return NotFound();
            }
            _db.Categories.Update(Category);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToPage("Index");
        }
    }
}
