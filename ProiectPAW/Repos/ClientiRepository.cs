using ProiectPAW;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar7
{
    public class ClientRepository
    {
        private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Rares\\source\\repos\\ProiectPAW\\ProiectPAW\\conexiune.mdf;Integrated Security=True;Connect Timeout=30";

        public List<Client> GetAll()
        {
            var results = new List<Client>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT IdClient, NumeClient, PrenumeClient, TipAbonament, DataInscriere, IdAntrenor FROM Clienti", connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var client = new Client();
                        client.IdClient = reader.GetInt32(reader.GetOrdinal("IdClient"));
                        client.NumeClient = reader.GetString(reader.GetOrdinal("NumeClient"));
                        client.PrenumeClient = reader.GetString(reader.GetOrdinal("PrenumeClient"));
                        client.TipAbonament = (TipAbonament)Enum.Parse(typeof(TipAbonament), reader.GetString(reader.GetOrdinal("TipAbonament")));
                        client.DataInscriere = reader.GetDateTime(reader.GetOrdinal("DataInscriere"));

                        int ordinalAntrenor = reader.GetOrdinal("IdAntrenor");
                        if (reader.IsDBNull(ordinalAntrenor))
                        {
                            client.idAntrenor = null; 
                        }
                        else
                        {
                            client.idAntrenor = reader.GetInt32(ordinalAntrenor); 
                        }

                        results.Add(client);
                    }
                }
                connection.Close();
            }

            return results;
        }

        public Client GetById(int id)
        {
            var client = new Client();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT IdClient, NumeClient, PrenumeClient, TipAbonament, DataInscriere, IdAntrenor FROM Clienti WHERE IdClient = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        client.IdClient = reader.GetInt32(reader.GetOrdinal("IdClient"));
                        client.NumeClient = reader.GetString(reader.GetOrdinal("NumeClient"));
                        client.PrenumeClient = reader.GetString(reader.GetOrdinal("PrenumeClient"));
                        client.TipAbonament = (TipAbonament)Enum.Parse(typeof(TipAbonament), reader.GetString(reader.GetOrdinal("TipAbonament")));
                        client.DataInscriere = reader.GetDateTime(reader.GetOrdinal("DataInscriere"));

                        int ordinalAntrenor = reader.GetOrdinal("IdAntrenor");
                        if (reader.IsDBNull(ordinalAntrenor))
                        {
                            client.idAntrenor = null; 
                        }
                        else
                        {
                            client.idAntrenor = reader.GetInt32(ordinalAntrenor);
                        }
                    }
                }
                connection.Close();
            }

            return client;
        }

        public void Add(Client client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("INSERT INTO Clienti (IdClient, NumeClient, PrenumeClient, TipAbonament, DataInscriere, IdAntrenor) VALUES (@id, @nume, @prenume, @tipAbonament, @dataInscriere, @idAntrenor)", connection))
                {
                    command.Parameters.AddWithValue("id", client.IdClient);
                    command.Parameters.AddWithValue("nume", client.NumeClient);
                    command.Parameters.AddWithValue("prenume", client.PrenumeClient);
                    command.Parameters.AddWithValue("tipAbonament", client.TipAbonament.ToString());
                    command.Parameters.AddWithValue("dataInscriere", client.DataInscriere);
                    command.Parameters.AddWithValue("idAntrenor", (object)client.idAntrenor ?? DBNull.Value); // CORECTAT: Litera mare "I"

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Update(Client client)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("UPDATE Clienti SET NumeClient = @nume, PrenumeClient = @prenume, TipAbonament = @tipAbonament, DataInscriere = @dataInscriere, IdAntrenor = @idAntrenor WHERE IdClient = @id", connection))
                {
                    command.Parameters.AddWithValue("id", client.IdClient);
                    command.Parameters.AddWithValue("nume", client.NumeClient);
                    command.Parameters.AddWithValue("prenume", client.PrenumeClient);
                    command.Parameters.AddWithValue("tipAbonament", client.TipAbonament.ToString());
                    command.Parameters.AddWithValue("dataInscriere", client.DataInscriere);
                    command.Parameters.AddWithValue("idAntrenor", (object)client.idAntrenor ?? DBNull.Value); // CORECTAT: Litera mare "I"

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Clienti WHERE IdClient = @id", connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}