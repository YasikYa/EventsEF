using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    public class TechnologyController : Controller
    {
        private EventContext _context;

        public TechnologyController(EventContext context) => _context = context;

        public IActionResult Create(string redirectUrl)
        {
            ViewBag.redirectUrl = redirectUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Create(string name, string redirectUrl)
        {
            _context.Technologies.Add(new Technology { Id = Guid.NewGuid(), Name = name });
            _context.SaveChanges();
            return Redirect(redirectUrl);
        }
    }
}
