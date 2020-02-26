using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Production_Forecast
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.productionForecast;
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
    }
}
