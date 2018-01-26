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
            var executingAssembly = Assembly.GetExecutingAssembly();
            List<string> allAssemblies = new List<string>();

            string path = Path.GetDirectoryName(executingAssembly.Location);

            foreach (string dll in Directory.GetFiles(path, "*.dll"))
                allAssemblies.Add(dll);


            foreach (var assembly in allAssemblies)
            {
                MemoryMessageBuss.RegisterAssembly(assembly);
            }


            //EventHandleRegister();

            //CommandHandleRegister();
        }

        private static void CommandHandleRegister()
        {
            CheckinCommandHandle checkinCommandHandle = new CheckinCommandHandle();
            MemoryMessageBuss.RegisterCommand<CreateCheckin>(checkinCommandHandle.Handle);
            MemoryMessageBuss.RegisterCommand<CommentCheckinByEmployee>(checkinCommandHandle.Handle);
            MemoryMessageBuss.RegisterCommand<CommentCheckinByEvaluator>(checkinCommandHandle.Handle);
        }

        private static void EventHandleRegister()
        {
            CheckinEventHandle checkinEventHandle = new CheckinEventHandle();
            MemoryMessageBuss.RegisterEvent<CheckinCreated>(checkinEventHandle.Handle);
            MemoryMessageBuss.RegisterEvent<CheckinCommentCommented>(checkinEventHandle.Handle);
            MemoryMessageBuss.RegisterEvent<CheckinCompleted>(checkinEventHandle.Handle);
        }
    }
}
