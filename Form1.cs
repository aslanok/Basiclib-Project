using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Kitaplik_proje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-1MFFLRS;Initial Catalog=kitaplık_proje;Integrated Security=True");

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Kitaplik", baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        string durum = "";

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("insert into Tbl_Kitaplik (Kitapad,Yazar,Tur,Sayfa,Durum) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut1.Parameters.AddWithValue("@p1", Txtkitapad.Text);
            komut1.Parameters.AddWithValue("@p2", Txtyazar.Text);
            komut1.Parameters.AddWithValue("@p3", CmbTur.Text);
            komut1.Parameters.AddWithValue("@p4", Txtsayfa.Text);
            komut1.Parameters.AddWithValue("@p5", durum);
            komut1.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap sisteme kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            Txtkitapid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            Txtkitapad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            Txtyazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbTur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            Txtsayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("delete from Tbl_Kitaplik where Kitapid=@p1", baglanti);
            komut2.Parameters.AddWithValue("@p1", Txtkitapid.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Listeden silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("update tbl_Kitaplik set Kitapad=@p1,Yazar=@p2,Tur=@p3,Sayfa=@p4,Durum=@p5 where kitapid=@p6", baglanti);
            komut3.Parameters.AddWithValue("@p1", Txtkitapad.Text);
            komut3.Parameters.AddWithValue("@p2", Txtyazar.Text);
            komut3.Parameters.AddWithValue("@p3", CmbTur.Text);
            komut3.Parameters.AddWithValue("@p4", Txtsayfa.Text);
            if(radioButton1.Checked == true)
            {
                komut3.Parameters.AddWithValue("@p5", "0");
            }
            else
            {
                komut3.Parameters.AddWithValue("@p5", "1");
            }
            komut3.Parameters.AddWithValue("@p6", Txtkitapid.Text);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            SqlCommand komut4 = new SqlCommand("Select * from Tbl_Kitaplik where Kitapad=@p1", baglanti);
            komut4.Parameters.AddWithValue("@p1", TxtKitapbul.Text);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut4);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            SqlCommand komut5 = new SqlCommand("Select * from Tbl_Kitaplik where Kitapad like '%"+ TxtKitapbul.Text+"%' ", baglanti);

        }
    }
}