using ProiectPAW.ClaseBaza;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPAW.Repos
{
    public class ProgramariRepository
    {
        private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Rares\\source\\repos\\ProiectPAW\\ProiectPAW\\conexiune.mdf;Integrated Security=True;Connect Timeout=30";

        public List<Programare> GetAll()
        {
            var lista = new List<Programare>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT IdRezervare, Durata, IdClient, IdAngajat FROM Programari";
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new Programare
                        {
                            IdRezervare = reader.GetInt32(reader.GetOrdinal("IdRezervare")),
                            Durata = reader.GetInt32(reader.GetOrdinal("Durata")),
                            IdClient = reader.GetInt32(reader.GetOrdinal("IdClient")),
                            IdAngajat = reader.GetInt32(reader.GetOrdinal("IdAngajat"))
                        });
                    }
                }
            }
            return lista;
        }
        public void Update(Programare p)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string checkClient = "SELECT COUNT(*) FROM Clienti WHERE IdClient = @idClient";
                using (var cmd = new SqlCommand(checkClient, connection))
                {
                    cmd.Parameters.AddWithValue("idClient", p.IdClient);
                    int countClient = Convert.ToInt32(cmd.ExecuteScalar());
                    if (countClient == 0)
                    {
                        throw new Exception($"Clientul cu ID-ul {p.IdClient} nu există în baza de date!");
                    }
                }
                string checkAntrenor = "SELECT COUNT(*) FROM Antrenori WHERE IdAntrenor = @idAngajat";
                using (var cmd = new SqlCommand(checkAntrenor, connection))
                {
                    cmd.Parameters.AddWithValue("idAngajat", p.IdAngajat);
                    int countAntrenor = Convert.ToInt32(cmd.ExecuteScalar());
                    if (countAntrenor == 0)
                    {
                        throw new Exception($"Antrenorul/Angajatul cu ID-ul {p.IdAngajat} nu există în baza de date!");
                    }
                }
                string query = @"UPDATE Programari 
                         SET Durata = @durata, 
                             IdClient = @idClient, 
                             IdAngajat = @idAngajat 
                         WHERE IdRezervare = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("durata", p.Durata);
                    command.Parameters.AddWithValue("idClient", p.IdClient);
                    command.Parameters.AddWithValue("idAngajat", p.IdAngajat);
                    command.Parameters.AddWithValue("id", p.IdRezervare);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void Delete(int idRezervare)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "DELETE FROM Programari WHERE IdRezervare = @id";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", idRezervare);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
        public void Add(Programare p)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Programari (IdRezervare, Durata, IdClient, IdAngajat) 
                         VALUES (@id, @durata, @idClient, @idAngajat)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("id", p.IdRezervare);
                    command.Parameters.AddWithValue("durata", p.Durata);
                    command.Parameters.AddWithValue("idClient", p.IdClient);
                    command.Parameters.AddWithValue("idAngajat", p.IdAngajat);
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
