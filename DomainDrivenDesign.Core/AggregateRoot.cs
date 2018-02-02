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

        public IList<IEvent> Changes
        {
            get { return _changes; }
        }

        protected void ApplyChange(IEvent e)
        {
            this.AsDynamic().Apply(e);
            _changes.Add(e);
        }

        public void LoadFromHistory(IList<IEvent> eventsHistory)
        {
            foreach (var e in eventsHistory)
            {
                //((dynamic)this).Apply((dynamic)@event);
                this.AsDynamic().Apply(e);
            }
        }
    }
}