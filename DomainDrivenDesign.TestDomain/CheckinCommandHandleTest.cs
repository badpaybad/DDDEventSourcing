using System;
using System.Data.Entity;
using System.Linq;
using DomainDrivenDesign.Core.Hris;
using DomainDrivenDesign.Domain;
using DomainDrivenDesign.Domain.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainDrivenDesign.TestDomain
{
    [TestClass]
    public class CheckinCommandHandleTest
    {
        private CheckinCommandHandle cmdHandle = new CheckinCommandHandle();

        public CheckinCommandHandleTest()
        {
            //should consider this
            new CreateDatabaseIfNotExists<HrisDbContext>().InitializeDatabase(new HrisDbContext());

            DomainBooter.Boot();
        }

        [TestMethod]
        public void CreateCheckin()
        {
            cmdHandle.Handle(new CreateCheckin( Guid.NewGuid(), 0,10, DateTime.Now));
        }

        [TestMethod]
        public void EvaluatorComment()
        {
            var checkinId = Guid.Parse("C127A8B7-A3EE-4617-8952-A1F407B1D70E");

            using (var db = new HrisDbContext())
            {
                checkinId = db.CheckinTests.Select(i => i.Id).FirstOrDefault();
            }
            
            cmdHandle.Handle(new CommentCheckinByEvaluator(checkinId,"Hello world temporary",2));
        }
    }
}
