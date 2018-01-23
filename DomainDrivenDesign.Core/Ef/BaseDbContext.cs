using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace DomainDrivenDesign.Core.Ef
{
    public class BaseDbContext : DbContext
    {
        //public BaseDbContext() : base("HomeCook")
        //{

        //}
        public BaseDbContext(string nameConnectionString) : base(nameConnectionString)
        {

        }
        public BaseDbContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            var types = Assembly.GetExecutingAssembly().GetTypes();

            var typesToRegister = types
                .Where(type => !string.IsNullOrEmpty(type.Namespace))
                .Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(AbstractTableMapConfiguration<>));

            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}