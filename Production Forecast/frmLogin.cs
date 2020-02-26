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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.productionForecast;
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            login login = new login();
            login.attemptLogin(txtUserName.Text, txtPassWord.Text);
            if (login.authorised == true)
            {
                //user + pass is correct so open new form
                frmMain frm = new frmMain();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Username or Password is wrong!");
                txtPassWord.Text = "";
                txtUserName.Text = "";
                txtUserName.Select();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void txtPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnLogIn.PerformClick();
        }
    }
}
