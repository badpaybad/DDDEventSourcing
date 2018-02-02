using System;
using System.Data.Entity;
using DomainDrivenDesign.Core.Implements;
using DomainDrivenDesign.Domain.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainDrivenDesign.TestDomain
{
    [TestClass]
    public class CheckinEventHandleTest
    {
        public CheckinEventHandleTest()
        {
            new CreateDatabaseIfNotExists<TestDbContext>().InitializeDatabase(new TestDbContext());
        }

        CheckinEventHandles evtHandle=new CheckinEventHandles();

        [TestMethod]
        public void CreateCheckin()
        {
            evtHandle.Handle(new CheckinCreated(Guid.NewGuid(), 0,DateTime.Now,10,0,DateTime.Now));
        }
    }
}