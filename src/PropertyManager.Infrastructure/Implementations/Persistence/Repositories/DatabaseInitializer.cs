using Microsoft.EntityFrameworkCore;
using PropertyManager.Infrastructure.Implementations.Persistence.EFCore.Context;

namespace PropertyManager.Infrastructure.Implementations.Persistence.Repositories
{
    public class DatabaseInitializer(PropertyManagerDbContext context)
    {
        public void Initialize()
        {
            bool created = context.Database.EnsureCreated();

            if (created)
            {
                Console.WriteLine("Base de datos creada, ejecutando scripts de inicialización...");

                var sqlFile = Path.Combine(AppContext.BaseDirectory, "Scripts", "InitData.sql");

                if (File.Exists(sqlFile))
                {
                    var sql = File.ReadAllText(sqlFile);
                    context.Database.ExecuteSqlRaw(sql);
                }
            }
            else
            {
                Console.WriteLine("La base de datos ya existía, no se ejecutaron scripts.");
            }
        }
    }
}
