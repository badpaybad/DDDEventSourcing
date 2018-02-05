using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Implements;
using DomainDrivenDesign.Domain.Commands;
using DomainDrivenDesign.Domain.Events;

namespace DomainDrivenDesign.Domain
{
    public static class DomainEngine
    {

        public static void Boot()
        {
            //should consider this
            new CreateDatabaseIfNotExists<TestDbContext>().InitializeDatabase(new TestDbContext());

            //if dont want manual regist = code
            //can use reflection to load dll from folder Bin eg:
            MemoryMessageBuss.AutoRegisterExecutingAssembly();

            //manual register command handle and event handle
            //EventHandleRegister();
            //CommandHandleRegister();
        }

        private static void CommandHandleRegister()
        {
            CheckinCommandHandles checkinCommandHandle = new CheckinCommandHandles();
            MemoryMessageBuss.RegisterCommand<CreateCheckin>(checkinCommandHandle.Handle);
            MemoryMessageBuss.RegisterCommand<CommentCheckinByEmployee>(checkinCommandHandle.Handle);
            MemoryMessageBuss.RegisterCommand<CommentCheckinByEvaluator>(checkinCommandHandle.Handle);
        }

        private static void EventHandleRegister()
        {
            CheckinEventHandles checkinEventHandle = new CheckinEventHandles();
            MemoryMessageBuss.RegisterEvent<CheckinCreated>(checkinEventHandle.Handle);
            MemoryMessageBuss.RegisterEvent<CheckinCommentCommented>(checkinEventHandle.Handle);
            MemoryMessageBuss.RegisterEvent<CheckinCompleted>(checkinEventHandle.Handle);
        }
    }
}
