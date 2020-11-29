using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Participant
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
