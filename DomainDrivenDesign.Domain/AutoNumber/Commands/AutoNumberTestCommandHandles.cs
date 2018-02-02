using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Commands;
using DomainDrivenDesign.Core.EventSourcingRepository;
using DomainDrivenDesign.Core.Implements;
using DomainDrivenDesign.Domain.AutoNumber.Commands;

namespace DomainDrivenDesign.Domain.AutoNumber.Commands
{
    public class AutoNumberTestCommandHandles : ICommandHandle<CreateAutoNumberTest>,
         ICommandHandle<ChangeNameOfAutoNumberTest>
    {
        ICqrsEventSourcingRepository<DomainAutoNumberTest> _repository
            = new CqrsEventSourcingRepository<DomainAutoNumberTest>(new EventPublisher());

        /// <summary>
        /// should find better way to get latest auto number
        /// or consider find latest auto number from UI
        /// </summary>
        /// <param name="c"></param>
        public void Handle(CreateAutoNumberTest c)
        {
            //todo: should find better way to get latest auto number
            //or consider find latest auto number from UI
            var lastestId = 0;
            using (var db = new TestDbContext())
            {
                lastestId = db.AutoNumberTests.Select(i => i.Id).OrderByDescending(i=>i).FirstOrDefault();
            }
            var id = lastestId + 1;
            _repository.CreateNew(new DomainAutoNumberTest(id, c.Name));
        }

        public void Handle(ChangeNameOfAutoNumberTest c)
        {
            var obj = _repository.Get(c.Id);
            obj.ChangeName(c.NewName);
            _repository.Save(obj);
        }
    }
}
