using DomainDrivenDesign.Core.Commands;

namespace DomainDrivenDesign.Domain.AutoNumber.Commands
{
    public class ChangeNameOfAutoNumberTest : ICommand
    {
        public readonly int Id;
        public readonly string NewName;

        public ChangeNameOfAutoNumberTest(int id, string newName)
        {
            Id = id;
            NewName = newName;
        }
    }
}