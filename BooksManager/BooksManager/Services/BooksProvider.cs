using BooksManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksManager.Services
{
    public class BooksProvider
    {
        private SqlConnection connection;
        public BooksProvider(SqlConnection connection)
        {
            this.connection = connection;
        }
        public List<Book> GetAll()
        {
            var list = new List<Book>();
            try
            {
                connection.Open();
                var cmd = new SqlCommand(
                    @"
SELECT
    [Id],
    [Title],
    [Pages],
    [Genre],
    [Authors]
FROM
    [Books]
WHERE
    [IsDeleted] = 'False'", 
                    connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var book = new Book
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Pages = reader.GetInt32(2),
                            Genre = reader.GetString(3),
                            Authors = reader.GetString(4)
                        };
                        list.Add(book);
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
        public bool AddItem(Book item)
        {
            var cmd = new SqlCommand(
                @"
INSERT INTO [Books]
    ([Title], [Pages], [Genre], [Authors])
VALUES
    (@Title, @Pages, @Genre, @Authors)", 
                connection);
            cmd.Parameters.AddWithValue("@Title", item.Title);
            cmd.Parameters.AddWithValue("@Pages", item.Pages);
            cmd.Parameters.AddWithValue("@Genre", item.Genre);
            cmd.Parameters.AddWithValue("@Authors", item.Authors);
            return ExecuteNonQuery(cmd);
        }
        public bool UpdateItem(Book item)
        {
            var cmd = new SqlCommand(
            @"
UPDATE [Books]
SET
    [Title] = @Title,
    [Pages] = @Pages,
    [Genre] = @Genre,
    [Authors] = @Authors
WHERE
    [Id] = @Id", 
                connection);
            cmd.Parameters.AddWithValue("@Title", item.Title);
            cmd.Parameters.AddWithValue("@Pages", item.Pages);
            cmd.Parameters.AddWithValue("@Genre", item.Genre);
            cmd.Parameters.AddWithValue("@Authors", item.Authors);
            cmd.Parameters.AddWithValue("@Id", item.Id);
            return ExecuteNonQuery(cmd);
        }
        public bool DeleteItem(int id)
        {
            var cmd = new SqlCommand(
            @"
UPDATE [Books]
SET
    [IsDeleted] = 'True'
WHERE
    [Id] = @Id",
                connection);
            cmd.Parameters.AddWithValue("@Id", id);
            return ExecuteNonQuery(cmd);
        }
        public bool ExecuteNonQuery(SqlCommand cmd)
        {
            bool result = false;
            try
            {
                connection.Open();
                result = cmd.ExecuteNonQuery() > 0;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

    }
}
