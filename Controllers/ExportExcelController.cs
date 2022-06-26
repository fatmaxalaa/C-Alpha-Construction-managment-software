using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Resources.Models;
using System.Data;
using System.Reflection;

namespace Resources.Controllers
{
    public class ExportExcelController : Controller
    {


        private readonly AppDBContext _context;

        public ExportExcelController(AppDBContext context)
        {

            _context = context;
        }


        public IActionResult Index()
        {
            List<Task> tasks = _context.Tasks.ToList();
            return View(tasks);
        }



        [HttpPost]
        public IActionResult ExportExcel()
        {
            var tasks = _context.Tasks.ToList();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(Common.ToDataTable(tasks.ToList()));
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Activity.xlsx");
                }
            }
            return View();
        }

        public static class Common
        {
            public static DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }







        #region ExcelV2

        //List<Task> _Task = new List<Task>();
        //public ExportExcelController()
        //{
        //    for (int i = 1; i <= _Task.Count; i++)
        //    {
        //        _Task.Add(new Task()
        //        {
        //            Id = i,
        //            Text = "Task" + i,
        //            Duration = i,
        //            Progress = i,
        //            ParentId = i,
        //            Type = "Type" +i,
        //            SortOrder = i
        //        });
        //    }
        //}



        //public IActionResult Index()
        //{
        //    using (var workbook = new XLWorkbook())
        //    {
        //        var workSheet = workbook.Worksheets.Add("Tasks");
        //        //var workSheet2 = workbook.Worksheets.Add("school");
        //        var currentRow = 1;

        //        //Header...
        //        workSheet.Cell(currentRow, 1).Value = "TaskId";
        //        workSheet.Cell(currentRow, 2).Value = "TaskName";
        //        workSheet.Cell(currentRow, 3).Value = "Duration";
        //        workSheet.Cell(currentRow, 4).Value = "Progress";
        //        workSheet.Cell(currentRow, 5).Value = "ParentId";
        //        workSheet.Cell(currentRow, 6).Value = "Type";
        //        workSheet.Cell(currentRow, 7).Value = "SortOrder";

        //        //Header...
        //        //workSheet2.Cell(currentRow, 1).Value = "schoolId";
        //        //workSheet2.Cell(currentRow, 2).Value = "Name";
        //        //workSheet2.Cell(currentRow, 3).Value = "Roll";

        //        //Body...
        //        foreach (var t in _Task)
        //        {
        //            currentRow++;
        //            workSheet.Cell(currentRow, 1).Value = t.Id;
        //            workSheet.Cell(currentRow, 2).Value = t.Text;
        //            workSheet.Cell(currentRow, 3).Value = t.Duration;
        //            workSheet.Cell(currentRow, 4).Value = t.Progress;
        //            workSheet.Cell(currentRow, 5).Value = t.ParentId;
        //            workSheet.Cell(currentRow, 6).Value = t.Type;
        //            workSheet.Cell(currentRow, 7).Value = t.SortOrder;
        //        }

        //        //foreach (var t in _Task)
        //        //{
        //        //    currentRow++;
        //        //    workSheet2.Cell(currentRow, 1).Value = t.StudentId;
        //        //    workSheet2.Cell(currentRow, 2).Value = t.Name;
        //        //    workSheet2.Cell(currentRow, 3).Value = t.roll;
        //        //}

        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //            var content = stream.ToArray();

        //            return File(
        //                content,
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                "Tasks.xlsx"
        //                );
        //        }



        //    }
        //}

        #endregion




    }
}
