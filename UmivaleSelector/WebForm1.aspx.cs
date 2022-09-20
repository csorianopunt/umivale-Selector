using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace UmivaleSelector
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        public static string url;

        protected void Page_Load(object sender, EventArgs e)
        {

            //ej.: tel = 888802675022247 & callid = 85269

            string tel = Request["tel"];
            string callid = Request["callid"];

            try
            {
                string connectionString = Properties.Settings.Default.connectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand("select TOP 1 DNI,Centro,Call_ID from DNI where Caller = " + tel + " Order By FechaHora DESC", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                        
                                string DNI = reader["DNI"].ToString();
                                string Centro = reader["Centro"].ToString();
                                string CallID = reader["Call_ID"].ToString();

                                /* si es SAC */
                                if (Centro == Properties.Settings.Default.Centro)
                                {
                                    url = "http://srvsynergy/synergy/docs/Pntv2preinputs.aspx?tel=" + tel + "&callid=" + callid;
                                }
                                /* si es Callcenter */
                                else
                                {
                                    url = "http://srvccsynergy/synergy/docs/Pntv2preinputs.aspx?tel=" + tel + "&callid=" + callid;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                url = "http://srvccsynergy/synergy/docs/Pntv2preinputs.aspx?tel=" + tel + "&callid=" + callid;
            }
        }
    }
}