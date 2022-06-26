using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resources.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Resources.Controllers
{
    public class AddTypesController:Controller
    {
        private readonly AppDBContext _context;

        public AddTypesController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.AddTypess.ToListAsync());

        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Lags = await _context.AddTypess
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Lags == null)
            {
                return NotFound();
            }

            return View(Lags);
        }
        public async Task<IActionResult> Create(int? ID, string srcn)
        {

            var acts = await _context.AddTypess.ToListAsync();
            List<string> TasksNames = new List<string>();
            List<string> TargetNames = new List<string>();
            var Links = await _context.Tasks.ToListAsync();
            foreach (var link in Links)
            {
                TasksNames.Add(link.Text);
            }
                //foreach (var link in Links)
                //{
                //    var a = await _context.Tasks.FindAsync(link.SourceTaskId);
                //    SourceNames.Add(a.Text);
                //    var b = await _context.Tasks.FindAsync(link.TargetTaskId);
                //    TargetNames.Add(b.Text);
                //}

                ViewBag.names = TasksNames;
         

            if (ID == null)
            {
                return View();
            }
            else
            {
                var activity = await _context.AddTypess.FindAsync(ID);

                if (ID == null)
                {
                    return NotFound();
                }
                return View(acts);
            }

        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.AddTypess
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int ID)
        {
            var project = await _context.AddTypess.FindAsync(ID);
            _context.AddTypess.Remove(project);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int ID, [Bind("TaskName,Type")] AddTypes LagData)

        {
            bool IsLagExist = false;

            AddTypes comp = await _context.AddTypess.FindAsync(ID);

            if (comp != null)
            {
                IsLagExist = true;
            }
            else
            {
                comp = new AddTypes();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    comp.Id = LagData.Id;
                    comp.TaskName = LagData.TaskName;
                    comp.Type = LagData.Type;
                    if (IsLagExist)
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
            return View(LagData);
        }


    }
}
