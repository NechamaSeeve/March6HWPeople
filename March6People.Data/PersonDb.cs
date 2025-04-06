using Microsoft.Data.SqlClient;

namespace March6People.Data
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
    public class PersonDb
    {
        private readonly string _connectionString;
        public PersonDb(string connectionString)
        {
            _connectionString = connectionString;
        }
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
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"],
                    
                });
            }
            return people;
        }
        public void Add(List<Person> people)
        {
            if (people[0].FirstName != null || people[0].LastName != null) 
            {
                using var connection = new SqlConnection(_connectionString);
                using var cmd = connection.CreateCommand();
                cmd.CommandText = "Insert into People(FirstName,LastName,Age) " +
                    "Values(@firstName,@lastName,@age)";
                connection.Open();
                foreach (Person p in people)
                {
                    if(p.FirstName != null || p.LastName != null)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@firstName", p.FirstName);
                        cmd.Parameters.AddWithValue("@lastName", p.LastName);
                        cmd.Parameters.AddWithValue("@age", p.Age);
                        cmd.ExecuteNonQuery();
                    }
                    
                }

              
            }
        }
        public void DeleteAll(List<int> ids)
        {
            if (ids.Count > 0) 
            { 
                using var connection = new SqlConnection(_connectionString);

                List<string> parameters = new();
                for (int i = 0; i < ids.Count; i++)
                {
                    parameters.Add($"@ids{i}");
                }
                string sql = $"DELETE FROM People WHERE Id IN ({string.Join(",", parameters)})";
                using var cmd = new SqlCommand(sql, connection);
                for (int i = 0; i < ids.Count; i++)
                {
                    cmd.Parameters.AddWithValue($"@ids{i}", ids[i]);
                }

                connection.Open();
                cmd.ExecuteNonQuery();
          }

        }

    }
}
