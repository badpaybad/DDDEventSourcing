using System;

namespace DomainDrivenDesign.Core.EventSourcingRepository
{
    public interface ICqrsEventSourcingRepository<TAggregate> where TAggregate : AggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <returns></returns>
        TAggregate Get(object aggregateId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregate"></param>
        /// <param name="expectedVersion">-1: automatic get lastest version</param>
        void Save(TAggregate aggregate, int expectedVersion = -1);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregate"></param>
        void CreateNew(TAggregate aggregate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aggregateId"></param>
        /// <param name="aggregateDoActionsBeforeSave"></param>
        /// <param name="expectedVersion">-1: automatic get lastest version</param>
        void GetDoSave(object aggregateId, Action<TAggregate> aggregateDoActionsBeforeSave, int expectedVersion = -1);
    }
}