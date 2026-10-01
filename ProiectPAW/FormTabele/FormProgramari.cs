using ProiectPAW.ClaseBaza;
using ProiectPAW.Repos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPAW.FormTabele
{
    public partial class FormProgramari : Form
    {
        private ProgramariRepository repo = new ProgramariRepository();
        private bool seIncarcaDatele = false;

        public FormProgramari()
        {
            InitializeComponent();
            setariTabel();
            refreshLista();
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }

        public void refreshLista()
        {
            seIncarcaDatele = true;
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = repo.GetAll();
            seIncarcaDatele = false;
            if (dataGridView1.Columns["IdRezervare"] != null)
            {
                dataGridView1.Columns["IdRezervare"].ReadOnly = true;
                dataGridView1.Columns["IdRezervare"].DefaultCellStyle.BackColor = Color.FromArgb(235, 240, 245);
            }
        }
        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (seIncarcaDatele || e.RowIndex < 0) return;

            try
            {
                Programare programareModificata = (Programare)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                repo.Update(programareModificata);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Eroare Validare ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                refreshLista();
            }
        }
        public void setariTabel()
        {
            dataGridView1.ReadOnly = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect; 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 35;

            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.GridColor = Color.FromArgb(220, 224, 230);
        }

        private static int contorId = 0;

        private void btnAdauga_Click(object sender, EventArgs e)
        {
            try
            {
                int idTemporar = contorId++;

                Programare programareNoua = new Programare(idTemporar, 60, 1, 1);
                repo.Add(programareNoua);

                refreshLista();

                int ultimulRand = dataGridView1.Rows.Count - 1;
                dataGridView1.CurrentCell = dataGridView1.Rows[ultimulRand].Cells["IdRezervare"];
                dataGridView1.BeginEdit(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la crearea programării noi: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSterge_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Programare selectata = (Programare)dataGridView1.CurrentRow.DataBoundItem;

                DialogResult rez = MessageBox.Show(
                    $"Sigur ștergeți programarea cu ID-ul {selectata.IdRezervare}?",
                    "Confirmare Ștergere",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (rez == DialogResult.Yes)
                {
                    try
                    {
                        repo.Delete(selectata.IdRezervare);
                        refreshLista();
                        MessageBox.Show("Programare ștearsă cu succes!", "Succes");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Eroare la ștergere: {ex.Message}", "Eroare");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selectați o programare din tabel pentru a o șterge!", "Atenție");
            }
        }
    }
}
