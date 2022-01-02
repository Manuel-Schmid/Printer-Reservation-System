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
		SqlConnection con = new SqlConnection();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Session["isAdmin"].ToString() == "False")
			{
				gvPrinters.Columns[0].Visible = false;
				gvPrinters.Columns[6].Visible = false;
				gvPrinters.Columns[7].Visible = false;
			}

			conBuilder.DataSource = GlobalVariables.dataSource;
			conBuilder.InitialCatalog = GlobalVariables.dbName;
			conBuilder.IntegratedSecurity = true;
			con.ConnectionString = conBuilder.ConnectionString;

			if (!IsPostBack)
			{
				gvBindPrinters();
			}

		}

		private void gvBindPrinters()
		{
			DataTable tblPrinters = new DataTable();

			con.Open();

			SqlCommand cmd = new SqlCommand("spSelectPrinters", con);

			cmd.CommandType = CommandType.StoredProcedure;

			SqlDataAdapter dap = new SqlDataAdapter(cmd);

			dap.Fill(tblPrinters);
			con.Close();

			if (tblPrinters.Rows.Count > 0)
			{
				if (Session["isAdmin"].ToString() == "True") tblPrinters.Rows.Add();

				gvPrinters.DataSource = tblPrinters;
				gvPrinters.DataBind();
			}
			else
			{
				tblPrinters.Rows.Add(tblPrinters.NewRow());
				gvPrinters.DataSource = tblPrinters;
				gvPrinters.DataBind();
				int columncount = gvPrinters.Rows[0].Cells.Count;
				gvPrinters.Rows[0].Cells.Clear();
				gvPrinters.Rows[0].Cells.Add(new TableCell());
				gvPrinters.Rows[0].Cells[0].ColumnSpan = columncount;
				gvPrinters.Rows[0].Cells[0].Text = "No Printers Found";
			}
		}


		protected void gvPrinters_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			GridViewRow row = (GridViewRow)gvPrinters.Rows[e.RowIndex];
			if (row.Cells[0].Text != "&nbsp;") // only if no empty ID
			{
				con.Open();
				SqlCommand cmd = new SqlCommand("spDeletePrinter", con);

				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
				cmd.Parameters["@ID"].Value = row.Cells[0].Text;

				cmd.ExecuteNonQuery();
				con.Close();
				gvBindPrinters();
			}
		}

		protected void gvPrinters_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			GridViewRow row = (GridViewRow)gvPrinters.Rows[e.RowIndex];
			gvPrinters.EditIndex = -1;

			bool isNewInsert = false;

			isNewInsert = (row.Cells[0].Text == "&nbsp;" || row.Cells[0].Text == "" || row.Cells[0].Text == null); // if there is no ID in this row

			con.Open();
			SqlCommand cmd;

			if (isNewInsert)
			{
				cmd = new SqlCommand("spInsertPrinter", con);
			}
			else
			{
				cmd = new SqlCommand("spUpdatePrinter", con);
			}

			cmd.CommandType = CommandType.StoredProcedure;

			string[] druckbereich = RemoveWhitespace(((TextBox)row.Cells[5].Controls[0]).Text).Split('x');

			if (!isNewInsert) cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
			cmd.Parameters.Add(new SqlParameter("@Marke", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Modell", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Typ", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Beschreibung", SqlDbType.VarChar));
			cmd.Parameters.Add(new SqlParameter("@Druckbereich_Laenge", SqlDbType.Float));
			cmd.Parameters.Add(new SqlParameter("@Druckbereich_Breite", SqlDbType.Float));
			cmd.Parameters.Add(new SqlParameter("@Druckbereich_Hoehe", SqlDbType.Float));
			if (!isNewInsert) cmd.Parameters["@ID"].Value = row.Cells[0].Text;
			cmd.Parameters["@Marke"].Value = ((TextBox)row.Cells[1].Controls[0]).Text;
			cmd.Parameters["@Modell"].Value = ((TextBox)row.Cells[2].Controls[0]).Text;
			cmd.Parameters["@Typ"].Value = ((TextBox)row.Cells[3].Controls[0]).Text;
			cmd.Parameters["@Beschreibung"].Value = ((TextBox)row.Cells[4].Controls[0]).Text;
			cmd.Parameters["@Druckbereich_Laenge"].Value = druckbereich[0];
			cmd.Parameters["@Druckbereich_Breite"].Value = druckbereich[1];
			cmd.Parameters["@Druckbereich_Hoehe"].Value = druckbereich[2];

			cmd.ExecuteNonQuery();
			con.Close();

			gvBindPrinters();
		}


		protected void gvPrinters_RowEditing(object sender, GridViewEditEventArgs e)
		{
			gvPrinters.EditIndex = e.NewEditIndex;
			gvBindPrinters();
		}

		protected void gvPrinters_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			gvPrinters.PageIndex = e.NewPageIndex;
			gvBindPrinters();
		}

		protected void gvPrinters_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			gvPrinters.EditIndex = -1;
			gvBindPrinters();
		}
		private string RemoveWhitespace(string input)
		{
			return new string(input.ToCharArray()
				.Where(c => !Char.IsWhiteSpace(c))
				.ToArray());
		}
	}
}