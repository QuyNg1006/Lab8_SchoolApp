using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Enrollments
{
    public class IndexModel : PageModel
    {
        private readonly SchoolContext _context;

        public IndexModel(SchoolContext context)
        {
            _context = context;
        }

        public IList<Enrollment> Items { get; set; } = new List<Enrollment>();

        public async Task OnGetAsync()
        {
            Items = await _context.Enrollments.Include(x => x.Course).Include(x => x.Student).ToListAsync();
        }
    }
}
