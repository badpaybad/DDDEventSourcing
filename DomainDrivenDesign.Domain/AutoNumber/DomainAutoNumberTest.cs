using System;
using DomainDrivenDesign.Core;
using DomainDrivenDesign.Domain.AutoNumber.Events;

namespace DomainDrivenDesign.Domain.AutoNumber
{
   public class DomainAutoNumberTest: AggregateRoot
    {
        public override string Id { get; set; }
        private string _name;

        public DomainAutoNumberTest()
        {
        }

        void Apply(AutoNumberTestCreated e)
        {
            Id = e.Id.ToString();
            _name = e.Name;
        }


        void Apply(AutoNumberTestChangedName e)
        {
            Id = e.Id.ToString();
            _name = e.NewName;

        }


        public DomainAutoNumberTest(int id, string Name)
        {
            ApplyChange(new AutoNumberTestCreated(id,Name));
        }

        public void ChangeName(string newName)
        {
            if (newName.IndexOf("fuck", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                throw new Exception("Invalid name");
            }

            ApplyChange(new AutoNumberTestChangedName(Id,newName));
        }
    }
}
