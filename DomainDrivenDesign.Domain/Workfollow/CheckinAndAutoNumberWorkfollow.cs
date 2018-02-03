using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Core.Implements;
using DomainDrivenDesign.Domain.AutoNumber.Commands;
using DomainDrivenDesign.Domain.Events;

namespace DomainDrivenDesign.Domain.Workfollow
{
    /// <summary>
    /// scenario if Checkin created, so should create AutoNumber
    /// </summary>
   public class CheckinAndAutoNumberWorkfollow:IEventHandle<CheckinCreated>
    {
        public void Handle(CheckinCreated e)
        {
            if (e.Duration >= 0)
            {
                MemoryMessageBuss.PushCommand(new CreateAutoNumberTest($"Checkin created with Id: {e.Id} duration: {e.Duration}"));
            }
        }
    }
}
