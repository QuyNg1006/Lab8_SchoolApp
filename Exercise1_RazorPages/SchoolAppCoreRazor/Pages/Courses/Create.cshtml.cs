using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly SchoolContext _context;

        public CreateModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Item { get; set; } = default!;

        public IActionResult OnGet()
        {
            LoadLists();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadLists(Item);
                return Page();
            }

            _context.Courses.Add(Item);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private void LoadLists(Course? item = null)
        {
        ViewData["DepartmentID"] = new SelectList(
            _context.Departments.Select(d => new { d.DepartmentID, DisplayText = d.DepartmentID + " - " + d.DepartmentName }),
            "DepartmentID", "DisplayText", item?.DepartmentID);
        }
    }
}
