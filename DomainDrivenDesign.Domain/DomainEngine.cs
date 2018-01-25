using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Hris;
using DomainDrivenDesign.Domain.Commands;
using DomainDrivenDesign.Domain.Events;

namespace DomainDrivenDesign.Domain
{
    public static class DomainEngine
    {
     
        public static void Boot()
        {
            //should consider this
            new CreateDatabaseIfNotExists<HrisDbContext>().InitializeDatabase(new HrisDbContext());

            CheckinEventHandle checkinEventHandle=new CheckinEventHandle();
            HrisMemoryMessageBuss.RegisterEvent<CheckinCreated>(checkinEventHandle.Handle);
            HrisMemoryMessageBuss.RegisterEvent<CheckinCommentCommented>(checkinEventHandle.Handle);
            HrisMemoryMessageBuss.RegisterEvent<CheckinCompleted>(checkinEventHandle.Handle);
            
        }
    }
}
