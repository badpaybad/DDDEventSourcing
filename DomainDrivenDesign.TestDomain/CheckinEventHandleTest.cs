using System;
using System.Data.Entity;
using DomainDrivenDesign.Core.Hris;
using DomainDrivenDesign.Domain.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainDrivenDesign.TestDomain
{
    [TestClass]
    public class CheckinEventHandleTest
    {
        public CheckinEventHandleTest()
        {
            new CreateDatabaseIfNotExists<HrisDbContext>().InitializeDatabase(new HrisDbContext());
        }

        CheckinEventHandle evtHandle=new CheckinEventHandle();

        [TestMethod]
        public void CreateCheckin()
        {
            evtHandle.Handle(new CheckinCreated(Guid.NewGuid(), 0,DateTime.Now,10,0,DateTime.Now));
        }
    }
}