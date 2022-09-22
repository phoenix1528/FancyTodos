using Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class SqliteInMemoryDb
    {
        public static DbContextOptions CreateSqliteInMemoryDbOptions()
        {
            var _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            return new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(_connection)
                .Options;
        }
    }
}
