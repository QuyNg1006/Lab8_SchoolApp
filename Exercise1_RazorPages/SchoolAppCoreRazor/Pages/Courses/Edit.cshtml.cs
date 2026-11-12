using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Courses
{
    public class EditModel : PageModel
    {
        private readonly SchoolContext _context;

        public EditModel(SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Course Item { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);
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
                if (!_context.Courses.Any(e => e.CourseID == Item.CourseID)) return NotFound();
                throw;
            }
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
