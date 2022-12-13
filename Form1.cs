using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apteka
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            data.FillComboBox("SELECT * FROM Pharmacies", "name", "id_Pharmacies", comboBox1);
            data.FillComboBox("SELECT * FROM medecine", "name", "id_medecine", comboBox2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Text = "";
            data.FillDatatoDGV(dataGridView1, Convert.ToInt16(comboBox1.SelectedValue));
            data.FillComboBox("SELECT medecine.name, medecine.id_medecine FROM medecine, data WHERE medecine.id_medecine = data.id_medecine AND id_pharmacies = " + comboBox1.SelectedValue, "name", "id_medecine", comboBox3);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool insertofupdate = false;

            DataTable dt = data.RunQuery("SELECT * FROM data WHERE id_pharmacies= " + comboBox1.SelectedValue);
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt16(comboBox2.SelectedValue) == Convert.ToInt16(row["id_medecine"])) insertofupdate = true; 
            }

            if (insertofupdate == true)
            {
                data.SqlCommander("UPDATE data SET count = count + " + textBox1.Text + " WHERE id_medecine = " + comboBox2.SelectedValue + " AND id_pharmacies = " + comboBox1.SelectedValue);
                data.FillDatatoDGV(dataGridView1, Convert.ToInt16(comboBox1.SelectedValue));
            }
            else
            {
                data.SqlCommander("INSERT INTO data (id_pharmacies, id_medecine, count) VALUES (" + comboBox1.SelectedValue + "," + comboBox2.SelectedValue + "," + textBox1.Text + ")");
                data.FillDatatoDGV(dataGridView1, Convert.ToInt16(comboBox1.SelectedValue));
                data.FillComboBox("SELECT medecine.name, medecine.id_medecine FROM medecine, data WHERE medecine.id_medecine = data.id_medecine AND id_pharmacies = " + comboBox1.SelectedValue, "name", "id_medecine", comboBox3);
            }
                       
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Company company = new Company();
            company.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            medecine medecine = new medecine();
            medecine.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int count = 0;
            DataTable dt = data.RunQuery("SELECT * FROM data WHERE id_medecine = " + comboBox3.SelectedValue + " AND id_pharmacies= " + comboBox1.SelectedValue);
            foreach (DataRow row in dt.Rows)
            {
                if (Convert.ToInt16(textBox2.Text) > Convert.ToInt16(row["count"]))
                {
                    MessageBox.Show("Введенное кол-во больше имеющегося");
                    return;
                } else
                {
                    count = Convert.ToInt16(row["count"]);
                }
            }

            int newCount = count - Convert.ToInt16(textBox1.Text);

            data.SqlCommander("UPDATE data SET count = " + newCount + " WHERE id_medecine = " + comboBox3.SelectedValue + " AND id_pharmacies = " + comboBox1.SelectedValue);
            data.FillDatatoDGV(dataGridView1, Convert.ToInt16(comboBox1.SelectedValue));
        }
    }
}
