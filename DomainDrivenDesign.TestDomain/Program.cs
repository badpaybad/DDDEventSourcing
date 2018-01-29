using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Core.Implements;
using DomainDrivenDesign.Domain;
using DomainDrivenDesign.Domain.Commands;

namespace DomainDrivenDesign.TestDomain
{
   public class Program
    {
      
        public static void Main(params string[] args)
        {
            DomainEngine.Boot();

            Console.WriteLine("Hello");

            Console.WriteLine("Create checkin");
            var checkin = new CreateCheckin(Guid.NewGuid(), 0, 10, DateTime.Now);
            MemoryMessageBuss.PushCommand(checkin);

            Console.WriteLine("Employee commend");
            MemoryMessageBuss.PushCommand(new CommentCheckinByEmployee(checkin.CheckinId, "Hi boss", 0));

            Console.WriteLine("Evaluator comment");
            MemoryMessageBuss.PushCommand(new CommentCheckinByEvaluator(checkin.CheckinId, "Hi man, we should change 'boss' to 'bro' ", 0));
            
            Console.WriteLine("TestDbContext like db to make thin query facade. ");
            Console.WriteLine("UI -> List all Checkin");

            using (var db =new TestDbContext())
            {
                foreach (var c in db.CheckinTests.ToList())
                {
                    Console.WriteLine($"Checkin: {c.StartDate} with Duration: {c.Duration}");
                    foreach (var ch in db.CheckinHistoryTests.Where(i=>i.CheckinId==c.Id).ToList())
                    {
                        Console.WriteLine($"-comment- {ch.Comment}");
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
