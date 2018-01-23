using System;
using System.Collections.Generic;
using DomainDrivenDesign.Core.Events;

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
            _changes.Add(e);
        }

        public void LoadFromHistory(IList<BaseEvent> eventsHistory)
        {
            foreach (var @event in eventsHistory)
            {
                ((dynamic)this).Apply((dynamic)@event);
            }
        }
    }
}