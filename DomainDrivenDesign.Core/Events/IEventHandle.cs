using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Reflection;

namespace DomainDrivenDesign.Core.Events
{
    public interface IEventHandle<T>: ICqrsHandle where T:IEvent
    {
        void Handle(T e);
    }
}
