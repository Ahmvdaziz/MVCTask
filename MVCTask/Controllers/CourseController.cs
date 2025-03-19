using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCTask.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTask.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _context.Courses.Include(c => c.Instructor).ToListAsync();
            return View(courses);
        }

        public IActionResult Create()
        {
            ViewBag.Instructors = _context.Instructors.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                if (course.InstructorId == 0) 
                {
                    course.InstructorId = null;
                }

                _context.Courses.Add(course);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Course added successfully!";
                return RedirectToAction("Index");
            }

            return View(course);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses.FindAsync(id);
            if (course == null) return NotFound();

            ViewBag.Instructors = _context.Instructors.ToList(); 

            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.CourseId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = " Course updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Courses.Any(e => e.CourseId == course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.Instructors = _context.Instructors.ToList();
            return View(course);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses.FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null) return NotFound();

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = " Course deleted successfully!";
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var course = await _context.Courses.Include(c => c.Instructor)
                                               .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null) return NotFound();

            return View(course);
        }
    }
}
