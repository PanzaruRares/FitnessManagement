using ProiectPAW.FormAdaugari;
using Seminar7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProiectPAW
{
    public partial class FormClienti : Form
    {

        public FormClienti()
        {
            InitializeComponent();
            setariTabel();
            refreshLista();
            printDocument1.PrintPage += new PrintPageEventHandler(PrintDocument1_PrintPage_1);
        }

        public void refreshLista()
        {
            this.dataGridView1.DataSource = null;
            ClientRepository repo = new ClientRepository();
            this.dataGridView1.DataSource = repo.GetAll();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AdaugaClient form = new AdaugaClient();
            form.ShowDialog();
            refreshLista();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Client clientSelectat = (Client)dataGridView1.CurrentRow.DataBoundItem;

                DialogResult rezultat = MessageBox.Show(
                    $"Sunteți sigur că doriți să ștergeți clientul {clientSelectat.NumeClient} {clientSelectat.PrenumeClient}?",
                    "Confirmare Ștergere",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (rezultat == DialogResult.Yes)
                {
                    try
                    {
                        ClientRepository repo = new ClientRepository();
                        repo.Delete(clientSelectat.IdClient);

                        MessageBox.Show("Clientul a fost șters cu succes!", "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        refreshLista();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"A apărut o eroare la ștergere: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vă rugăm să selectați mai întâi un client din tabel pentru a-l șterge!", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                Client clientSelectat = (Client)dataGridView1.CurrentRow.DataBoundItem;
                AdaugaClient formModificare = new AdaugaClient(clientSelectat);
                formModificare.ShowDialog();
                refreshLista();
            }
            else
            {
                MessageBox.Show("Vă rugăm să selectați mai întâi un client din tabel pentru a-l modifica!", "Atenție", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            if (printPreviewDialog1 is Form dialogForm)
            {
                dialogForm.WindowState = FormWindowState.Maximized;
            }
            printPreviewDialog1.ShowDialog();
        }
        private void PrintDocument1_PrintPage_1(object sender, PrintPageEventArgs e)
        {
            ClientRepository repo = new ClientRepository();
            List<Client> listaClienți = repo.GetAll();

            Font fontTitlu = new Font("Segoe UI", 18, FontStyle.Bold);
            Font fontHeader = new Font("Segoe UI", 12, FontStyle.Bold);
            Font fontText = new Font("Segoe UI", 10, FontStyle.Regular);

            Brush pensulaNeagra = Brushes.Black;
            Brush pensulaAlbastra = Brushes.Navy;
            Pen creionGri = new Pen(Color.LightGray, 1);

            int startX = e.MarginBounds.Left;
            int startY = e.MarginBounds.Top;
            int distantaRand = 30;

            int colIdX = startX;
            int colNumeX = startX + 80;
            int colAbonamentX = startX + 350;

            e.Graphics.DrawString("RAPORT CONFIGURARE CLIENȚI", fontTitlu, pensulaAlbastra, startX, startY);
            startY += 50;

            e.Graphics.DrawString("Generat la data: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm"), fontText, Brushes.Gray, startX, startY);
            startY += 40;

            e.Graphics.DrawString("ID", fontHeader, pensulaAlbastra, colIdX, startY);
            e.Graphics.DrawString("Nume Complet Client", fontHeader, pensulaAlbastra, colNumeX, startY);
            e.Graphics.DrawString("Tip Abonament", fontHeader, pensulaAlbastra, colAbonamentX, startY);

            startY += 25;
            e.Graphics.DrawLine(creionGri, startX, startY, e.MarginBounds.Right, startY);
            startY += 10;

            foreach (var client in listaClienți)
            {
                if (startY + distantaRand > e.MarginBounds.Bottom)
                {
                    e.HasMorePages = true;
                    return;
                }

                e.Graphics.DrawString(client.IdClient.ToString(), fontText, pensulaNeagra, colIdX, startY);

                string numeComplet = $"{client.NumeClient} {client.PrenumeClient}";
                e.Graphics.DrawString(numeComplet, fontText, pensulaNeagra, colNumeX, startY);

                e.Graphics.DrawString(client.TipAbonament.ToString(), fontText, pensulaNeagra, colAbonamentX, startY);

                startY += distantaRand;
                e.Graphics.DrawLine(creionGri, startX, startY - 5, e.MarginBounds.Right, startY - 5);
            }

            e.HasMorePages = false;
        }

        public void setariTabel()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(41, 128, 185);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 35;
            dataGridView1.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.GridColor = Color.FromArgb(220, 224, 230);
        }
    }
}
