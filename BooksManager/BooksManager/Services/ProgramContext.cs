using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.Services
{
    public class ProgramContext
    {
        public AuthorizationService Authorization { get; }
        public BooksProvider Books { get; }
        public ProgramContext()
        {
            var connection = CreateSqlConnection();
            Authorization = new AuthorizationService(connection);
            Books = new BooksProvider(connection);
        }
        private SqlConnection CreateSqlConnection()
        {
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";
            builder.InitialCatalog = "BooksStorage";
            builder.IntegratedSecurity = true;
            return new SqlConnection(builder.ToString());
        }
    }
}
