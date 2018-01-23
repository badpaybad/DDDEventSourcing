using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainDrivenDesign.Domain;
using DomainDrivenDesign.Domain.Commands;

namespace DomainDrivenDesign.TestDomain
{
   public class Program
    {
       CheckinCommandHandle cmd=new CheckinCommandHandle();

        public static void Main(params string[] args)
        {
            DomainBooter.Boot();

            Console.WriteLine("Hello");
            Console.ReadLine();
        }
    }
}
