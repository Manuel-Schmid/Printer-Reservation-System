using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Printer_Reservation_System
{
    public partial class PrinterOverview : System.Web.UI.Page
    {
        readonly SqlConnectionStringBuilder conBuilder = new SqlConnectionStringBuilder();

        protected void Page_Load(object sender, EventArgs e)
        {
            conBuilder.DataSource = @"NOTEBOOKMANY\MSSQLSERVER2019";
            conBuilder.InitialCatalog = "3D_Drucker";
            conBuilder.IntegratedSecurity = true;

            selectPrinters();
        }

        private void selectPrinters()
        {
            DataTable tblPrinters = new DataTable();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = conBuilder.ConnectionString;
            con.Open();

            SqlCommand cmd = new SqlCommand("spSelectPrinters", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter dap = new SqlDataAdapter(cmd);

            dap.Fill(tblPrinters);

            gridviewPrinters.DataSource = tblPrinters;
            gridviewPrinters.DataBind();

            con.Close();
        }
    }
}