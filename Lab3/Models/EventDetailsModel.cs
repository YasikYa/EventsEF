using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3.Models
{
    public class EventDetailsModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string City { get; set; }

        public string Agenda { get; set; }

        public IEnumerable<string> Technologies { get; set; }

        public IEnumerable<ParticipantDetailModel> Participants { get; set; }
    }
}
