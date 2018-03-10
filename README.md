# Domain driven design and EventSourcing

## The benefit
 - Event sourcing (the history of business domain object) => we can do Machine learning, Deep learning, AI ... to investigate the behaviors.
 - Command and Query: Easy to separate the logic business and logic display. Easy control code, find & change commands, events, logic domain business take less effort.
 - Can use distributed queue or message buss to store command and query => distributed process
 - Can seperate CommandHandle process and EventHandle process (actualy can be difference server), both of them see each other through the queue or message buss
 - Can be more security because of separate server to run CommandHandle, EventHandle and the Frontend (thin query, query facade). Between all of them see each other by the queue or message buss (redis queue, message buss, rabitMQ, or other streaming process system ...)

## DDD EventSourcing

 - .Net framework 4.6.1
 - Entity framework 6.0.0
 - Newtonjson 10.0.3
 - System.Data.SqlClient 4.4.2
 - MsSql 2008 or later ( can use any kind of db, just add provider for Entity framework)
 
 # Guider
 - Clone and open with visual studio 2017
 - Rebuild all solution to restore nuget package
 - because I use IDatabaseInitializer (.net entity framework) so that Should care any change for Entity of DbContext.
 In my way I just delete table __MigrationHistory and do manual change for each table : ))
 
 # Thoery coding
 UI -> people Read and think -> people Do, action ... something -> UI push Command(s) -> Command Handle -> Build Object Domain by Events using Repository -> Domain call action(s) do business -> Repository store Events into EventSourcing Db -> Fire Event -> Event Handle write to Database Read -> Thin Query Facade from Db read -> UI ...
 
 ### Dispatch Commands, Events handling
 - Each type of commands, events will have a function to process named Handle ( classes inherit from ICommandHandle, ICommandEventHandle)
 - If you want distributed can do like this:
 -- Each type (type full name) ơf commands, event will be the queue name. (can use redis queue ...)
 -- When UI push command, command will be enqueue command (seriallize object command into json ) to queue name (its type full name).
 -- Create and console or process or services ... to dequeue and call Handle function

https://github.com/badpaybad/DDDEventSourcing/wiki/Step-to-step-to-implement-code
 
 ## CqrsEventSourcingRepository and EventSourcingDbContext and EventSourcingDescription
 ### EventSourcingDbContext 
 just db access for CqrsEventSourcingRepository
 ### EventSourcingDescription
 Entity (Table in db) to store Events fired from Domain object
 ### CqrsEventSourcingRepository
 Get Events and build them become Object Domain. 
 CQRS require unique Identify for each Domain so that my solution for this Identify is: Type of Domain and Id of Domain. 
 Actually table "EventSourcingDescription" have compose Primary key (Id,AggregateType,Version).
 So that Id of Domain (also Id for table in db read) can be Auto number, Guid ... or any things. 
 
 ## class MemoryMessageBuss
 I just use Memory to fake message buss
 - Implement your own with RabitMQ or AzureServicesBuss or Redis queue ... to full seperate and more security and can distributed.
 - I use Redis as own message buss in this git https://github.com/badpaybad/redis-microservices
 ### DomainDrivenDesign.Core.Implements.MemoryQueue 
 Implement memory queue, use separte thread for real async process command and event 
 
 ## class DomainEngine
 - To register handle to process Event and Command

 ## Domain business (DDD) (should learn how to analytic and design business by DDD)
 eg: class Checkin inherit AggregateRoot as an Domain in this sample.
 - each class for Domain business, inherit AggregateRoot and MUST create private functions Apply to apply Events.
 Why must create private Apply plz Check function LoadFromHistory in class AggregateRoot 
- function LoadFromHistory called at function Get in class CqrsEventSourcingRepository

## ICqrsHandle
Use to reflection to load dynamic file dll and register to MemoryMessageBuss
ICommandHandle , IEventHandle inherit from ICqrsHandle
- Check function DomainEngine.Boot() and find MemoryMessageBuss.RegisterAssembly(assembly);

## Workfollow
Can create separate project. It is simple like that. If some Event fired. Just register (subcribe) and call next command.
This class also inherit IEventHandle but It should NOT directly write to db read. It just decide if an Event fired (mean an action of Domain done) which command should be the next call. eg: check class CheckinAndAutoNumberWorkfollow

## DomainDrivenDesign.Core.Implements
(namespace or folder)
- U can place your thin query facade eg: TestDbContext. And Implement your own EventPublisher
- Do your own code base of your project here

### Developer guide:
- Create domain business: Must inherit AggregateRoot
- Create command handle: Must inherit ICommandHandle
- Create event handle: Must inherit IEventHandle
- eg: check code in folder DomainDrivenDesign.Domain.AutoNumber 

# DDD reference
- https://github.com/heynickc/awesome-ddd
- https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs
- https://github.com/gregoryyoung/m-r
- https://github.com/jbogard/MediatR

# Patterns always good but business analytic and design should be good first
 Should try to learn DDD. Careful to create correct Domain and its actions, events. Because Events are history and they should not be remove or change. 
 
 
 
