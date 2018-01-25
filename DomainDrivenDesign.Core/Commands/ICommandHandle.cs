using DomainDrivenDesign.Core.Reflection;

namespace DomainDrivenDesign.Core.Commands
{
    public interface ICommandHandle<T> : ICqrsHandle where T : ICommand
    {
        void Handle(T c);
    }
}