using System;
using System.Collections.Generic;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Core.Reflection;

namespace DomainDrivenDesign.Core
{
    public abstract class AggregateRoot
    {

        public abstract string Id { get; set; }

        private readonly IList<IEvent> _changes = new List<IEvent>();
        //private readonly Dictionary<Type, Action<IEvent>> _applyChanges = new Dictionary<Type, Action<IEvent>>();

        public IList<IEvent> Changes
        {
            get { return _changes; }
        }

        protected void ApplyChange(IEvent e)
        {
            this.AsDynamic().Apply(e);
            _changes.Add(e);
        }

        /// <summary>
        /// Each Domain inherit from aggregateroot MUST implement private void Apply(IEvent e)
        /// 
        /// </summary>
        /// <param name="eventsHistory"></param>
        public void LoadFromHistory(IList<IEvent> eventsHistory)
        {
            foreach (var e in eventsHistory)
            {
                 this.AsDynamic().Apply(e);

                ////((dynamic)this).Apply((dynamic)@event);
                //Action<IEvent> apply;
                //if (_applyChanges.TryGetValue(e.GetType(), out apply))
                //{
                //     apply(e);
                //}
                //else
                //{
                //    throw new NotImplementedException($"Must register function to apply event.");
                //}
            }
        }
    }
}