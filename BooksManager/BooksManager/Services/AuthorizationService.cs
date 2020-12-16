using BooksManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.Services
{
    public class AuthorizationService
    {
        private SqlConnection connection;
        public User Me { get; private set; }
        public AuthorizationService(SqlConnection connection)
        {
            this.connection = connection;
        }

        public User GetUserByLogin(string login)
        {
            User user = null;
            try
            {
                connection.Open();
                var cmd = new SqlCommand(
                    @"
SELECT
    [Id],
    [Login],
    [Password],
    [Role]
FROM
    [Users]
WHERE
    [Login] = @Login", 
                    connection);
                cmd.Parameters.AddWithValue("@Login", login);

                using (var reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    var item = new User
                    {
                        Id = reader.GetInt32(0),
                        Login = reader.GetString(1),
                        Password = reader.GetString(2),
                        Role = reader.GetString(3)
                    };
                    user = item;
                }
            }
            finally
            {
                connection.Close();
            }
            return user;
        }

        public string GetHashedPassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var key = Encoding.UTF8.GetBytes("Makushkin");
            var h = new HMACSHA256(key);
            var hash = h.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public bool VerifyPassword(string login, string password)
        {
            bool result = false;
            var user = GetUserByLogin(login);
            if (user == null)
                return result;
            string hashedPassword = GetHashedPassword(password);
            if (user.Password == hashedPassword)
            {
                result = true;
                Me = user;
                Me.Password = null;
            }
            return result;
        }
    }
}
