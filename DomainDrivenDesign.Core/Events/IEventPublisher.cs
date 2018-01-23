namespace DomainDrivenDesign.Core.Events
{
    public interface IEventPublisher
    {
        void Publish(IEvent e);
    }
}