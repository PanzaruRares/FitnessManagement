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

namespace ProiectPAW
{
    public partial class FormAntrenori : Form
    {
        private AntrenorRepository repo = new AntrenorRepository();
        private bool seIncarcaDatele = false;
        public FormAntrenori()
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

            if (dataGridView1.Columns["IdAntrenor"] != null)
            {
                dataGridView1.Columns["IdAntrenor"].ReadOnly = false;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            { 
                Random rand = new Random();
                int idTemporar = -rand.Next(1, 10000);

                Antrenor antrenorGol = new Antrenor(idTemporar, "Nume nou...", "Specializare...",0);
                repo.Add(antrenorGol);

                refreshLista();

                int ultimulRand = dataGridView1.Rows.Count - 1;
                dataGridView1.CurrentCell = dataGridView1.Rows[ultimulRand].Cells["Nume"];
                dataGridView1.BeginEdit(true); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la inserarea rândului nou: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (seIncarcaDatele || e.RowIndex < 0) return;

            try
            {
                Antrenor antrenorModificat = (Antrenor)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                repo.Update(antrenorModificat);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la salvarea automată în SQL: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                refreshLista();
            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Antrenor antrenorSelectat = (Antrenor)dataGridView1.CurrentRow.DataBoundItem;

                DialogResult rez = MessageBox.Show(
                    $"Sunteți sigur că doriți să ștergeți antrenorul '{antrenorSelectat.Nume}'?",
                    "Confirmare Ștergere",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (rez == DialogResult.Yes)
                {
                    try
                    {
                        repo.Delete(antrenorSelectat.IdAntrenor);
                        refreshLista();
                        MessageBox.Show("Antrenor șters cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Nu s-a putut șterge antrenorul deoarece are clienți asignați în tabela Clienti! Detalii: {ex.Message}", "Eroare Cheie Externă", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să selectați mai întâi un antrenor din tabel!", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

    }
}   

