using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FusionCharts.DataEngine;
using FusionCharts.Visualization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Resources.Models;


namespace Resources.Controllers
{
    public class ResourceController : Controller
    {
        private readonly AppDBContext _context;

        public ResourceController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Resources
        public async Task<IActionResult> Index()
        {
            return View(await _context.Resources.ToListAsync());

        }

        // GET: Resources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources
                .FirstOrDefaultAsync(m => m.ResourceId == id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // GET: Resources/Create
        public async Task<IActionResult> Create(int? ID)
        

            {


            var acts = await _context.Tasks.ToListAsync();
            List<string> actsname = new List<string>();
            foreach (var item in acts)
            {
                actsname.Add(item.Text);
            }
            ViewBag.actsoptions = actsname;

            ViewBag.PageName = ID == null ? "Create Activity" : "Edit Activity";
            ViewBag.IsEdit = ID == null ? false : true;
            if (ID == null)
            {
                return View();
            }
            else
            {
                var activity = await _context.Tasks.FindAsync(ID);

                if (ID == null)
                {
                    return NotFound();
                }
                return View(acts);
            }

        }

        // POST: Resources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,ResourceId,ResourceType,CostPerDay,Duration,UnitsPerDay,TotalCost,TaskName")] Resource resource)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(resource);
        }

        // GET: Resources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var acts = await _context.Tasks.ToListAsync();
            List<string> actsname = new List<string>();
            foreach (var item in acts)
            {
                actsname.Add(item.Text);
            }
            ViewBag.actsoptions = actsname;
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
            {
                return NotFound();
            }
            return View(resource);
        }

