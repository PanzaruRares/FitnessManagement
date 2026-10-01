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

namespace ProiectPAW
{
    public partial class FormAbonamente : Form
    {
        private AbonamentRepository repo = new AbonamentRepository();
        private bool seIncarcaDatele = false;

        public FormAbonamente()
        {
            InitializeComponent();
            repo.InitializeazaAbonamente(); 
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

            if (dataGridView1.Columns["TipAbonament"] != null)
            {
                dataGridView1.Columns["TipAbonament"].ReadOnly = true;
                dataGridView1.Columns["TipAbonament"].DefaultCellStyle.BackColor = Color.FromArgb(235, 240, 245);
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (seIncarcaDatele || e.RowIndex < 0) return;

            try
            {
                Abonament abonamentModificat = (Abonament)dataGridView1.Rows[e.RowIndex].DataBoundItem;
                repo.Update(abonamentModificat);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eroare la salvarea automată: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
