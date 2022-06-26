using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resources.Models;


namespace Resources.Controllers
{
    public class CrashesController : Controller
    {
        public AppDBContext _context { get; set; }

        public CrashesController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Crashes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Crashings.ToListAsync());
        }

        // GET: Crashes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crash = await _context.Crashings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (crash == null)
            {
                return NotFound();
            }

            return View(crash);
        }

        // GET: Crashes/Create
        public async Task<IActionResult> Create(int? Id)
        {
            List<Models.Task> acts = await _context.Tasks.ToListAsync();
            List<int> actsname = new List<int>();
            foreach (var item in acts)
            {
                if (item.TotalFloat== 0)
                {
                    actsname.Add(item.Duration);
                }
            }
            ViewBag.actsoptions = actsname;


            List<string> actname = new List<string>();
            foreach (var item in acts)
            {
                if (item.TotalFloat== 0)
                {
                    actname.Add(item.Text);
                }
            }
            ViewBag.actsoptionsName = actname;

            List<int> actID = new List<int>();
            foreach (var item in acts)
            {
                if (item.TotalFloat== 0)
                {
                    actID.Add(item.Id);
                }
            }
            ViewBag.actsoptionsID = actID;

            ViewBag.PageName = Id == null ? "Create Activity" : "Edit Activity";
            ViewBag.IsEdit = Id == null ? false : true;
            if (Id == null)
            {
                return View();
            }
            else
            {
                var activity = await _context.Tasks.FindAsync(Id);

                if (Id == null)
                {
                    return NotFound();
                }
                return View(acts);
            }

        }

        // POST: Crashes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OptimisticDuration,MostLikelyDuration,PessimesticDuration,ExpectedTime,RequiredTime,Probability,TaskId,TaskName")] Crashing crash)
        {
            if (ModelState.IsValid)
            {
                _context.Add(crash);
                //_context.Update(crash);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(crash);
        }

        // GET: Crashes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            ViewBag.PageName = id == null ? "Create Crash" : "Edit Crash";
            ViewBag.IsEdit = id == null ? false : true;
            if (id == null)
            {
                return View();
            }
            else
            {
                var crash = await _context.Crashings.FindAsync(id);
                if (crash == null)
                {
                    return NotFound();
                }
                return View(crash);
            }
        }

        // POST: Crashes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OptimisticDuration,MostLikelyDuration,PessimesticDuration,ExpectedTime,TaskId,RequiredTime")] Crashing crashData)
        {
            bool IsCrashExist = false;
            Crashing crashes = await _context.Crashings.FindAsync(crashData.Id);
            if (crashes!= null)
            {
                IsCrashExist = true;
            }
            else
            {
                crashes= new Crashing();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    crashes.Id = crashData.Id;
                    crashes.OptimisticDuration= crashData.OptimisticDuration;
                    crashes.MostLikelyDuration = crashData.MostLikelyDuration;
                    crashData.PessimesticDuration= crashData.PessimesticDuration;
                    crashData.ExpectedTime=crashData.ExpectedTime;
                    crashData.TaskId= crashData.TaskId;
                    crashData.TaskName=crashData.TaskName;
                    crashData.RequiredTime = crashData.RequiredTime;

                    if (IsCrashExist)
                    {
                        _context.Update(crashData);
                    }
                    else
                    {
                        _context.Add(crashes);
                        //_context.Update(crashes);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw;

                }
                return RedirectToAction(nameof(Index));
            }
            return View(crashData);
        }

        // GET: Crashes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var crash = await _context.Crashings
                .FirstOrDefaultAsync(m => m.Id == id);


            return View(crash);
        }

        // POST: Crashes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int crashingId, [Bind("Id,OptimisticDuration,MostLikelyDuration,PessimesticDuration,ExpectedTime,TaskId")]
        Crashing crashesData)
        {
            var crash = await _context.Crashings.FindAsync(crashesData.Id);
            _context.Crashings.Remove(crash);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CrashExists(int id)
        {
            return _context.Crashings.Any(e => e.Id == id);
        }



        [HttpGet]
        public ActionResult Calc_Te()
        {
            List<Crashing> crashes = _context.Crashings.ToList();
            //expectedTime = crashes.Select(crashes)

            crashes.First().Te = crashes.First().ExpectedTime;
            for (int i = 1; i < crashes.Count; i++)
            {
                crashes[i].Te = crashes[i-1].Te+crashes[i].ExpectedTime;

            }
            foreach (Crashing item in crashes)
            {
                _context.Remove(item);
            }
            foreach (Crashing item in crashes)
            {
                _context.Add(item);
                _context.Update(item);
            }
            crashes = _context.Crashings.ToList();

            decimal.Round((decimal)crashes.Last().Te, 2, MidpointRounding.AwayFromZero);
            _context.SaveChanges();
            return View(crashes);
        }


        [HttpGet]
        public ActionResult Variance()
        {
            List<Crashing> crashes = _context.Crashings.ToList();
            crashes[0].TotalSegma = crashes[0].Segma;
            for (int i = 1; i < crashes.Count; i++)
            {
                crashes[i].TotalSegma =Math.Pow((Math.Pow((crashes[i-1].TotalSegma), 2)+Math.Pow((crashes[i].Segma), 2)), 0.5);

            }
            foreach (Crashing item in _context.Crashings)
            {
                _context.Remove(item);
            }
            foreach (Crashing item in crashes)
            {
                _context.Add(item);
                _context.Update(item);
            }
            crashes = _context.Crashings.ToList();
            _context.SaveChanges();
            decimal seg = ((decimal)crashes.Last().TotalSegma);
            seg = Math.Round(seg, 2, MidpointRounding.AwayFromZero);
            ViewBag.variance =seg;
            return View(crashes);
        }

        [HttpGet]
        public ActionResult Probability()
        {

            List<Crashing> crashesTe = _context.Crashings.ToList();



            double timeTe = crashesTe.Last().Te;

            double segma = crashesTe.Last().TotalSegma;

            double timeTs = crashesTe.Last().RequiredTime;


            double Z = -1*((timeTs-timeTe)/segma)/100;

            double probability = crashesTe.Last().Probability;

            //double Prob = 0;

            if (Z == 0)
            {
                crashesTe.Last().Probability=0.5*100;
            }
            else if (Z >0 && Z <= 0.26)
            {
                crashesTe.Last().Probability=0.6*100;
            }
            else if (Z >0.26 && Z <=0.53)
            {
                crashesTe.Last().Probability=0.7*100;
            }
            else if (Z >0.53 && Z <=0.85)
            {
                crashesTe.Last().Probability=0.8*100;
            }
            else if (Z >0.85 && Z <=1.29)
            {
                crashesTe.Last().Probability = 0.9*100;
            }
            //probability = Prob;
            crashesTe = _context.Crashings.ToList();
            _context.SaveChanges();
            ViewBag.ts= timeTs;
            ViewBag.Prob=crashesTe.Last().Probability;


            return View(crashesTe);
        }
    }
}
