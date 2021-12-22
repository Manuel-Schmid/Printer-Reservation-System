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
    public partial class StudentsOverview : System.Web.UI.Page
    {
        readonly SqlConnectionStringBuilder conBuilder = new SqlConnectionStringBuilder();
        SqlConnection con = new SqlConnection();

        protected void Page_Load(object sender, EventArgs e)
        {
            conBuilder.DataSource = @"NOTEBOOKMANY\MSSQLSERVER2019";
            conBuilder.InitialCatalog = "3D_Drucker";
            conBuilder.IntegratedSecurity = true;
            con.ConnectionString = conBuilder.ConnectionString;

            if (!IsPostBack)
            {
                gvBindStudents();
            }
        }

        protected void gvBindStudents()
        {
            DataTable tblPrinters = new DataTable();

            con.Open();

            SqlCommand cmd = new SqlCommand("spSelectStudents", con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter dap = new SqlDataAdapter(cmd);

            dap.Fill(tblPrinters);
            con.Close();
            if (tblPrinters.Rows.Count > 0)
            {
                gridviewStudents.DataSource = tblPrinters;
                gridviewStudents.DataBind();
            }
            else
            {
                tblPrinters.Rows.Add(tblPrinters.NewRow());
                gridviewStudents.DataSource = tblPrinters;
                gridviewStudents.DataBind();
                int columncount = gridviewStudents.Rows[0].Cells.Count;
                gridviewStudents.Rows[0].Cells.Clear();
                gridviewStudents.Rows[0].Cells.Add(new TableCell());
                gridviewStudents.Rows[0].Cells[0].ColumnSpan = columncount;
                gridviewStudents.Rows[0].Cells[0].Text = "No Records Found";
            }
        }

        protected void gridviewStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = (GridViewRow)gridviewStudents.Rows[e.RowIndex];
            Label lbldeleteid = (Label)row.FindControl("lblID");
            Console.WriteLine(lbldeleteid);
            //con.Open();
            //SqlCommand cmd = new SqlCommand("delete FROM detail where id='" + Convert.ToInt32(gridviewStudents.DataKeys[e.RowIndex].Value.ToString()) + "'", con);
            //cmd.ExecuteNonQuery();
            //con.Close();
            //gvBindStudents();
        }

        protected void gridviewStudents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridviewStudents.EditIndex = e.NewEditIndex;
            gvBindStudents();
        }
        protected void gridviewStudents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int userid = Convert.ToInt32(gridviewStudents.DataKeys[e.RowIndex].Value.ToString());
            GridViewRow row = (GridViewRow)gridviewStudents.Rows[e.RowIndex];
            Label lblID = (Label)row.FindControl("lblID");
            //TextBox txtname=(TextBox)gr.cell[].control[];  
            TextBox textName = (TextBox)row.Cells[0].Controls[0];
            TextBox textadd = (TextBox)row.Cells[1].Controls[0];
            TextBox textc = (TextBox)row.Cells[2].Controls[0];
            //TextBox textadd = (TextBox)row.FindControl("txtadd");  
            //TextBox textc = (TextBox)row.FindControl("txtc");  
            gridviewStudents.EditIndex = -1;
            con.Open();
            //SqlCommand cmd = new SqlCommand("SELECT * FROM detail", con);  
            SqlCommand cmd = new SqlCommand("update detail set name='" + textName.Text + "',address='" + textadd.Text + "',country='" + textc.Text + "'where id='" + userid + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            gvBindStudents();
            //gridviewStudents.DataBind();  
        }



        protected void gridviewStudents_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridviewStudents.PageIndex = e.NewPageIndex;
            gvBindStudents();
        }

        protected void gridviewStudents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridviewStudents.EditIndex = -1;
            gvBindStudents();
        }
    }
}