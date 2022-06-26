using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Resources.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task = Resources.Models.Task;


namespace Resources.Controllers
{
    
        public class HomeController : Controller
        {
            private readonly ILogger<HomeController> _logger;
            private readonly AppDBContext _context;
            public HomeController(ILogger<HomeController> logger, AppDBContext context)
            {
                _logger = logger;
                _context = context;
              }
            
            public IActionResult Index()
            {
                return View();
            }
        public IActionResult trial()
        {
            return View();
        }

        public IActionResult ExportExcel()
        {
            return View();
        }

        public IActionResult Calendar()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }


            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        [HttpGet]

        [HttpGet]

        public ActionResult Schedule()

        {
            var Activities = _context.Tasks.ToList();
            List<Link> relations = _context.Links.ToList();

            foreach (var act in Activities)
            {
                act.StartDate = DateTime.Now;
                act.EndDate = act.StartDate.AddDays(act.Duration);
            }


            foreach (Task item in Activities)
            {
                foreach (var rell in relations)
                {
                    foreach (var rel in relations)
                    {
                        Task source = Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId);
                        Task target = Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId);
                        var lag1 = _context.Lags.Where(e => e.SourceName == source.Text).ToList();
                        Lag lag2 = lag1.FirstOrDefault(e => e.TargetName == target.Text);
                        int Lag = 0;
                        if (lag2 != null) { Lag = lag2.LagValue; }
                        switch (rel.Type)
                        {
                            case "0":
                                DateTime t1 = Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).EndDate.AddDays(Lag);
                                if (t1 > Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate)
                                {
                                    Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate = t1;
                                    Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).EndDate = Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate.AddDays(Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).Duration);
                                };
                                break;
                            case "2":
                                DateTime t3 = Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).EndDate.AddDays(Lag - Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).Duration);
                                if (t3 > Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate) { Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate = t3; Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).EndDate = Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate.AddDays(Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).Duration); };
                                break;
                            case "1":
                                DateTime t2 = Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).StartDate.AddDays(Lag);
                                if (t2 > Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate) { Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate = t2; Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).EndDate = Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate.AddDays(Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).Duration); };
                                break;

                            case "3":
                                DateTime t4 = Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).EndDate.AddDays(Lag - Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).Duration);
                                if (t4 < Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate) { Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate = t4; Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).EndDate = Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate.AddDays(Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).Duration); };
                                break;
                            default:
                                return RedirectToAction(nameof(Index));
                        }

                    }

                }
            }
            foreach (var act in Activities)
            {

                act.EndDate = act.StartDate.AddDays(act.Duration);
            }

            List<int> situation = new List<int>();

            //////////////////////
            ////////////////////////////// start of float calculation
            ///


            DateTime a = DateTime.MinValue;//End of project
            DateTime b = DateTime.MaxValue;//start of project

            foreach (Task item in Activities)
            {
                if (item.EndDate > a)
                {
                    a = item.EndDate;
                }
                if (item.StartDate < b)
                {
                    b = item.StartDate;
                }
            }

            foreach (Task item in Activities)
            {
                item.LateFinish = a;
                item.LateStart = item.LateFinish.AddDays(-item.Duration);
            }
            foreach (Task item in Activities)
            {
                //item.LateStart = item.LateFinish.AddDays(-(int)(item.Duration));
                foreach (var rell in relations)
                {
                    foreach (var rel in relations)
                    {
                        Task source = Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId);
                        Task target = Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId);
                        List<Lag> lag1 = _context.Lags.Where(e => e.SourceName == source.Text).ToList();
                        Lag lag2 = lag1.FirstOrDefault(e => e.TargetName == target.Text);
                        int Lag = 0;
                        if (lag2 != null) { Lag = lag2.LagValue; }
                        switch (rel.Type)
                        {
                            case "0":
                                DateTime t1 = Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).LateStart.AddDays(-Lag);
                                if (t1 < Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateFinish)
                                {
                                    Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateFinish = t1;
                                    Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateStart = Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateFinish.AddDays(-(int)(Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).Duration));
                                };
                                break;
                            case "2":
                                DateTime t3 = Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).LateFinish.AddDays(-Lag);
                                if (t3 < Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateFinish)
                                {
                                    Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateFinish = t3;
                                    Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateStart = Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).StartDate.AddDays(Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).Duration);
                                };
                                break;
                            case "1":
                                DateTime t2 = Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).LateStart.AddDays(-Lag);
                                if (t2 < Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateStart)
                                {
                                    Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateStart = t2;
                                    Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateFinish = Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateStart.AddDays(Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).Duration);
                                };
                                break;

                            case "3":
                                DateTime t4 = Activities.FirstOrDefault(e => e.Id == rel.TargetTaskId).LateFinish.AddDays(-Lag);
                                if (t4 < Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateStart)
                                {
                                    Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateStart = t4;
                                    Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateFinish = Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).LateStart.AddDays(Activities.FirstOrDefault(e => e.Id == rel.SourceTaskId).Duration);
                                };
                                break;
                            default:
                                return RedirectToAction(nameof(Index));


                        }
                    }

                }

            }
            foreach (Task item in Activities)
            {
                item.EndDate = item.StartDate.AddDays(item.Duration);
                item.TotalFloat = (int)(item.LateStart - item.StartDate).Days;
                if (item.TotalFloat == 0) { item.Criticality = Criticality.High; }
                else { item.Criticality = Criticality.Low; }
            }



            /////////////////////////
            for (int i = 0; i < ((a - b).Days * 2); i++)
            {
                DateTime check = a.AddDays(i);
                if (check.DayOfWeek == DayOfWeek.Friday) { situation.Add(0); }
                else { situation.Add(1); }
                check = a;
            }
            foreach (Task tsk in Activities)
            {

                int value = (int)(tsk.StartDate - b).Days;
                int value2 = (int)(tsk.EndDate - b).Days;
                int init = 0;
                int init2 = 0;
                for (int i = 0; i < situation.Count; i++)
                {


                    if (init == value)
                    {
                        tsk.StartDate = b.AddDays(i);

                    }
                    if (situation[i] == 1) { init++; }
                }
                for (int i = 0; i < situation.Count; i++)
                {

                    if (init2 == value2)
                    {
                        tsk.EndDate = b.AddDays(i);//try b.adddays(i)

                    }
                    if (situation[i] == 1) { init2++; }
                }
            }
            foreach (var Act in Activities)
            {
                Act.LateFinish = Act.EndDate.AddDays(Act.TotalFloat);
                Act.LateStart = Act.StartDate.AddDays(Act.TotalFloat);
            }

            ////////////////////////////////
            foreach (var Act in _context.Tasks)
            {
                _context.Remove(Act);
            }
            foreach (var Act in Activities)
            {
                _context.Add(Act);
                _context.Update(Act);
            }
            //Companies = _context.activities.ToList();
            _context.SaveChangesAsync();





            return View(Activities);
        }
        public ActionResult CriticalPath()
        {


            var CriticalActivities = _context.Tasks.Where(e => e.TotalFloat == 0);
            List<string> ActivitiesNames = new List<string>();
            List<int> ActivitiesID = new List<int>();
            List<DateTime> Starts = new List<DateTime>();
            List<DateTime> Ends = new List<DateTime>();
            List<int> StartsYears = new List<int>();
            List<int> StartsMonth = new List<int>();
            List<int> StartsDays = new List<int>();
            List<int> EndYears = new List<int>();
            List<int> EndMonth = new List<int>();
            List<int> EndDays = new List<int>();
            foreach (Models.Task item in CriticalActivities)
            {
                ActivitiesNames.Add(item.Text);
                ActivitiesID.Add(item.Id);
                Starts.Add(item.StartDate);
                Ends.Add(item.EndDate);
                StartsYears.Add(item.StartDate.Year);
                StartsMonth.Add(item.StartDate.Month);
                StartsDays.Add(item.StartDate.Day);
                EndYears.Add(item.EndDate.Year);
                EndMonth.Add(item.EndDate.Month);
                EndDays.Add(item.EndDate.Day);
            }
            ViewBag.ActivitiesNames = ActivitiesNames;
            ViewBag.ActivitiesID = ActivitiesID;
            ViewBag.Starts = Starts;
            ViewBag.Ends = Ends;
            ViewBag.StartsYears = StartsYears;
            ViewBag.StartsDays = StartsDays;
            ViewBag.StartsMonth = StartsMonth;
            ViewBag.EndYears = EndYears;
            ViewBag.EndDays = EndDays;
            ViewBag.EndMonth = EndMonth;
            return View(CriticalActivities);


        }
        //public void SetCurrentProjectId(int id)
        //{
        //    var globals = _context.Globals.First();
        //    globals.ProjectId = id;
        //    ////////////////////////////////

        //    _context.Remove(_context.Globals.First());

        //    _context.Add(globals);
        //    _context.Update(globals);
        //    _context.SaveChangesAsync();


        //}
        //public async Task<IActionResult> EditProjectdets(int? id)
        //{

        //    var acts = await _context.Projects.ToListAsync();

        //    List<string> projectname = new List<string>();
        //    foreach (var item in acts)
        //    {
        //        projectname.Add(item.Name);
        //        _context.Update(item);
        //        await _context.SaveChangesAsync();
        //    }
        //    ViewBag.actsoptions = projectname;

        //    var tasks = await _context.Tasks.ToListAsync();
        //    List<string> tasksName = new List<string>();
        //    foreach(var task in tasks)
        //    {
        //        tasksName.Add(task.Text);
        //        _context.Update(task);
        //        await _context.SaveChangesAsync();
        //    }
        //    ViewBag.tasksoptions = tasksName;
        //    //if (id == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    //var taskdata = await _context.Tasks.FindAsync(id);
        //    //if (taskdata == null)
        //    //{
        //    //    return NotFound();
        //    //}

        //    return View();

        //}



        ///////////////////////////////////
        public async Task<IActionResult> EditProjectdets(int? id)
        {
            var acts = await _context.Projects.ToListAsync();

            List<string> projectname = new List<string>();
            foreach (var item in acts)
            {
                projectname.Add(item.Name);
                _context.Update(item);
               _context.SaveChanges();
            }
            ViewBag.actsoptions = projectname;

            var tasks = await _context.Tasks.ToListAsync();
            List<string> tasksName = new List<string>();
            foreach (var task in tasks)
            {
                tasksName.Add(task.Text);
                _context.Update(task);
              
            }
            ViewBag.tasksoptions = tasksName;

            var types = await _context.Tasks.ToListAsync();
            List<int> taskType = new List<int>();
            foreach (var task in types)
            {
                taskType.Add((int)task.type1);
                _context.Update(task);

            }
            ViewBag.tasksTypesoptions = taskType;

            _context.SaveChanges();
            ViewBag.PageName = id == null ? "Create Tasks" : "Edit Tasks";
            ViewBag.IsEdit = id == null ? false : true;
            if (id == null)
            {
                return View();
            }
            else
            {
                var tasks_1 = await _context.Tasks.FindAsync(id);
                if (tasks_1 == null)
                {
                    return NotFound();
                }
                return View(tasks_1);
            }
           
            return View();
        }

        ///////////////////////////////////////////

        // POST: tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProjectdets(int id, [Bind("Id,Text,StartDate,Duration,EndDate,ParentId,Progress,Type,SortOrder,resources,projectsrelated,TotalFloat,LateStart,LateFinish,Criticality")]
        Task taskTypeData)
        {
            bool IsCrashExist = false;
            Task task = await _context.Tasks.FindAsync(taskTypeData.Id);
            if (task!= null)
            {
                IsCrashExist = true;
            }
            else
            {
                task= new Task();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    //task.Id = taskTypeData.Id;
                    task.Text=taskTypeData.Text;
                    task.type1=taskTypeData.type1;
                    task.projectsrelated=taskTypeData.projectsrelated;

                    if (IsCrashExist)
                    {
                        _context.Update(taskTypeData);
                    }
                    else
                    {
                        _context.Add(task);
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
            return View(taskTypeData);
        }


    }
}


