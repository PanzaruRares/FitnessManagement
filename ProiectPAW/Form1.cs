using ProiectPAW.FormTabele;
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
    public partial class Form1 : Form
    {
        private Label lblTotalClienti = new Label();
        private Label lblDataCurenta = new Label();
        public Form1()
        {
            InitializeComponent();
            StylingPrezentare();
            IncarcaDateStatistice();
        }
        ClientRepository repo1 = new ClientRepository();
        AntrenorRepository repo2 = new AntrenorRepository();

        private void aFISARECLIENTIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClienti form = new FormClienti();
            form.Show();
        }

        private void aFISAREABONAMENTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAbonamente form1 = new FormAbonamente();
            form1.Show();
        }

        private void aFISAREINSTRUCTORIToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormAntrenori form3 = new FormAntrenori();
            form3.Show();
        }

        private void aFISAREREZERVARIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProgramari form4 = new FormProgramari();
            form4.Show();
        }
        private void IncarcaDateStatistice()
        {
            try
            {
                ClientRepository repo = new ClientRepository();
                int numarClienti = repo.GetAll().Count;

                lblTotalClienti.Text = $"👥 Clienți: {numarClienti}";
            }
            catch
            {
                lblTotalClienti.Text = "👥 Clienți: --";
            }
        }
        private void StylingPrezentare()
        {
            this.Text = "Dashboard Principal";
            this.BackColor = Color.FromArgb(245, 247, 250);
            this.Size = new Size(850, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            label1.Text = "SISTEM DE GESTIUNE SALA FITNESS";
            label1.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(41, 128, 185);
            label1.Location = new Point(60, 80);
            label1.AutoSize = true;
            label2.Text = "Utilizați meniul de mai sus pentru a naviga prin secțiunile aplicației.";
            label2.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            label2.ForeColor = Color.FromArgb(127, 140, 141);
            label2.Location = new Point(64, 130);
            label2.AutoSize = true;
            label3.Text = "STATISTICI RAPIDE";
            label3.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(44, 62, 80);
            label3.Location = new Point(64, 210);
            label3.AutoSize = true;
            lblTotalClienti.Font = new Font("Segoe UI", 13, FontStyle.Bold);
            lblTotalClienti.ForeColor = Color.FromArgb(41, 128, 185);
            lblTotalClienti.BackColor = Color.White; 
            lblTotalClienti.Location = new Point(64, 245);
            lblTotalClienti.Size = new Size(200, 60);
            lblTotalClienti.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblTotalClienti); 
            lblDataCurenta.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            lblDataCurenta.ForeColor = Color.FromArgb(44, 62, 80);
            lblDataCurenta.BackColor = Color.White;
            lblDataCurenta.Location = new Point(285, 245);
            lblDataCurenta.Size = new Size(200, 60);
            lblDataCurenta.TextAlign = ContentAlignment.MiddleCenter;
            lblDataCurenta.Text = "Data: " + DateTime.Now.ToString("dd.MM.yyyy");
            this.Controls.Add(lblDataCurenta);
            label4.Text = "Status Sistem: Activ și Conectat la baza de date SQL";
            label4.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            label4.ForeColor = Color.FromArgb(149, 165, 166);
            label4.Location = new Point(64, 460);
            label4.AutoSize = true;
        }
    }
}
