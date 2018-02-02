namespace DomainDrivenDesign.Core.Events
{
    public interface IEvent
    {
        int Version { get; set; }      
    }
}