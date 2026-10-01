using Seminar7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPAW.FormAdaugari
{
    public partial class AdaugaClient : Form
    {
        private ClientRepository repo = new ClientRepository();
        private bool esteModificare = false;
        private Client clientDeModificat = null;
        public AdaugaClient()
        {
            InitializeComponent();
            cbAbonament.DataSource = Enum.GetValues(typeof(TipAbonament));
        }
        public AdaugaClient(Client client) : this()
        {
            esteModificare = true;
            clientDeModificat = client;
            this.Text = "Modifică Client";

            tbId.Text = client.IdClient.ToString();
            tbId.ReadOnly = true;

            tbNume.Text = client.NumeClient;
            tbPrenume.Text = client.PrenumeClient;
            cbAbonament.SelectedItem = client.TipAbonament;
            dtpDataInscriere.Value = client.DataInscriere;

            if (client.idAntrenor.HasValue)
            {
                tbIdAntrenor.Text = client.idAntrenor.Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int idClient = Convert.ToInt32(tbId.Text);
                string nume = tbNume.Text.Trim();
                string prenume = tbPrenume.Text.Trim();
                TipAbonament abonament = (TipAbonament)cbAbonament.SelectedItem;
                DateTime dataInscriere = dtpDataInscriere.Value;

                int? idAntrenor = null;
                if (!string.IsNullOrWhiteSpace(tbIdAntrenor.Text))
                {
                    idAntrenor = Convert.ToInt32(tbIdAntrenor.Text);
                }
                Client clientCurent = new Client(idClient, nume, prenume, abonament, dataInscriere, idAntrenor);
                if (tbId.ReadOnly == true)
                {
                    repo.Update(clientCurent);

                    MessageBox.Show("Clientul a fost modificat cu succes în baza de date!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
                else
                {
                    repo.Add(clientCurent);

                    MessageBox.Show("Clientul a fost adăugat cu succes în baza de date!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CurataCampuri();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"A apărut o eroare neprevăzută: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CurataCampuri()
        {
            tbId.Clear();
            tbNume.Clear();
            tbPrenume.Clear();
            tbIdAntrenor.Clear();
            tbCautaNumeAntrenor.Clear();
            if (cbAbonament.Items.Count > 0) cbAbonament.SelectedIndex = 0;
            dtpDataInscriere.Value = DateTime.Now;
        }
        private void button2_Click(object sender, EventArgs e)
        {

            string numeCautat = tbCautaNumeAntrenor.Text.Trim();

            if (string.IsNullOrWhiteSpace(numeCautat))
            {
                MessageBox.Show("Introduceți un nume de antrenor pentru a-l căuta!", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AntrenorRepository antrenorRepo = new AntrenorRepository();
            int? idAntrenor = antrenorRepo.ObtineIdAntrenorDupaNume(numeCautat);

            if (idAntrenor.HasValue)
            {
                tbIdAntrenor.Text = idAntrenor.Value.ToString();
            }
            else
            {
                MessageBox.Show("Nu s-a găsit niciun antrenor cu acest nume în baza de date!", "Inexistent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tbIdAntrenor.Clear();
            }

        }
    }
}
