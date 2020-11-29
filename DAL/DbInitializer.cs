using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DbInitializer
    {
        public static void Initialize(EventContext context)
        {
            context.Database.EnsureCreated();

            if (context.Events.Any())
                return;

            var participants = new List<Participant>
            {
                new Participant { Id = Guid.NewGuid(), FirstName = "Denis", LastName = "Denisov", Age = 18 },
                new Participant { Id = Guid.NewGuid(), FirstName = "Ihor", LastName = "Petrov", Age = 31 },
                new Participant { Id = Guid.NewGuid(), FirstName = "Anton", LastName = "Kovaak", Age = 22 },
                new Participant { Id = Guid.NewGuid(), FirstName = "Vladislav", LastName = "Sharpov", Age = 20 },
                new Participant { Id = Guid.NewGuid(), FirstName = "Alexey", LastName = "Sidorov", Age = 20 },
                new Participant { Id = Guid.NewGuid(), FirstName = "Danil", LastName = "Kvyat", Age = 23 }
            };

            context.Participants.AddRange(participants);
            context.SaveChanges();

            var locations = new List<Location>
            {
                new Location {Id = Guid.NewGuid(), City = "Kharkiv"},
                new Location {Id = Guid.NewGuid(), City = "Kyiv"}
            };

            context.Locations.AddRange(locations);
            context.SaveChanges();

            var technologies = new List<Technology>
            {
                new Technology { Id = Guid.NewGuid(), Name = ".NET" },
                new Technology { Id = Guid.NewGuid(), Name = "Azure" }
            };

            context.Technologies.AddRange(technologies);
            context.SaveChanges();

            var newEvent = new Event
            {
                Id = Guid.NewGuid(),
                Location = context.Locations.First(),
                Agenda = "Learn Azure services in .NET system",
                Title = "Azure Intro",
                Participants = context.Participants.ToList(),
                Technologies = context.Technologies.ToList()
            };
            context.Events.Add(newEvent);
            context.SaveChanges();
        }
    }
}
