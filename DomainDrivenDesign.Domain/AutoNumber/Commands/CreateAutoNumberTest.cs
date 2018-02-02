using DomainDrivenDesign.Core.Commands;

namespace DomainDrivenDesign.Domain.AutoNumber.Commands
{
    public class CreateAutoNumberTest : ICommand
    {
        public readonly string Name;

        public CreateAutoNumberTest(string name)
        {
            Name = name;
        }
    }
}
