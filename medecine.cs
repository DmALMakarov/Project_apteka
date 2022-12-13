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
    public partial class medecine : Form
    {
        public medecine()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void medecine_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            data.FillComboBox("SELECT * FROM medecine", "name", "id_medecine", comboBox2);
            data.FillComboBox("SELECT * FROM company", "name", "id_company", comboBox1);
            data.FillComboBox("SELECT * FROM company", "name", "id_company", comboBox4);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            DataTable dt = data.RunQuery("SELECT * FROM medecine WHERE id_medecine = " + comboBox2.SelectedValue);
            foreach(DataRow row in dt.Rows)
            {
                comboBox1.SelectedValue = Convert.ToInt16(row["id_company"]);
                textBox1.Text = row["name"].ToString();
                textBox2.Text = row["price"].ToString();
            }

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            groupBox3.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            data.SqlCommander("UPDATE medecine SET name = '" + textBox1.Text + "', price = " + textBox2.Text + ", id_company = " + comboBox1.SelectedValue + "  WHERE id_medecine = " + comboBox2.SelectedValue);
            MessageBox.Show("Данные обновлены!");
            LoadData();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
            groupBox3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data.SqlCommander("INSERT INTO medecine (name, price, id_company) VALUES ('"+textBox4.Text+"','"+textBox3.Text+"',"+comboBox4.SelectedValue+")");

            MessageBox.Show("Лекарственный препарат добавлен!");
            LoadData();
        }
    }
}
