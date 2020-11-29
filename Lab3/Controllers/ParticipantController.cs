using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers
{
    public class ParticipantController : Controller
    {
        private EventContext _context;

        public ParticipantController(EventContext context) => _context = context;

        public IActionResult Create(string redirectUrl)
        {
            ViewBag.redirectUrl = redirectUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ParticipantCreateModel createModel, string redirectUrl)
        {
            var newParticipant = new Participant
            {
                Age = createModel.Age,
                FirstName = createModel.FirstName,
                LastName = createModel.LastName,
                Id = Guid.NewGuid()
            };
            _context.Participants.Add(newParticipant);
            _context.SaveChanges();
            return Redirect(redirectUrl);
        }
    }
}
