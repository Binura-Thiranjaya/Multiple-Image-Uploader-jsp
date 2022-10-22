using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace imageUploader
{
    public partial class info : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                DisplayRecord();

            }
        }
        public DataTable DisplayRecord()
        {
            String ID =  Request.QueryString["ID"];

            string query = "SELECT dbo.Image_Details.record_id, dbo.Image_Details.Image, dbo.Image_Header.Id, dbo.Image_Header.Date,dbo.Image_Header.Name FROM dbo.Image_Details INNER JOIN dbo.Image_Header ON dbo.Image_Details.record_id = dbo.Image_Header.Id  where dbo.Image_Details.record_id = '"+ID+"'";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
            con.Open();

            SqlDataAdapter Adp = new SqlDataAdapter(query, con);
            DataTable Dt = new DataTable();
            Adp.Fill(Dt);
            grid1.DataSource = Dt;
            grid1.DataBind();

            return Dt;
            con.Close();

        }
        protected void GoBack(object sender, EventArgs e)
        {
            Response.Redirect("index.aspx");
        }
    }
}