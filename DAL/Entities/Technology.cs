using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Entities
{
    public class Technology
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
