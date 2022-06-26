using Resources.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using System.IO;
using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
//using System.Threading.Tasks;

namespace Resources.Controllers
{
    [Produces("application/json")]
    [Route("api/task")]
    public class TaskController : Controller
    {
        private readonly AppDBContext _context;

        public TaskController(AppDBContext context)
        {
            _context = context;
        }


     



        [HttpGet]
        public IEnumerable<WebApiTask> Get()
        {

         


            return _context.Tasks.ToList().Select(t => (WebApiTask)t);
        }


        [HttpGet("{id}")]
        public Models.Task? Get(int id)
        {
  
            return _context.Tasks.Find(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiTask"></param>
        /// <returns></returns>
        [HttpPost]
        public ObjectResult Post(WebApiTask apiTask)
        {
            var newTask = (Models.Task)apiTask;

            newTask.SortOrder = _context.Tasks.Max(t => t.SortOrder) + 1;

            _context.Tasks.Add(newTask);
            _context.SaveChanges();

            return Ok(new
            {
                tid = newTask.Id,
                action = "inserted"
            });
        }

        [HttpPut("{id}")]
        public ObjectResult? Put(int id, WebApiTask apiTask)
        {
            var updatedTask = (Models.Task)apiTask;
            var dbTask = _context.Tasks.Find(id);
            if (dbTask == null)
            {
                return null;
            }
            dbTask.Text = updatedTask.Text;
            dbTask.StartDate = updatedTask.StartDate;
            dbTask.Duration = updatedTask.Duration;
            dbTask.ParentId = updatedTask.ParentId;
            dbTask.Progress = updatedTask.Progress;
            dbTask.Type = updatedTask.Type;

            if (!string.IsNullOrEmpty(apiTask.target))
            {
                //reordering occurred                         
                this._UpdateOrders(dbTask, apiTask.target);
            }



            _context.SaveChanges();

            return Ok(new
            {
                action = "updated"
            });
        }

        [HttpDelete("{id}")]
        public ObjectResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                var links = _context.Links.Where(e => e.SourceTaskId == id).ToList();
                if (links != null) { foreach (var r in links) { _context.Links.Remove(r); } }
                var links1 = _context.Links.Where(e => e.TargetTaskId == id).ToList();
                if (links1 != null) { foreach (var r in links1) { _context.Links.Remove(r); } }
                _context.Tasks.Remove(task);
                _context.SaveChanges();

            }

            return Ok(new
            {
                action = "deleted"
            });
        }

        private void _UpdateOrders(Models.Task updatedTask, string orderTarget)
        {
            int adjacentTaskId;
            var nextSibling = false;

            var targetId = orderTarget;

            // adjacent task id is sent either as '{id}' or as 'next:{id}' depending 
            // on whether it's the next or the previous sibling
            if (targetId.StartsWith("next:"))
            {
                targetId = targetId.Replace("next:", "");
                nextSibling = true;
            }

            if (!int.TryParse(targetId, out adjacentTaskId))
            {
                return;
            }

            var adjacentTask = _context.Tasks.Find(adjacentTaskId);
            var startOrder = adjacentTask!.SortOrder;

            if (nextSibling)
                startOrder++;

            updatedTask.SortOrder = startOrder;

            var updateOrders = _context.Tasks
                .Where(t => t.Id != updatedTask.Id)
                .Where(t => t.SortOrder >= startOrder)
                .OrderBy(t => t.SortOrder);

            var taskList = updateOrders.ToList();

            taskList.ForEach(t => t.SortOrder++);
        }



        //Export To Excel
        #region ExcelExporter

        //public IActionResult Index()
        //{
        //   //List<Activities> activities = _context.Activities.ToList();
        //   List<Task> tasks = _context.Tasks.ToList();
        //    return View(tasks);
        //}



        //[HttpPost]
        //public IActionResult ExportExcel()
        //{
        //    var tasks = _context.Tasks.ToList();
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(Common.ToDataTable(tasks.ToList()));
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tasks.xlsx");
        //        }
        //    }
        //    return RedirectToAction("ExportExcel" , "Home" , tasks);
        //}

        //public static class Common
        //{
        //    public static DataTable ToDataTable<T>(List<T> items)
        //    {
        //        DataTable dataTable = new DataTable(typeof(T).Name);
        //        //Get all the properties
        //        PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //        foreach (PropertyInfo prop in Props)
        //        {
        //            //Setting column names as Property names
        //            dataTable.Columns.Add(prop.Name);
        //        }
        //        foreach (T item in items)
        //        {
        //            var values = new object[Props.Length];
        //            for (int i = 0; i < Props.Length; i++)
        //            {
        //                //inserting property values to datatable rows
        //                values[i] = Props[i].GetValue(item, null);
        //            }
        //            dataTable.Rows.Add(values);
        //        }
        //        //put a breakpoint here and check datatable
        //        return dataTable;
        //    }
        //}



        #endregion



    }
}
