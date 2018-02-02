using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.Domain.AutoNumber.Events
{
    public class AutoNumberTestChangedName : IEvent
    {
        public readonly string Id;
        public readonly string NewName;

        public AutoNumberTestChangedName(string id, string newName)
        {
            Id = id;
            NewName = newName;
        }

        public int Version { get; set; }
    }
}