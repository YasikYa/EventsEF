using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Agenda { get; set; }

        public Location Location { get; set; }

        public ICollection<Participant> Participants { get; set; }

        public ICollection<Technology> Technologies { get; set; }
    }
}
