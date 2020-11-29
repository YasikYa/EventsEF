using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options) : base(options) { }

        public DbSet<Technology> Technologies { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Participant> Participants { get; set; }

        public DbSet<Event> Events { get; set; }
    }
}
