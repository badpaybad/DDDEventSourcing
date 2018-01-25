using System;
using System.Collections.Generic;
using DomainDrivenDesign.Core.Events;
using DomainDrivenDesign.Core.Reflection;

namespace DomainDrivenDesign.Core
{
    public abstract class AggregateRoot
    {
        
        public Guid Id { get; set; }

        private readonly IList<BaseEvent> _changes = new List<BaseEvent>();

        public IList<BaseEvent> Changes
        {
            get { return _changes; }
        }

        protected void ApplyChange(BaseEvent e)
        {
            this.AsDynamic().Apply(e);
            _changes.Add(e);
        }

        public void LoadFromHistory(IList<BaseEvent> eventsHistory)
        {
            foreach (var e in eventsHistory)
            {
                //((dynamic)this).Apply((dynamic)@event);
                this.AsDynamic().Apply(e);
            }
        }
    }
}