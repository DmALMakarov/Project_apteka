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
    public partial class Company : Form
    {
        public Company()
        {
            InitializeComponent();
        }

        private void Company_Load(object sender, EventArgs e)
        {
            DataTable dt = data.RunQuery("SELECT * FROM company");
            data.PopulateTreeView(treeView1, dt, 0, null, "SELECT * FROM medecine WHERE id_company =", "id_company");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
