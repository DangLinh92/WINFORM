using ComponentFactory.Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LFEM_OCR
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;

            string connString = "Data Source = 10.70.21.233;Initial Catalog = WHNP1_RSM;User Id = whnp1mesadmin;Password = whnp1mesadmin;Connect Timeout=3";
            SqlConnection conn = new SqlConnection(connString);
            conn.Open();

            SqlCommand command = new SqlCommand("Select MODEL, ROTATE from TB_MODEL_ROTATE", conn);

            DataTable dt = new DataTable();
            dt.Columns.Add("MODEL", typeof(System.String));
            dt.Columns.Add("ROTATE", typeof(System.String));

            using(SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    dt.Rows.Add(reader["MODEL"], reader["ROTATE"]);
                }
            }

            conn.Close();
            dataGridView1.DataSource = dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string connString = "Data Source = 10.70.21.233;Initial Catalog = WHNP1_RSM;User Id = whnp1mesadmin;Password = whnp1mesadmin;Connect Timeout=3";
            SqlConnection conn = new SqlConnection(connString);

            conn.Open();

            SqlCommand cmd = new SqlCommand("SP_SETTING_MODEL_ROTATE", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            for(int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                if(!string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[0].Value.ToString().Trim()) && !string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[1].Value.ToString().Trim()))
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("@A_MODEL", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().ToUpper();
                    cmd.Parameters.Add("@A_ROTATE", SqlDbType.VarChar).Value = dataGridView1.Rows[i].Cells[1].Value.ToString().Trim().ToUpper();
                    cmd.ExecuteNonQuery();
                }
            }
            
            conn.Close();

            MessageBox.Show("       SAVE OK!       ", "MESSAGE", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
