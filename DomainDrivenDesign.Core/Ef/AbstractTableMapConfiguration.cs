using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;

namespace DomainDrivenDesign.Core.Ef
{
    public abstract class AbstractTableMapConfiguration<T> : EntityTypeConfiguration<T> where T : class
    {

        protected AbstractTableMapConfiguration()
        {
            var tableName = GetTableName();

            ToTable(tableName);
        }

        public string GetTableName()
        {
            var dnAttribute = typeof(T).GetCustomAttributes(typeof(TableAttribute), true
            ).FirstOrDefault() as TableAttribute;

            string tableName = string.Empty;

            if (dnAttribute != null)
            {
                tableName = dnAttribute.Name;
            }

            if (string.IsNullOrEmpty(tableName))
                tableName = typeof(T).Name;

            return tableName;
        }
    }
}