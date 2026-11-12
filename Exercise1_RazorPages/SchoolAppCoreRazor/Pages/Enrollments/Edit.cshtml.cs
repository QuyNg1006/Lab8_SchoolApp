using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Enrollments
{
    public class EditModel : PageModel
    {
        private readonly SchoolContext _context;

        public EditModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Enrollment Item { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.Enrollments.FirstOrDefaultAsync(m => m.EnrollmentID == id);
            if (item == null) return NotFound();
            Item = item;
            LoadLists(Item);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadLists(Item);
                return Page();
            }

            _context.Attach(Item).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Enrollments.Any(e => e.EnrollmentID == Item.EnrollmentID)) return NotFound();
                throw;
            }
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
