using System.Linq;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Core.Implements;
using DomainDrivenDesign.Core.Implements.Model;
using DomainDrivenDesign.Domain.Events;

namespace DomainDrivenDesign.Domain.AutoNumber.Events
{
    public class AutoNumberTestEventHandles : IEventHandle<AutoNumberTestCreated>,
          IEventHandle<AutoNumberTestChangedName>
    {
        public void Handle(AutoNumberTestCreated e)
        {
            using (var db = new TestDbContext())
            {
                db.AutoNumberTests.Add(new AutoNumberTest() { Id = e.Id, Name = e.Name });
                db.SaveChanges();
            }
        }

        public void Handle(AutoNumberTestChangedName e)
        {
            var id = int.Parse(e.Id);
            using (var db = new TestDbContext())
            {
                var temp = db.AutoNumberTests.FirstOrDefault(i => i.Id == id);
                if (temp == null) return;
                temp.Name = e.NewName;
                db.SaveChanges();
            }
        }
    }
}
