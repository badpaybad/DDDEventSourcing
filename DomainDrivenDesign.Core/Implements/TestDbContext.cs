using System.Data.Entity;
using DomainDrivenDesign.Core.Ef;
using DomainDrivenDesign.Core.Implements.Model;

namespace DomainDrivenDesign.Core.Implements
{
    public class TestDbContext : BaseDbContext
    {
        public TestDbContext() : base("HrisTest")
        {
        }

        public DbSet<CheckinTest> CheckinTests { get; set; }
        public DbSet<CheckinHistoryTest> CheckinHistoryTests { get; set; }
    }
}