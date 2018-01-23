namespace DomainDrivenDesign.Core.Commands
{
    public interface ICommandHandle<T> where T : ICommand
    {
        void Handle(T c);
    }
}