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
 
 ## class MemoryMessageBuss
 - Fake message buss
 - Implement your own with RabitMQ or AzzureServicesBuss or Redis queue
 
 ## class DomainEngine
 - To register handle to process Event and Command

 ## class Checkin (DDD)
 class Checkin inherit AggregateRoot as an Domain in this sample.
