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
using System.Globalization;

namespace Production_Forecast
{
    public partial class frmMain : Form
    {
        public string tempString { get; set; }
        public bool red { get; set; }
        public bool green { get; set; }
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
                        //traditional
                        lblTraditionalDoor.Text = sdr["traditionalDoor"].ToString();
                        lblCostOfExtras.Text = sdr["costExtras"].ToString();
                        lblTotalDelivery.Text = sdr["totalDelivery"].ToString();
                        lblTotalInstallation.Text = sdr["totalInstallation"].ToString();
                        lblSubTotal.Text = sdr["subTotal"].ToString();
                        lblInvoicePrevMonths.Text = sdr["invoicePrevMonths"].ToString();
                        lblInvoicethisMonth.Text = sdr["invoiceThisMonth"].ToString();
                        lblTotal.Text = sdr["total"].ToString();
                        lblTarget.Text = sdr["target"].ToString();
                        lblAmountToTarget.Text = sdr["amountToTarget"].ToString();
                        //slimline
                        lblSlimlineDoor.Text = sdr["slimlineDoor"].ToString();
                        lblCostOfExtrasSL.Text = sdr["costExtrasSL"].ToString();
                        lblTotalDeliverySL.Text = sdr["totalDeliverySL"].ToString();
                        lblTotalInstallationSL.Text = sdr["totalInstallationSL"].ToString();
                        lblSubTotalSL.Text = sdr["subTotalSL"].ToString();
                        lblInvoicePrevMonthsSL.Text = sdr["invoicePrevMonthsSL"].ToString();
                        lblInvoicethisMonthSL.Text = sdr["invoiceThisMonthSL"].ToString();
                        lblTotalSL.Text = sdr["totalSL"].ToString();
                        lblTargetSL.Text = sdr["targetSL"].ToString();
                        lblAmountToTargetSL.Text = sdr["amountToTargetSL"].ToString();
                        //invoice stuff
                        lblFreeHandInvoiceTotal.Text = sdr["freehandInvoiceTotal"].ToString();
                        lblProformaTotal.Text = sdr["proformaTotal"].ToString();
                        lblCredit.Text = sdr["creditTotal"].ToString();
                        lblPaymentApp.Text = sdr["paymentApp"].ToString();
                        lblInvoiceTotal.Text = sdr["invoiceTotal"].ToString();
                        //TOTALS
                        lblProformaPrevious.Text = sdr["proformaPrev"].ToString();
                        lblOverAll.Text = sdr["overAllTotal"].ToString();
                        lblOverAll2.Text = sdr["overAllTotal2"].ToString();
                        //days until
                        lbl10Days.Text = sdr["10days"].ToString();
                        lblDaysUntil10.Text = sdr["daysUntil10"].ToString();
                        lbl15Days.Text = sdr["15days"].ToString();
                        lblDaysUntil15.Text = sdr["daysUntil15"].ToString();
                        //dailyGoal
                        lblTraditionalDG10.Text = sdr["traditionalDailyGoal"].ToString();
                        lblSlimlineDG10.Text = sdr["slimlineDailyGoal"].ToString();
                        lblTraditionalDG15.Text = sdr["traditionalDailyGoall"].ToString();
                        lblSlimlineDG15.Text = sdr["slimlineDailyGoall"].ToString();
                    }

                }
            }
            // adjust the strings so the formatting is nicer c:
            //traditional
            fixStrings(lblTraditionalDoor.Text, 1);
            lblTraditionalDoor.Text = tempString;

            fixStrings(lblCostOfExtras.Text, 1);
            lblCostOfExtras.Text = tempString;

            fixStrings(lblTotalDelivery.Text, 1);
            lblTotalDelivery.Text = tempString;

            fixStrings(lblTotalInstallation.Text, 1);
            lblTotalInstallation.Text = tempString;

            fixStrings(lblSubTotal.Text, 1);
            lblSubTotal.Text = tempString;

            fixStrings(lblInvoicePrevMonths.Text, 1);
            lblInvoicePrevMonths.Text = tempString;

            fixStrings(lblInvoicethisMonth.Text, 1);
            lblInvoicethisMonth.Text = tempString;

            fixStrings(lblTotal.Text, 1);
            lblTotal.Text = tempString;

            fixStrings(lblTarget.Text, 1);
            lblTarget.Text = tempString;

            fixStrings(lblAmountToTarget.Text, 2);
            lblAmountToTarget.Text = tempString;
            if (red == true)
                lblAmountToTarget.BackColor = Color.PaleVioletRed;
            if (green == true)
                lblAmountToTarget.BackColor = Color.LightSeaGreen;

            //slimline
            fixStrings(lblSlimlineDoor.Text, 1);
            lblSlimlineDoor.Text = tempString;

            fixStrings(lblCostOfExtrasSL.Text, 1);
            lblCostOfExtrasSL.Text = tempString;

            fixStrings(lblTotalDeliverySL.Text, 1);
            lblTotalDeliverySL.Text = tempString;

            fixStrings(lblTotalInstallationSL.Text, 1);
            lblTotalInstallationSL.Text = tempString;

            fixStrings(lblSubTotalSL.Text, 1);
            lblSubTotalSL.Text = tempString;

            fixStrings(lblInvoicePrevMonthsSL.Text, 1);
            lblInvoicePrevMonthsSL.Text = tempString;

            fixStrings(lblInvoicethisMonthSL.Text, 1);
            lblInvoicethisMonthSL.Text = tempString;

            fixStrings(lblTotalSL.Text, 1);
            lblTotalSL.Text = tempString;

            fixStrings(lblTargetSL.Text, 1);
            lblTargetSL.Text = tempString;

            fixStrings(lblAmountToTargetSL.Text, 2);
            lblAmountToTargetSL.Text = tempString;
            if (red == true)
                lblAmountToTargetSL.BackColor = Color.PaleVioletRed;
            if (green == true)
                lblAmountToTargetSL.BackColor = Color.LightSeaGreen;

            fixStrings(lblFreeHandInvoiceTotal.Text, 1);
            lblFreeHandInvoiceTotal.Text = tempString;
            fixStrings(lblProformaTotal.Text, 1);
            lblProformaTotal.Text = tempString;
            fixStrings(lblCredit.Text, 1);
            lblCredit.Text = tempString;
            fixStrings(lblPaymentApp.Text, 1);
            lblPaymentApp.Text = tempString;
            fixStrings(lblInvoiceTotal.Text, 1);
            lblInvoiceTotal.Text = tempString;
            if (red == true)
                lblInvoiceTotal.BackColor = Color.LightSeaGreen;
            if (green == true)
                lblInvoiceTotal.BackColor = Color.PaleVioletRed;
            if (lblInvoiceTotal.Text == "£0.00")
                lblInvoiceTotal.BackColor = Color.LightSeaGreen;

            fixStrings(lblProformaPrevious.Text, 1);
            lblProformaPrevious.Text = tempString;

            fixStrings(lblOverAll.Text, 1);
            lblOverAll.Text = tempString;
            fixStrings(lblOverAll2.Text, 1);
            lblOverAll2.Text = tempString;

            //uhhh some minor formatting for these dudes
            tempString = lbl10Days.Text;
            tempString = tempString.Remove(11, (lbl10Days.Text.Length - 11));
            lbl10Days.Text = tempString;

            tempString = lbl15Days.Text;
            tempString = tempString.Remove(11, (lbl15Days.Text.Length - 11));
            lbl15Days.Text = tempString;

            fixStrings(lblTraditionalDG10.Text, 1);
            lblTraditionalDG10.Text = tempString;
            fixStrings(lblSlimlineDG10.Text, 1);
            lblSlimlineDG10.Text = tempString;
            fixStrings(lblTraditionalDG15.Text, 1);
            lblTraditionalDG15.Text = tempString;
            fixStrings(lblSlimlineDG15.Text, 1);
            lblSlimlineDG15.Text = tempString;

        }
        public void fixStrings(string data, int Erabu)
        {
            red = false;
            green = false;
            tempString = data;
            double temp = 0;
            temp = Convert.ToDouble(data);
            data = temp.ToString("#,##0.00");

            if (temp > 0)
                red = true;
            else
                green = true;

            //ammend stuff like colour and having £-100
            data = "£" + data;
            //first up is the - number
            if (Erabu == 1)
            {
                if (data.Contains("-"))
                {
                    data = data.Replace("-", "");
                    data = data.Insert(0, "-");
                }
            }
            else if (Erabu == 2)
            {
                if (data.Contains("-"))
                {
                    data = "£0";
                }
            }
            tempString = data;

        }

        private void btnPrevInvoice_Click(object sender, EventArgs e)
        {
            string temp = lblInvoicePrevMonths.Text;
            double value = 0;
            if (temp.Contains("£"))
            {
                temp = temp.Replace("£", "");
                value = Convert.ToDouble(temp);
            }
            if (value > 0)
            {
                //open new form to display what doors are needed
                //also build string here and pass that over to allow for one form with minimal changes to the code
                //get the start and end date for the search
                DateTime dateStart = new DateTime();
                string test;
                test = Convert.ToDateTime(cmbMonth.Text + " 01, " + cmbYear.Text).ToString("yyyy-MM-dd");
                //test = test.ToString("yyyy-MM-dd");
                dateStart = Convert.ToDateTime(test, CultureInfo.InvariantCulture);


                string sql = "select a.id,COALESCE(b.id,0) as delivery_id,COALESCE(CAST(date_printed_delivery as nvarchar(max)),'N/A') as date_printed_delivery," +
                    "COALESCE(CAST(date_printed_invoice as nvarchar(max)), 'N/A') as date_printed_invoice,g.[NAME],a.order_number,a.quantity_on_order, " +
                    "quantity_same,f.status_description,a.date_completion,CASE WHEN COALESCE(e.payment_confirm, 0) = 0 THEN 'Not Comfirmed' WHEN COALESCE(e.payment_confirm,0) = 1 THEN 'Confirmed' END as [payment_confirmed], " +
                    "COALESCE(h.line_total, 0) as Door_cost from dbo.door a " +
                    "LEFT JOIN dbo.invoice_door b ON a.id = b.door_id " +
                    "LEFT JOIN dbo.invoice c ON b.invoice_id = c.id " +
                    "LEFT JOIN dbo.door_type d ON a.door_type_id = d.id " +
                    "LEFT JOIN dbo.door_payment e ON a.id = e.door_id " +
                    "LEFT JOIN dbo.[status] f on status_id = f.id " +
                    "LEFT JOIN dbo.SALES_LEDGER g ON a.customer_acc_ref = g.ACCOUNT_REF " +
                     "LEFT JOIN dbo.view_door_value h ON a.id = h.id " +
                    "where date_printed_invoice is null AND" +
                    " (slimline_y_n = 0 OR slimline_y_n is null) and(status_id = 1 or status_id = 2 or status_id = 3 or status_id = 5) AND " +
                    "a.date_completion > DATEADD(day,-60,'" + dateStart.ToString("yyyy-MM-dd") + "') AND a.date_completion < '" + dateStart.ToString("yyyy-MM-dd") + "'" +
                    "  ORDER BY a.date_completion ASC";

                //open the form
                frmInvoice frm = new frmInvoice(sql);
                frm.Show();
            }
        }

        private void btnPrevInvoiceSL_Click(object sender, EventArgs e)
        {
            string temp = lblInvoicePrevMonthsSL.Text;
            double value = 0;
            if (temp.Contains("£"))
            {
                temp = temp.Replace("£", "");
                value = Convert.ToDouble(temp);
            }
            if (value > 0)
            {
                //open new form to display what doors are needed
                //also build string here and pass that over to allow for one form with minimal changes to the code
                //get the start and end date for the search
                DateTime dateStart = new DateTime();
                string test;
                test = Convert.ToDateTime(cmbMonth.Text + " 01, " + cmbYear.Text).ToString("yyyy-MM-dd");
                //test = test.ToString("yyyy-MM-dd");
                dateStart = Convert.ToDateTime(test);


                string sql = "select a.id,COALESCE(b.id,0) as delivery_id,COALESCE(CAST(date_printed_delivery as nvarchar(max)),'N/A') as date_printed_delivery," +
                    "COALESCE(CAST(date_printed_invoice as nvarchar(max)), 'N/A') as date_printed_invoice,g.[NAME],a.order_number,a.quantity_on_order, " +
                    "quantity_same,f.status_description,a.date_completion,CASE WHEN COALESCE(e.payment_confirm, 0) = 0 THEN 'Not Comfirmed' WHEN COALESCE(e.payment_confirm,0) = 1 THEN 'Confirmed' END as [payment_confirmed], " +
                    "COALESCE(h.line_total, 0) as Door_cost from dbo.door a " +
                    "LEFT JOIN dbo.invoice_door b ON a.id = b.door_id " +
                    "LEFT JOIN dbo.invoice c ON b.invoice_id = c.id " +
                    "LEFT JOIN dbo.door_type d ON a.door_type_id = d.id " +
                    "LEFT JOIN dbo.door_payment e ON a.id = e.door_id " +
                    "LEFT JOIN dbo.[status] f on status_id = f.id " +
                    "LEFT JOIN dbo.SALES_LEDGER g ON a.customer_acc_ref = g.ACCOUNT_REF " +
                    "LEFT JOIN dbo.view_door_value h ON a.id = h.id " +
                    "where date_printed_invoice is null AND" +
                    " (slimline_y_n = -1) and(status_id = 1 or status_id = 2 or status_id = 3 or status_id = 5) AND " +
                    "a.date_completion > DATEADD(day,-60,'" + dateStart.ToString("yyyy - MM - dd") + "') AND a.date_completion < '" + dateStart.ToString("yyyy - MM - dd") + "'" +
                    " ORDER BY a.date_completion ASC";

                //open the form
                frmInvoice frm = new frmInvoice(sql);
                frm.Show();
            }
        }

        private void btnThisMonthInvoice_Click(object sender, EventArgs e)
        {
            string temp = lblInvoicethisMonth.Text;
            double value = 0;
            if (temp.Contains("£"))
            {
                temp = temp.Replace("£", "");
                value = Convert.ToDouble(temp);
            }
            if (value > 0)
            {
                //open new form to display what doors are needed
                //also build string here and pass that over to allow for one form with minimal changes to the code
                //get the start and end date for the search
                DateTime dateStart = new DateTime();
                string test;
                test = Convert.ToDateTime(cmbMonth.Text + " 01, " + cmbYear.Text).ToString("yyyy-MM-dd");
                //test = test.ToString("yyyy-MM-dd");
                dateStart = Convert.ToDateTime(test);


                string sql = "select a.id,COALESCE(b.id,0) as delivery_id,COALESCE(CAST(date_printed_delivery as nvarchar(max)),'N/A') as date_printed_delivery," +
                    "COALESCE(CAST(date_printed_invoice as nvarchar(max)), 'N/A') as date_printed_invoice,g.[NAME],a.order_number,a.quantity_on_order, " +
                    "quantity_same,f.status_description,a.date_completion,CASE WHEN COALESCE(e.payment_confirm, 0) = 0 THEN 'Not Comfirmed' WHEN COALESCE(e.payment_confirm,0) = 1 THEN 'Confirmed' END as [payment_confirmed], " +
                    "COALESCE(h.line_total, 0) as Door_cost from dbo.door a " +
                    "LEFT JOIN dbo.invoice_door b ON a.id = b.door_id " +
                    "LEFT JOIN dbo.invoice c ON b.invoice_id = c.id " +
                    "LEFT JOIN dbo.door_type d ON a.door_type_id = d.id " +
                    "LEFT JOIN dbo.door_payment e ON a.id = e.door_id " +
                    "LEFT JOIN dbo.[status] f on status_id = f.id " +
                    "LEFT JOIN dbo.SALES_LEDGER g ON a.customer_acc_ref = g.ACCOUNT_REF " +
                     "LEFT JOIN dbo.view_door_value h ON a.id = h.id " +
                    "where date_printed_invoice is null AND" +
                    " (slimline_y_n = 0 OR slimline_y_n is null) and(status_id = 1 or status_id = 2 or status_id = 3 or status_id = 5) AND " +
                    "a.date_completion >= '" + dateStart.ToString("yyyy - MM - dd") + "' AND a.date_completion < DATEADD(month,1,'" + dateStart.ToString("yyyy - MM - dd") + "')" +
                    " ORDER BY a.date_completion ASC";

                //open the form
                frmInvoice frm = new frmInvoice(sql);
                frm.Show();
            }
        }

        private void btnThisMonthInvoiceSL_Click(object sender, EventArgs e)
        {
            string temp = lblInvoicethisMonthSL.Text;
            double value = 0;
            if (temp.Contains("£"))
            {
                temp = temp.Replace("£", "");
                value = Convert.ToDouble(temp);
            }
            if (value > 0)
            {
                //open new form to display what doors are needed
                //also build string here and pass that over to allow for one form with minimal changes to the code
                //get the start and end date for the search
                DateTime dateStart = new DateTime();
                string test;
                test = Convert.ToDateTime(cmbMonth.Text + " 01, " + cmbYear.Text).ToString("yyyy-MM-dd");
                //test = test.ToString("yyyy-MM-dd");
                dateStart = Convert.ToDateTime(test);


                string sql = "select a.id,COALESCE(b.id,0) as delivery_id,COALESCE(CAST(date_printed_delivery as nvarchar(max)),'N/A') as date_printed_delivery," +
                    "COALESCE(CAST(date_printed_invoice as nvarchar(max)), 'N/A') as date_printed_invoice,g.[NAME],a.order_number,a.quantity_on_order, " +
                    "quantity_same,f.status_description,a.date_completion,CASE WHEN COALESCE(e.payment_confirm, 0) = 0 THEN 'Not Comfirmed' WHEN COALESCE(e.payment_confirm,0) = 1 THEN 'Confirmed' END as [payment_confirmed], " +
                    "COALESCE(h.line_total, 0) as Door_cost from dbo.door a " +
                    "LEFT JOIN dbo.invoice_door b ON a.id = b.door_id " +
                    "LEFT JOIN dbo.invoice c ON b.invoice_id = c.id " +
                    "LEFT JOIN dbo.door_type d ON a.door_type_id = d.id " +
                    "LEFT JOIN dbo.door_payment e ON a.id = e.door_id " +
                    "LEFT JOIN dbo.[status] f on status_id = f.id " +
                    "LEFT JOIN dbo.SALES_LEDGER g ON a.customer_acc_ref = g.ACCOUNT_REF " +
                     "LEFT JOIN dbo.view_door_value h ON a.id = h.id " +
                    "where date_printed_invoice is null AND" +
                    " (slimline_y_n = -1) and(status_id = 1 or status_id = 2 or status_id = 3 or status_id = 5) AND " +
                    "a.date_completion >= '" + dateStart.ToString("yyyy - MM - dd") + "' AND a.date_completion < DATEADD(month,1,'" + dateStart.ToString("yyyy - MM - dd") + "')" +
                    " ORDER BY a.date_completion ASC";


                //open the form
                frmInvoice frm = new frmInvoice(sql);
                frm.Show();
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnSearch.PerformClick();
        }
    }
}
