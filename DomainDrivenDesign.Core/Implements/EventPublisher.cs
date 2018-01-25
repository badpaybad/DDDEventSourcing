using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.Core.Implements
{
    public  class EventPublisher:IEventPublisher
    {
        public void Publish(IEvent e)
        {
            MemoryMessageBuss.Push(e);
        }
    }
}
