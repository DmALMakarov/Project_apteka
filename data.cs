using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace apteka
{
    class data
    {
        static SQLiteConnection sqlCon = new SQLiteConnection(ConfigurationManager.ConnectionStrings["SQLITE"].ToString());
        static DataSet ds = new DataSet();
        static SQLiteDataAdapter da;
        static SQLiteCommand cmd;

        public static void FillComboBox(string query, string member, string vmember, ComboBox comboBox)
        {
            da = new SQLiteDataAdapter(query, sqlCon);
            DataTable tbl = new DataTable();
            da.Fill(tbl);

            comboBox.ValueMember = vmember;
            comboBox.DisplayMember = member;
            comboBox.DataSource = tbl;
            
            sqlCon.Close();
        }

        //Метод для заполнения табиц из БД
        public static void FillDatatoDGV(DataGridView dataGridView, int id)
        {
            cmd = new SQLiteCommand("SELECT medecine.name AS 'Название препарата', data.count AS 'Количество' FROM medecine, data WHERE medecine.id_medecine = data.id_medecine AND id_pharmacies = " + id, sqlCon);
            ds = new DataSet();
            da = new SQLiteDataAdapter(cmd);
            da.Fill(ds, "e");
            dataGridView.DataSource = ds.Tables[0];
        }

        public static void UpdateorInsert()
        {

        }

        public static void PopulateTreeView(TreeView treeView, DataTable dtParent, int parentId, TreeNode treeNode, string query, string idName)
        {
            foreach (DataRow row in dtParent.Rows)
            {
                TreeNode child = new TreeNode
                {
                    Text = row["name"].ToString(),
                    Tag = row[idName]
                };



                if (parentId == 0)
                {
                    treeView.Nodes.Add(child);
                    DataTable dtChild = RunQuery(query + child.Tag);
                    PopulateTreeView(treeView, dtChild, Convert.ToInt32(child.Tag), child, null, "id_medecine");
                }
                else
                {
                    treeNode.Nodes.Add(child);
                }
            }
        }

        public static DataTable RunQuery(String Query)
        {
            DataTable dt = new DataTable();

            string constr = @"Data Source=.\DB.sqlite;Version=3;";

            using (SQLiteConnection con = new SQLiteConnection(constr))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(Query))
                {
                    using (SQLiteDataAdapter sda = new SQLiteDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }

        //Выолнение SQL-кода
        public static void SqlCommander(string query)
        {
            sqlCon.Open();
            SQLiteCommand command = new SQLiteCommand(query, sqlCon);
            command.ExecuteNonQuery();
            sqlCon.Close();
        }


        public static void getData()
        {

        }



    }
}
