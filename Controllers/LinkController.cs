using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Resources.Models;
using System.Collections.Generic;
using System.Linq;


namespace Resources.Controllers
{
    [Produces("application/json")]
    [Route("api/link")]
    public class LinkController : Controller
    {


        private readonly AppDBContext _context;
        public LinkController(AppDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IEnumerable<WebApiLink> Get()
        {
            return _context.Links
                .ToList()
                .Select(t => (WebApiLink)t);
        }

        [HttpGet("{id}")]
        public Link? Get(int id)
        {
            return _context
                .Links
                .Find(id);
        }


        [HttpPost]
        public ObjectResult Post(WebApiLink apiLink)
        {
            var newLink = (Link)apiLink;

            _context.Links.Add(newLink);
            _context.SaveChanges();

            return Ok(new
            {
                tid = newLink.Id,
                action = "inserted"
            });
        }

        [HttpPut("{id}")]
        public ObjectResult Put(int id, WebApiLink apiLink)
        {
            var updatedLink = (Link)apiLink;
            updatedLink.Id = id;
            _context.Entry(updatedLink).State = EntityState.Modified;


            _context.SaveChanges();

            return Ok(new
            {
                action = "updated"
            });
        }


        [HttpDelete("{id}")]
        public ObjectResult DeleteLink(int id)
        {
            var Link = _context.Links.Find(id);
            if (Link != null)
            {
                _context.Links.Remove(Link);
                _context.SaveChanges();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }



    }
}
