using System.Data.Entity;
using DomainDrivenDesign.Core.Ef;
using DomainDrivenDesign.Core.Hris.Model;

namespace DomainDrivenDesign.Core.Hris
{
    public class HrisDbContext : BaseDbContext
    {
        public HrisDbContext() : base("HrisTest")
        {
        }

        public DbSet<CheckinTest> CheckinTests { get; set; }
        public DbSet<CheckinHistoryTest> CheckinHistoryTests { get; set; }
    }
}