using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Models
{
    public class EventModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public IEnumerable<string> Technologies { get; set; }
    }
}
