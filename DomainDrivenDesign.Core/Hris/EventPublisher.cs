using System.CodeDom;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.Core.Hris
{
    public  class EventPublisher:IEventPublisher
    {
        public void Publish(IEvent e)
        {
            HrisMemoryMessageBuss.Push(e);
        }
    }
}
