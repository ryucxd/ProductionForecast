using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Production_Forecast
{
    class login
    {
        public bool authorised { get; set; }
        public bool correctLogin { get; set; }
        public bool passwordWrong { get; set; }

        public int loginID { get; set; }


        public void attemptLogin (string username,string password)
        {
            string sql = "SELECT COALESCE(id,0) FROM dbo.[user] WHERE username = '" + username + "' AND password = '" + password + "';";
            using (SqlConnection conn = new SqlConnection(CONNECT.ConnectionStringUser))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    loginID = Convert.ToInt32(cmd.ExecuteScalar());
                    conn.Close();
                    if ((Convert.ToString(loginID)) == "0" || string.IsNullOrEmpty(Convert.ToString(loginID)))
                    {
                        passwordWrong = true;
                        authorised = false;
                    }
                    else
                        authorised = true; //remove this later as we will need to check dbo.user to see if they are allowed to log in :)
                }
                if (passwordWrong == true)
                    return;
                else
                {
                    //check if they are allowed to log in :)
                }
            }
        }
    }
}
