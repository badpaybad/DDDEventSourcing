using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DomainDrivenDesign.Core.Implements;
using DomainDrivenDesign.Domain;
using DomainDrivenDesign.Domain.AutoNumber.Commands;
using DomainDrivenDesign.Domain.Commands;
using DomainDrivenDesign.DomainShoppingCart.Commands;
using DomainDrivenDesign.DomainShoppingCart.Entities;
using Newtonsoft.Json;

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

            //Console.WriteLine("Employee commend");
            //MemoryMessageBuss.PushCommand(new CommentCheckinByEmployee(checkin.CheckinId, "Hi boss", 0));

            //Console.WriteLine("Evaluator comment");
            //MemoryMessageBuss.PushCommand(new CommentCheckinByEvaluator(checkin.CheckinId, "Hi man, we should change 'boss' to 'bro' ", 0));
            

            //Console.WriteLine("Test auto number");
            // MemoryMessageBuss.PushCommand(new CreateAutoNumberTest("My old Name"));
            //from ui got Id for AutoNumberTest
            //using (var db =new TestDbContext())
            //{
            //    var temp = db.AutoNumberTests.FirstOrDefault();
            //    MemoryMessageBuss.PushCommand(new ChangeNameOfAutoNumberTest(10, "This is New Name"));
            //}

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

                foreach (var a in db.AutoNumberTests.ToList())
                {
                    Console.WriteLine($"Autonumber Id:{a.Id} Name:{a.Name}");
                }
            }

            Console.WriteLine("Test shopping cart");

            var shoppingCartId = Guid.NewGuid().ToString();
            Console.WriteLine("create shopping cart");
            MemoryMessageBuss.PushCommand(new CreateShoppingCart(shoppingCartId));
            Console.WriteLine("add item shopping cart");
            MemoryMessageBuss.PushCommand(new AddItemToShoppingCart(shoppingCartId,"itemId1","Test1",2));

            Console.WriteLine("Get shopping cart data from http runtime cache");
            //get data from thin query facade of shopping cart : )) just httpruntime cache
            var shoppingCart = HttpRuntime.Cache[shoppingCartId] as FrontEndCart;

            Console.WriteLine(JsonConvert.SerializeObject(shoppingCart));

            Console.ReadLine();
        }
    }
}
