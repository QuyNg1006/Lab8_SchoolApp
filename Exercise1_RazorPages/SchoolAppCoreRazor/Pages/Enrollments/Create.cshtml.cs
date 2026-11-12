using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Enrollments
{
    public class CreateModel : PageModel
    {
        private readonly SchoolContext _context;

        public CreateModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrollment Item { get; set; } = default!;

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

            _context.Enrollments.Add(Item);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private void LoadLists(Enrollment? item = null)
        {
        ViewData["CourseID"] = new SelectList(
            _context.Courses.Select(d => new { d.CourseID, DisplayText = d.CourseID + " - " + d.Title }),
            "CourseID", "DisplayText", item?.CourseID);
        ViewData["StudentID"] = new SelectList(
            _context.Students.Select(d => new { d.StudentID, DisplayText = d.StudentID + " - " + d.LastName + " " + d.FirstName }),
            "StudentID", "DisplayText", item?.StudentID);
        }
    }
}
