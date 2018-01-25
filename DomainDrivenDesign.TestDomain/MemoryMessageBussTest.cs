using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Implements;
using DomainDrivenDesign.Domain;
using DomainDrivenDesign.Domain.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainDrivenDesign.TestDomain
{
    [TestClass]
    public class MemoryMessageBussTest
    {
        public MemoryMessageBussTest()
        {
            DomainEngine.Boot();
        }

        [TestMethod]
        public void CreateCheckin()
        {
            var createCheckin = new CreateCheckin(Guid.NewGuid(), 0, 10, DateTime.Now);

            MemoryMessageBuss.PushCommand(createCheckin);

            MemoryMessageBuss.PushCommand(new CommentCheckinByEmployee(createCheckin.CheckinId,"Hi boss",0));
            MemoryMessageBuss.PushCommand(new CommentCheckinByEvaluator(createCheckin.CheckinId,"Hi man, we should change 'boss' to 'bro' ",0));

        }
    }
}
