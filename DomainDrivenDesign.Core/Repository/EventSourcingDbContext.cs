using System.Data.Entity;
using DomainDrivenDesign.Core.Ef;

namespace DomainDrivenDesign.Core.Repository
{
    internal class EventSourcingDbContext : BaseDbContext
    {
        public EventSourcingDbContext() : base("EventSourcing")
        {
        }

        public DbSet<EventSourcingDescription> EventSoucings { get; set; }
    }
}