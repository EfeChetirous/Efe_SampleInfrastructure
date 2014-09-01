using System.Data.Entity;

namespace Infrastructure.Data.DbInitializer
{
    public class DataBaseInitializer : IDatabaseInitializer<DataContext>
    {
        public void InitializeDatabase(DataContext context)
        {
            context.Database.CreateIfNotExists();
        }
    }
}