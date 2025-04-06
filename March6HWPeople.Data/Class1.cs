using Microsoft.Data.SqlClient;
namespace March6HWPeople.Data
{
    public class PersonDb()
    {
        private readonly string _connectionString;

        public PersonDb(string connectionString) => _connectionString = connectionString;

        public List<Person> GetPeople()
        {
            using var connection = new SqlConnection(_connectionString);
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            var people = new List<Person>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                people.Add(new()
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["Name"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"],
                });
            }
            return people;
        }
    }
}
