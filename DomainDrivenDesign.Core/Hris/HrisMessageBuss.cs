using System;
using System.Collections.Generic;
using System.Data.Entity;
using DomainDrivenDesign.Core.Commands;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Core.Repository;

namespace DomainDrivenDesign.Core.Hris
{
    public static class HrisMessageBuss
    {

        static readonly Dictionary<Type, List<Action<IEvent>>> _eventHandler = new Dictionary<Type, List<Action<IEvent>>>();

        static object _lock = new object();


        static HrisMessageBuss()
        {
           
        }


        public static void RegisterEvent<T>(Action<T> handle) where T : IEvent
        {
            lock (_lock)
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

        internal static void InvokeEvent(IEvent e)
        {
            var t = e.GetType();
            List<Action<IEvent>> listAction;
            lock (_lock)
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


    }
}