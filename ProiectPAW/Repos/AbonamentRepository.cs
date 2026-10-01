using ProiectPAW.ClaseBaza;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectPAW.Repos
{

        public class AbonamentRepository
        {
            private string _connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Rares\\source\\repos\\ProiectPAW\\ProiectPAW\\conexiune.mdf;Integrated Security=True;Connect Timeout=30";

            public void InitializeazaAbonamente()
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var checkCommand = new SqlCommand("SELECT COUNT(*) FROM Abonamente", connection);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());

                    if (count == 0)
                    {
                        string insertQuery = @"
                        INSERT INTO Abonamente (TipAbonament, Pret, Facilitati) VALUES 
                        ('Bronze', 100.00, 'Acces zona forta (08:00 - 16:00)'),
                        ('Silver', 150.00, 'Acces zona forta nelimitat'),
                        ('Gold', 200.00, 'Acces forta + cardio nelimitat'),
                        ('PRO', 300.00, 'Acces all-inclusive');";

                        using (var insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                    connection.Close();
                }
            }

            public List<Abonament> GetAll()
            {
                var rezultate = new List<Abonament>();
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("SELECT TipAbonament, Pret, Facilitati FROM Abonamente", connection))
                    {
                        var reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            var ab = new Abonament();
                            ab.TipAbonament = reader.GetString(reader.GetOrdinal("TipAbonament"));
                            ab.Pret = reader.GetDecimal(reader.GetOrdinal("Pret"));
                            ab.Facilitati = reader.IsDBNull(reader.GetOrdinal("Facilitati")) ? "" : reader.GetString(reader.GetOrdinal("Facilitati"));

                            rezultate.Add(ab);
                        }
                    }
                    connection.Close();
                }
                return rezultate;
            }


            public void Update(Abonament abonament)
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Abonamente SET Pret = @pret, Facilitati = @facilitati WHERE TipAbonament = @tip";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("pret", abonament.Pret);
                        command.Parameters.AddWithValue("facilitati", abonament.Facilitati);
                        command.Parameters.AddWithValue("tip", abonament.TipAbonament);

                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
        }
    }

