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
 
 # Thoery
 UI -> people Read and think -> people Do, action ... something -> UI push Command(s) -> Command Handle -> Build Object Domain by Events using Repository -> Domain call action(s) do business -> Repository store Events into EventSourcing Db -> Fire Event -> Event Handle write to Database Read -> Thin Query Facade from Db read -> UI ...
 
 ## class MemoryMessageBuss
 I just use Memory to fake message buss
 - Implement your own with RabitMQ or AzzureServicesBuss or Redis queue ... to full seperate and more security and can distributed.
 - I use Redis as own message buss in this git https://github.com/badpaybad/redis-microservices
 
 ## class DomainEngine
 - To register handle to process Event and Command

 ## class Checkin (DDD)
 class Checkin inherit AggregateRoot as an Domain in this sample.

# ICqrsHandle
Use to reflection to load dynamic file dll and register to MemoryMessageBuss
ICommandHandle , IEventHandle inherit from ICqrsHandle
- Check function DomainEngine.Boot() and find MemoryMessageBuss.RegisterAssembly(assembly);

#DomainDrivenDesign.Core.Implements
(namespace or folder)
- U can place your thin query facade eg: TestDbContext. And Implement your own EventPublisher
