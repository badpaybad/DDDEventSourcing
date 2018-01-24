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
            HrisMessageBuss.RegisterEvent<CheckinCreated>(checkinEventHandle.Handle);
            HrisMessageBuss.RegisterEvent<CheckinCommentCommented>(checkinEventHandle.Handle);
            HrisMessageBuss.RegisterEvent<CheckinCompleted>(checkinEventHandle.Handle);
            
        }
    }
}
