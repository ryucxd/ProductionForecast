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

namespace Production_Forecast
{
    public partial class frmInvoice : Form
    {
        public string sql { get; set; }
        public frmInvoice(string _sql)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.productionForecast;
            sql = _sql;
            //use this string to fill dgv
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    conn.Close();
                }
            }
            format();
        }

        public void format()
        {
            //adjust dgv hereeeeeee
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Delivery Note #";
            dataGridView1.Columns[2].HeaderText = "Delivery Note Printed";
            dataGridView1.Columns[3].HeaderText = "Invoice Printed";
            dataGridView1.Columns[4].HeaderText = "Customer";
            dataGridView1.Columns[5].HeaderText = "Order Number";
            dataGridView1.Columns[6].HeaderText = "QTY Order";
            dataGridView1.Columns[7].HeaderText = "QTY same";
            dataGridView1.Columns[8].HeaderText = "Status";
            dataGridView1.Columns[9].HeaderText = "Completion Date";
            dataGridView1.Columns[10].HeaderText = "Price Confirm";
            dataGridView1.Columns[11].HeaderText = "Line Cost";
        }
    }
}
