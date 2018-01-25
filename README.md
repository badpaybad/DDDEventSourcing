# Domain driven design and EventSourcing
DDD EventSourcing

 .Net framework 4.6.1
 Entity framework 6.0.0
 Newtonjson 10.0.3
 System.Data.SqlClient 4.4.2
 
 #Guider
 Clone and open with visual studio 2017
 Rebuill all solution to restore nuget package
 
 ## class MemoryMessageBuss
 Fake message buss
 Can use RabitMQ or AzzureServicesBuss or Redis queue
 
 ## class DomainEngine
 To register handle to process Event and Command
