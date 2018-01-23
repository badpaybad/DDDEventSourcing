using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainDrivenDesign.Core.Events
{
    public interface IEventHandle<T> where T:IEvent
    {
        void Handle(T e);
    }
}
