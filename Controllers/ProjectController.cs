using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resources.Models;
using System.Threading.Tasks;

namespace Resources.Controllers
{
    public class ProjectController:Controller
    {
        public AppDBContext _context { get; set; }
        public ProjectController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()

        {
            var Companies = await _context.Projects.ToListAsync();
            return View(Companies);
        }
        public IActionResult Interface()

        {

            return View();
        }
        //public IActionResult Index() { return View(); }
        //public async Task<IActionResult> Details(int? employeeId)
        //{
        //    if (employeeId == null) { return NotFound(); }


        //    //var company = await _context.projects.FirstOrDefaultAsync(m => m.ID == employeeId);
        //    //var employee = await _context.employees.Include(e => e.company).FirstOrDefaultAsync(m => m.ID == employeeId);
        //    var t = await _context.wbs.Where(e => e.ProjecttID == employeeId).ToListAsync();

        //    if (t == null) { return NotFound(); }
        //    return View(t);
        //}
        // GET: Employees/Delete/1
        public async Task<IActionResult> Delete(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }
            var company = await _context.Projects.FirstOrDefaultAsync(m => m.ID == ID);

            return View(company);
        }

        // POST: Employees/Delete/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int ID)
        {
            var project = await _context.Projects.FindAsync(ID);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        //AddOrEdit Get Method
        public async Task<IActionResult> AddOrEdit(int? ID)
        {
           
            ViewBag.PageName = ID == null ? "Create Project" : "Edit Project";
            ViewBag.IsEdit = ID == null ? false : true;
            if (ID == null)
            {
                return View();
            }
            else
            {
                var project = await _context.Projects.FindAsync(ID);

                if (project == null)
                {
                    return NotFound();
                }
                //ViewBag.ownerName=project.OwnerName;
                return View(project);
            }
        }

        //AddOrEdit Post Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int ID, [Bind("ID,Name,Location,OwnerName,ProjectTemplate,Location,ManagerName,ProjectStartDate,ProjectEndDate,ProjectDuration")] Project ProjectData)

        {
            bool IsProjectExist = false;

            Project comp = await _context.Projects.FindAsync(ID);

            if (comp != null)
            {
                IsProjectExist = true;
            }
            else
            {
                comp = new Project();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    comp.ID = ProjectData.ID;
                    comp.Name = ProjectData.Name;
                    comp.OwnerName = ProjectData.OwnerName;
                    comp.Location = ProjectData.Location;
                    comp.ManagerName = ProjectData.ManagerName;
                    comp.ProjectStartDate = ProjectData.ProjectStartDate;
                    comp.ProjectEndDate = ProjectData.ProjectEndDate;
                    comp.ProjectDuration = ProjectData.ProjectDuration;




                    if (IsProjectExist)
                    {
                        _context.Update(comp);
                    }
                    else
                    {

                        _context.Add(comp);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }
            return View(ProjectData);
        }

    }

}
