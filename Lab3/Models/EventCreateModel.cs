using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Models
{
    public class EventCreateModel
    {
        public string Title { get; set; }

        public string Agenda { get; set; }

        public Guid LocationId { get; set; }

        public IEnumerable<Guid> TechnologiesIds { get; set; }
    }
}
