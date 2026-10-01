using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar7
{
    public class AntrenorRepository
    {
        private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Rares\\source\\repos\\ProiectPAW\\ProiectPAW\\conexiune.mdf;Integrated Security=True;Connect Timeout=30";

        public List<Antrenor> GetAll()
        {
            var results = new List<Antrenor>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT IdAntrenor, Nume, Prenume, Salariu FROM Antrenori", connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var antrenor = new Antrenor();
                        antrenor.IdAntrenor = reader.GetInt32(reader.GetOrdinal("IdAntrenor"));
                        antrenor.Nume = reader.GetString(reader.GetOrdinal("Nume"));
                        antrenor.Prenume = reader.GetString(reader.GetOrdinal("Prenume"));
                        antrenor.Salariu = reader.GetDecimal(reader.GetOrdinal("Salariu"));

                        results.Add(antrenor);
                    }
                }
                connection.Close();
            }

            return results;
        }

        public Antrenor GetById(int id)
        {
            var antrenor = new Antrenor();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT IdAntrenor, Nume, Prenume, Salariu FROM Antrenori WHERE IdAntrenor = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        antrenor.IdAntrenor = reader.GetInt32(reader.GetOrdinal("IdAntrenor"));
                        antrenor.Nume = reader.GetString(reader.GetOrdinal("Nume"));
                        antrenor.Prenume = reader.GetString(reader.GetOrdinal("Prenume"));
                        antrenor.Salariu = reader.GetDecimal(reader.GetOrdinal("Salariu"));
                    }
                }
                connection.Close();
            }

            return antrenor;
        }

        public void Add(Antrenor antrenor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Antrenori (IdAntrenor, Nume, Prenume, Salariu) VALUES (@id, @nume, @prenume, @salariu)", connection))
                {
                    command.Parameters.AddWithValue("id", antrenor.IdAntrenor);
                    command.Parameters.AddWithValue("nume", antrenor.Nume);
                    command.Parameters.AddWithValue("prenume", antrenor.Prenume);
                    command.Parameters.AddWithValue("salariu", antrenor.Salariu);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Update(Antrenor antrenor)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE Antrenori SET Nume = @nume, Prenume = @prenume, Salariu = @salariu WHERE IdAntrenor = @id", connection))
                {
                    command.Parameters.AddWithValue("id", antrenor.IdAntrenor);
                    command.Parameters.AddWithValue("nume", antrenor.Nume);
                    command.Parameters.AddWithValue("prenume", antrenor.Prenume);
                    command.Parameters.AddWithValue("salariu", antrenor.Salariu);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        public int? ObtineIdAntrenorDupaNume(string nume)
        {
            int? idGasit = null;
            string query = "SELECT IdAntrenor FROM Antrenori WHERE Nume = @nume";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("nume", nume);

                    var result = command.ExecuteScalar(); 
                    if (result != null && result != DBNull.Value)
                    {
                        idGasit = Convert.ToInt32(result);
                    }
                }
                connection.Close();
            }

            return idGasit; 
        }
        public void PopuleazaDateTest()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var checkCommand = new SqlCommand("SELECT COUNT(*) FROM Antrenori", connection);
                int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                if (count == 0)
                {
                    string insertQuery = @"
                INSERT INTO Antrenori (IdAntrenor, Nume, Prenume, Salariu) VALUES 
                (1, 'Popescu', 'Andrei', 4500.00),
                (2, 'Ionescu', 'Maria', 5200.00),
                (3, 'Stancu', 'Radu', 4000.00);";

                    using (var insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
        }
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Antrenori WHERE IdAntrenor = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}