using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.DomainCheckin.Events
{
   public class CheckinEventHandles:IEventHandle<CheckinCreated>
    {
        public void Handle(CheckinCreated e)
        {
            throw new NotImplementedException();
        }
    }
}
