using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Entities;
using Lab3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Lab3.Controllers
{
    public class EventController : Controller
    {
        private EventContext _context;

        public EventController(EventContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var events = _context.Events.Select(e => new EventModel
            {
                City = e.Location.City,
                Technologies = e.Technologies.Select(t => t.Name),
                Title = e.Title,
                Id = e.Id
            }).ToList();
            return View(events);
        }

        public IActionResult Delete(Guid id)
        {
            var eventToDelete = _context.Events.Where(e => e.Id == id).FirstOrDefault();
            _context.Events.Remove(eventToDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(Guid id)
        {
            var eventDetails = _context.Events.Where(e => e.Id == id).Select(e => new EventDetailsModel
            {
                Id = e.Id,
                Agenda = e.Agenda,
                City = e.Location.City,
                Technologies = e.Technologies.Select(t => t.Name),
                Title = e.Title,
                Participants = e.Participants.Select(p => new ParticipantDetailModel
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = p.Age,
                    Id = p.Id
                })
            }).FirstOrDefault();

            return View(eventDetails);
        }

        public IActionResult Create()
        {
            AddLocationDropDown();
            AddTechnologySelect();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EventCreateModel eventCreate)
        {
            var createEvent = new Event()
            {
                Id = Guid.NewGuid(),
                Agenda = eventCreate.Agenda,
                Title = eventCreate.Title,
                Location = _context.Locations.Where(l => l.Id == eventCreate.LocationId).FirstOrDefault(),
                Technologies = _context.Technologies.Where(t => eventCreate.TechnologiesIds.Any(technologyId => technologyId == t.Id)).ToList()
            };
            _context.Events.Add(createEvent);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult AddParticipants(Guid eventId, string eventName)
        {
            ViewBag.eventId = eventId;
            ViewBag.eventName = eventName;
            AddParticipantSelect(eventId);
            return View();
        }

        [HttpPost]
        public IActionResult AddParticipants(Guid eventId, Guid[] participantIds)
        {
            var participants = _context.Participants.Where(p => participantIds.Any(pid => pid == p.Id)).ToList();
            var eventToAdd = _context.Events.Include(e => e.Participants).Where(e => e.Id == eventId).FirstOrDefault();
            participants.ForEach(p => eventToAdd.Participants.Add(p));
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = eventId });
        }

        public IActionResult RemoveParticipant(Guid eventId, Guid participantId)
        {
            var eventToRemove = _context.Events.Include(e => e.Participants).Where(e => e.Id == eventId).FirstOrDefault();
            var participantToRemove = eventToRemove.Participants.Where(p => p.Id == participantId).FirstOrDefault();
            eventToRemove.Participants.Remove(participantToRemove);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = eventId });
        }

        private void AddLocationDropDown()
        {
            ViewBag.Location = _context.Locations.ToList().Select(l => new SelectListItem { Text = l.City, Value = l.Id.ToString()});
        }

        private void AddTechnologySelect()
        {
            ViewBag.Technologies = new MultiSelectList(_context.Technologies.ToList().Select(t => new { TechnologyId = t.Id.ToString(), TechnologyName = t.Name}), "TechnologyId", "TechnologyName");
        }

        private void AddParticipantSelect(Guid eventId)
        {
            ViewBag.Participants = new MultiSelectList(_context.Participants.Where(p => p.Events.All(e => e.Id != eventId))
                .ToList().Select(t => new { ParticipantId = t.Id.ToString(), ParticipantName = $"{t.FirstName} {t.LastName}" }), "ParticipantId", "ParticipantName");
        }
    }
}
