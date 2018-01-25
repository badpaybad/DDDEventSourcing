using System;
using System.Collections.Generic;
using System.Data.Entity;
using DomainDrivenDesign.Core.Commands;
using DomainDrivenDesign.Core.Events;

namespace DomainDrivenDesign.Core.Hris
{
    public static class HrisMemoryMessageBuss
    {

        static readonly Dictionary<Type, List<Action<IEvent>>> _eventHandler = new Dictionary<Type, List<Action<IEvent>>>();
        static readonly Dictionary<Type, Action<ICommand>> _commandHandler = new Dictionary<Type, Action<ICommand>>();

        static object _eventLocker = new object();
        static object _commandLocker = new object();


        static HrisMemoryMessageBuss()
        {
           
        }


        public static void RegisterEvent<T>(Action<T> handle) where T : IEvent
        {
            lock (_eventLocker)
            {
                var t = typeof(T);

                List<Action<IEvent>> ax;

                if (_eventHandler.TryGetValue(t, out ax))
                {
                    ax.Add(p => handle((T)p));
                }
                else
                {
                    ax = new List<Action<IEvent>>() { p => handle((T)p) };
                }

                _eventHandler[t] = ax;
            }
        }

        internal static void Push(IEvent e)
        {
            var t = e.GetType();
            List<Action<IEvent>> listAction;
            lock (_eventLocker)
            {
                if (!_eventHandler.TryGetValue(t, out listAction) || listAction == null || listAction.Count == 0)
                {
                    throw new EntryPointNotFoundException($"Not found type: {t}");
                }
            }

            foreach (var a in listAction)
            {
                a(e);
            }
        }



        public static void RegisterCommand<T>(Action<T> handle) where T : ICommand
        {
            lock (_commandLocker)
            {
                var t = typeof(T);

                Action<ICommand> ax;

                if (_commandHandler.TryGetValue(t, out ax))
                {
                    throw new Exception($"Should only one handle to cover type: {t}");
                }

                _commandHandler[t] = (p)=> handle((T)p);
            }
        }

        internal static void Push(ICommand e)
        {
            var t = e.GetType();
            Action<ICommand> a;
            lock (_commandLocker)
            {
                if (!_commandHandler.TryGetValue(t, out a) || a == null )
                {
                    throw new EntryPointNotFoundException($"Not found type: {t}");
                }
            }
            
            a(e);
          
        }


    }
}