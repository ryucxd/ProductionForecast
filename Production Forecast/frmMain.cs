using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Production_Forecast
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.productionForecast;

            //start filling the comboboxes :)
            //year
            cmbYear.Items.Add("2019");
            cmbYear.Items.Add("2020");
            cmbYear.Items.Add("2021");
            cmbYear.Items.Add("2022");
            cmbYear.SelectedIndex = 1;
            //month
            cmbMonth.Items.Add("January");
            cmbMonth.Items.Add("February");
            cmbMonth.Items.Add("March");
            cmbMonth.Items.Add("April");
            cmbMonth.Items.Add("May");
            cmbMonth.Items.Add("June");
            cmbMonth.Items.Add("July");
            cmbMonth.Items.Add("August");
            cmbMonth.Items.Add("September");
            cmbMonth.Items.Add("October");
            cmbMonth.Items.Add("November");
            cmbMonth.Items.Add("December");
            cmbMonth.SelectedIndex = 0;
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //oopsie
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //properly clos e the form to stop the one hidden form from hanging and wasting resources :)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    Environment.Exit(1);
                else
                    e.Cancel = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //validation
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_production_forecast", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@month", SqlDbType.VarChar).Value = cmbMonth.Text;
                    cmd.Parameters.Add("@year", SqlDbType.VarChar).Value = cmbYear.Text;
                    conn.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();
                    while (sdr.Read())
                    {
                        lblTraditionalDoor.Text = "£" + sdr["traditionalDoor"].ToString();
                        lblCostOfExtras.Text = "£" + sdr["costExtras"].ToString();
                        lblTotalDelivery.Text = "£" + sdr["totalDelivery"].ToString();
                        lblTotalInstallation.Text = "£" + sdr["totalInstallation"].ToString();
                        lblSubTotal.Text = "£" + sdr["subTotal"].ToString();
                        lblInvoicePrevMonths.Text = "£" + sdr["invoicePrevMonths"].ToString();
                        lblInvoicethisMonth.Text = "£" + sdr["invoiceThisMonth"].ToString();
                        lblTotal.Text = "£" + sdr["total"].ToString();
                        lblTarget.Text = "£" + sdr["target"].ToString();
                        lblAmountToTarget.Text = "£" + sdr["amountToTarget"].ToString();
                    }

                }
            }
        }
    }
}
