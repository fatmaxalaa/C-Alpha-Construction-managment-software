using Microsoft.AspNetCore.Mvc;
using Resources.Models;
using System.Linq;

namespace Resources.Controllers
{
    [Produces("application/json")]
    [Route("api/data")]
    public class DataController : Controller
    {

        private readonly AppDBContext _context;
        public DataController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public object Get()
        {
            return new
            {
                data = _context.Tasks.OrderBy(t => t.SortOrder).ToList().Select(t => (WebApiTask)t),
                links = _context.Links.ToList().Select(l => (WebApiLink)l)

            };
        }

        public IActionResult Index()
        {
            return View();
        }



    }
}