        // POST: Resources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,ResourceId,ResourceType,CostPerDay,Duration,UnitsPerDay,TotalCost,TaskName")] Resource resource)
        {
            if (id != resource.ResourceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResourceExists(resource.ResourceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(resource);
        }

        // GET: Resources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resource = await _context.Resources
                .FirstOrDefaultAsync(m => m.ResourceId == id);
            if (resource == null)
            {
                return NotFound();
            }

            return View(resource);
        }

        // POST: Resources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resource = await _context.Resources.FindAsync(id);
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResourceExists(int id)
        {
            return _context.Resources.Any(e => e.ResourceId == id);
        }
        /// <summary>
        /// ///////////
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> CashReport(int? id)
        {

            DateTime span1 = DateTime.MaxValue;
            DateTime span2 = DateTime.MinValue;
            List<double> costs = new List<double>();
            var activities = await _context.Tasks.ToListAsync();
            foreach (var act in activities)
            {
                if (act.EndDate > span2)
                {
                    span2 = act.EndDate;
                }
            }
            foreach (var act in activities)
            {
                if (act.StartDate < span1)
                {
                    span1 = act.StartDate;
                }
            }
            for (DateTime s = span1; s < span2; s = s.AddDays(1))
            {
                costs.Add(0);
            }
            var resources = await _context.Resources.ToListAsync();
            foreach (var rs in resources)
            {
                var act = await _context.Tasks.FindAsync(activities.FirstOrDefault(e => e.Text == rs.TaskName).Id);//id task
                DateTime s = act.StartDate;
                DateTime e = act.EndDate;
                for (DateTime o = s; o < e; o = o.AddDays(1))
                {
                    costs[(int)((o - span1).Days)] += rs.CostPerDay;
                }
            }
            ////////////////
            //DateTime a = DateTime.Now;
            List<DateTime> Dates = new List<DateTime>();
            List<double> Accumlative = new List<double>();
            List<double> MonthsAccumlative = new List<double>();
            List<string> months = new List<string>();
            List<int> monthcost = new List<int>();
            double value = 0.0d;
            double valuemonth = 0.0d;
            for (int i = 0; i < costs.Count; i++)
            {
                Dates.Add(span1);
                Dates[i] = span1.AddDays(i);
                if (!months.Contains(Dates[i].ToString("yyyy MMMM")))
                {
                    months.Add(Dates[i].ToString("yyyy MMMM"));
                }
                for (int y = 0; y < i + 1; y++)
                {
                    value += costs[y];///costs 

                }
                Accumlative.Add(value);
                value = 0.0d;


            }
            for (int i = 0; i < months.Count; i++) { monthcost.Add(0); }
            for (int i = 0; i < months.Count; i++)
            {
                for (int y = 0; y < Dates.Count; y++)
                {
                    if (Dates[y].ToString("yyyy MMMM") == months[i])
                    {
                        monthcost[i] += (int)costs[y];
                    }

                }
            }
            for (int i = 0; i < monthcost.Count; i++)
            {
                for (int y = 0; y < i + 1; y++)
                {
                    valuemonth += monthcost[y];///costs 

                }
                MonthsAccumlative.Add(valuemonth);
                valuemonth = 0.0d;
            }


            ///////////////////monthly
            DataTable ChartDatamonth = new DataTable();
            ChartDatamonth.Columns.Add("Days", typeof(System.String));
            ChartDatamonth.Columns.Add(" ", typeof(System.Double));
            for (int i = 0; i < months.Count; i++)
            {
                ChartDatamonth.Rows.Add(months[i], MonthsAccumlative[i]);
            }

            // Create static source with this data table
            StaticSource source1 = new StaticSource(ChartDatamonth);
            // Create instance of DataModel class
            DataModel model1 = new DataModel();
            // Add DataSource to the DataModel
            model1.DataSources.Add(source1);
            // Instantiate Column Chart
            Charts.LineChart column1 = new Charts.LineChart("first_chart");

            // Set Chart's width and height

            column1.Width.Pixel(600);
            column1.Height.Pixel(600);
            // Set DataModel instance as the data source of the chart
            column1.Data.Source = model1;
            // Set Chart Title
            column1.Caption.Text = "Graphical Representation Of Cash Flow(monthly)";
            // Sset chart sub title
            //column.SubCaption.Text = "2017-2018";

            // set XAxis Text
            column1.XAxis.Text = "months";
            // Set YAxis title
            column1.YAxis.Text = "Accumlative Costs";
            column1.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ViewData["Chart1"] = column1.Render();
            //////////////////
            /////////////
            ///
            ///////////////////////daily
            ///////////////////////////
            DataTable ChartData = new DataTable();
            ChartData.Columns.Add("Days", typeof(System.String));
            ChartData.Columns.Add(" ", typeof(System.Double));
            for (int i = 0; i < Dates.Count; i++)
            {
                ChartData.Rows.Add(Dates[i], Accumlative[i]);
            }

            // Create static source with this data table
            StaticSource source = new StaticSource(ChartData);
            // Create instance of DataModel class
            DataModel model = new DataModel();
            // Add DataSource to the DataModel
            model.DataSources.Add(source);
            // Instantiate Column Chart
            Charts.LineChart column = new Charts.LineChart("second_chart");

            // Set Chart's width and height

            column.Width.Pixel(600);
            column.Height.Pixel(600);
            // Set DataModel instance as the data source of the chart
            column.Data.Source = model;
            // Set Chart Title
            column.Caption.Text = "Graphical Representation Of Cash Flow (daily)";
            // Sset chart sub title
            //column.SubCaption.Text = "2017-2018";

            // set XAxis Text
            column.XAxis.Text = "Days";
            // Set YAxis title
            column.YAxis.Text = "Accumlative Costs";
            column.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ViewData["Chart"] = column.Render();
            ViewBag.L = "Ahmed";
            ViewBag.acc = Accumlative;
            ViewBag.span1 = span1;
            return View(costs);
        }
        public async Task<IActionResult> ResourceProfile(string Name)
        {

            DateTime span1 = DateTime.MaxValue;
            DateTime span2 = DateTime.MinValue;
            List<double> units = new List<double>();
            var activities = await _context.Tasks.ToListAsync();
            foreach (var act in activities)
            {
                if (act.EndDate> span2)
                {
                    span2 = act.EndDate;
                }
            }
            foreach (var act in activities)
            {
                if (act.StartDate < span1)
                {
                    span1 = act.StartDate;
                }
            }
            for (DateTime s = span1; s < span2; s = s.AddDays(1))
            {
                units.Add(0);
            }
          
            var resources = await _context.Resources.Where(n => n.Title == Name).ToListAsync();
            foreach (var rs in resources)
            {
                var act = await _context.Tasks.FindAsync(activities.FirstOrDefault(e => e.Text == rs.TaskName).Id);
                DateTime s = act.StartDate;
                DateTime e = act.EndDate;
                for (DateTime o = s; o < e; o = o.AddDays(1))
                {
                    units[(int)((o - span1).Days)] += rs.UnitsPerDay;
                }
                
            }
            ////////////////
            //DateTime a = DateTime.Now;
            List<DateTime> Dates = new List<DateTime>();
            DateTime span11 = span1;
            for (int i = 0; i < units.Count; i++)
            {
                Dates.Add(span11);
                Dates[i] = span11.AddDays(i);
                span11.AddDays(1);
            }
            DataTable ChartData = new DataTable();
            ChartData.Columns.Add("Bars", typeof(System.Double));
            ChartData.Columns.Add("Days", typeof(System.String));



            for (int i = 0; i < Dates.Count; i++)
            {
                ChartData.Rows.Add(units[i], Dates[i]);
            }

            // Create static source with this data table
            StaticSource source = new StaticSource(ChartData);
            // Create instance of DataModel class
            DataModel model = new DataModel();
            // Add DataSource to the DataModel
            model.DataSources.Add(source);
            // Instantiate Column Chart
            Charts.BarChart column = new Charts.BarChart("second_chart");

            // Set Chart's width and height

            column.Width.Pixel(700);
            column.Height.Pixel(700);
            // Set DataModel instance as the data source of the chart
            column.Data.Source = model;
            // Set Chart Title
            column.Caption.Text = "Graphical Representation Resource Usage";
            // Sset chart sub title
            //column.SubCaption.Text = "2017-2018";

            // set XAxis Text
            column.XAxis.Text = "Days";
            // Set YAxis title
            column.YAxis.Text = " Consumed Units Per Day";
            column.ThemeName = FusionChartsTheme.ThemeName.FUSION;

            ViewData["Chart2"] = column.Render();
            var resourcess = await _context.Resources.ToListAsync();
            List<string> ResourcesNames = new List<string>();
            foreach (var i in resources) { ResourcesNames.Add(i.Title); };
            List<SelectListItem> ResourcesNamesList = new List<SelectListItem>();
            ResourcesNamesList.Add(new SelectListItem() { Text = "", Value = "" });
            foreach (var i in resourcess)
            {
                SelectListItem item = new SelectListItem() { Text = i.Title, Value = i.Title };

                bool IsExist = false;
                foreach (var ii in ResourcesNamesList)
                {
                    if (ii.Value == item.Value) { IsExist = true; };
                }
                if (!IsExist)
                {
                    ResourcesNamesList.Add(item);
                }

            }
            ViewBag.options = ResourcesNamesList;
            ViewBag.resname = Name;
            return View(units);

        }
    }
}
