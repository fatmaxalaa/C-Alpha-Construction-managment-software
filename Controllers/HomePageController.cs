using Microsoft.AspNetCore.Mvc;
using Resources.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resources.Controllers
{
    public class HomePageController : Controller
    {
        private readonly AppDBContext _context;

        public HomePageController(AppDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //to Call user Name in home page
            var user = _context.users.FirstOrDefault().UserName;
            ViewBag.userName=user;

            ////call owner name in home page
            //var owner = _context.Projects.FirstOrDefault(e => e.OwnerName == "Wesam Morsy").ManagerName;
            //ViewBag.Owner = owner;


            //open tasks
            List<Models.Task> tasks = _context.Tasks.Where(s => s.EndDate>System.DateTime.Now).ToList();
            var numTask = tasks.Count();
            ViewBag.tasksNum = numTask;

            //closed tasks
            List<Models.Task> tasksEnded = _context.Tasks.Where(s => s.EndDate<System.DateTime.Now).ToList();
            ViewBag.Ended= tasksEnded.Count();

            //Issues
            List<Issues> issues = _context.Issues.ToList();
            var numIssues = issues.Count();
            ViewBag.issuessNum = numIssues;


            //Visulization of open Milstone
            List<Models.AddTypes> milestone = _context.AddTypess.Where(e => e.Type== "Mile Stone").Where(e => e.StartTaskDate > System.DateTime.Today).ToList();
            ViewBag.milestone= milestone.Count();


            //Visulization of Closed Milestone
            List<Models.AddTypes> milestone_end = _context.AddTypess.Where(e => e.Type== "Mile Stone").Where(e => e.StartTaskDate < System.DateTime.Today).ToList();
            ViewBag.milestone_end= milestone_end.Count();


            //Visulization of open tasks
            var acts = _context.Tasks.Where(e => e.EndDate > System.DateTime.Today).ToList();
            List<string> tasks1_S = new List<string>();
            foreach (var task in tasks)
            {
                tasks1_S.Add(task.Text);
            }
            ViewBag.task1 = tasks1_S;


            //Visulization of Closed tasks
            var endedtask = _context.Tasks.Where(e => e.EndDate <System.DateTime.Now).ToList();
            List<string> tasks_End = new List<string>();
            foreach (var task in endedtask)
            {
                tasks_End.Add(task.Text);
            }
            ViewBag.endedtask = tasks_End;

            ////// project duration
            //List<Project> projDur = _context.Projects.Where(e=>e.ID==2).Where(e => e.ProjectDuration> 1).ToList();

            //ViewBag.projDuration= projDur;


            //Visulization of open Issues
            var openIssue = _context.Issues.ToList();
            List<string> open_iss = new List<string>();
            foreach (var task in openIssue)
            {
                open_iss.Add(task.IsusseName);
            }
            ViewBag.openissue= open_iss;


            //Visulization of open Issues
            var closedIssue = _context.Issues.ToList();
            List<string> closeissue = new List<string>();
            foreach (var task in closedIssue)
            {
                closeissue.Add(task.IsusseName);
            }
            ViewBag.closeissue= closeissue;

            return View();
        }
        //public async Task<IActionResult> CountTasks(int Id)
        //{
        //    List<Models.Task> tasks = _context.Tasks.Where(s=>s.StartDate>System.DateTime.Now).ToList();
        //    //foreach(var task in _context.Tasks)
        //    //{
        //    //    if(task.StartDate > System.DateTime.Today)
        //    //    {
        //    //        tasks.Add(task);
        //    //    }
        //    //}
        //    var numTask = tasks.Count();
        //    ViewBag.tasksNum = numTask;
        //    return RedirectToAction("Index","HomePage",numTask);
        //}
    }
}
