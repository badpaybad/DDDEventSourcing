using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.Domain.AutoNumber.Events
{
    public class AutoNumberTestCreated : IEvent
    {
        public readonly int Id;
        public readonly string Name;

        public AutoNumberTestCreated(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Version { get; set; }
    }
}