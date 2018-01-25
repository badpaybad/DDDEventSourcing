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
    public class HrisMemoryMessageBussTest
    {
        public HrisMemoryMessageBussTest()
        {
            DomainEngine.Boot();
        }

        [TestMethod]
        public void CreateCheckin()
        {
            var createCheckin = new CreateCheckin(Guid.NewGuid(), 0, 10, DateTime.Now);

            HrisMemoryMessageBuss.PushCommand(createCheckin);

            HrisMemoryMessageBuss.PushCommand(new CommentCheckinByEmployee(createCheckin.CheckinId,"Hi boss",0));
            HrisMemoryMessageBuss.PushCommand(new CommentCheckinByEvaluator(createCheckin.CheckinId,"Hi man, we should change 'boss' to 'bro' ",0));

        }
    }
}
