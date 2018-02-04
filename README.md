# Domain driven design and EventSourcing
DDD EventSourcing

 - .Net framework 4.6.1
 - Entity framework 6.0.0
 - Newtonjson 10.0.3
 - System.Data.SqlClient 4.4.2
 - MsSql 2008 or later
 
 # Guider
 - Clone and open with visual studio 2017
 - Rebuild all solution to restore nuget package
 - because I use IDatabaseInitializer (.net entity framework) so that Should care any change for Entity of DbContext.
 In my way I just delete table __MigrationHistory and do manual change for each table : ))
 
 # Thoery coding
 UI -> people Read and think -> people Do, action ... something -> UI push Command(s) -> Command Handle -> Build Object Domain by Events using Repository -> Domain call action(s) do business -> Repository store Events into EventSourcing Db -> Fire Event -> Event Handle write to Database Read -> Thin Query Facade from Db read -> UI ...
 
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
 - 
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

# DDD reference
- https://github.com/heynickc/awesome-ddd
- https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs

# Patterns always good but business analytic and design should be good first
 Should try to learn DDD. Careful to create correct Domain and its actions, events. Because Events are history and they should not be remove or change. 
