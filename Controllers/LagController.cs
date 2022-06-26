using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resources.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Controllers
{
    public class LagController : Controller
    {
        private readonly AppDBContext _context;

        public LagController(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lags.ToListAsync());

        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Lags = await _context.Lags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (Lags == null)
            {
                return NotFound();
            }

            return View(Lags);
        }
        public async Task<IActionResult> Create(int? ID, string srcn)
        {

            var acts = await _context.Lags.ToListAsync();
            List<string> SourceNames = new List<string>();
            List<string> TargetNames = new List<string>();
            var Links= await _context.Links.ToListAsync();
            foreach(var link in Links)
            { var a = await _context.Tasks.FindAsync(link.SourceTaskId);
                SourceNames.Add(a.Text);
                var b= await _context.Tasks.FindAsync(link.TargetTaskId);
                TargetNames.Add(b.Text);
            }

            ViewBag.names = SourceNames;
            ViewBag.TargetNames = TargetNames;

            if (ID == null)
            {
                return View();
            }
            else
            {
                var activity = await _context.Lags.FindAsync(ID);

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

            var resource = await _context.Lags
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
            var project = await _context.Lags.FindAsync(ID);
            _context.Lags.Remove(project);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int ID, [Bind("SourceName,TargetName,LagValue")] Lag LagData)

        {
            bool IsLagExist = false;

            Lag comp = await _context.Lags.FindAsync(ID);

            if (comp != null)
            {
                IsLagExist = true;
            }
            else
            {
                comp = new Lag();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    comp.SourceName = LagData.SourceName;
                    comp.LagValue = LagData.LagValue;
                    comp.Id = LagData.Id;
                    comp.TargetName = LagData.TargetName;
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
