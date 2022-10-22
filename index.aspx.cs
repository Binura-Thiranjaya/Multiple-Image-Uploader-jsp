using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace imageUploader
{
    public partial class index : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {        
            if (!Page.IsPostBack)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Welcome User!');", true);
                DisplayRecord();
            }
        }
        public DataTable DisplayRecord()
        {
            string query = "SELECT DISTINCT dbo.Image_Details.record_id,    dbo.Image_Header.Name  FROM dbo.Image_Details INNER JOIN dbo.Image_Header ON dbo.Image_Details.record_id = dbo.Image_Header.Id";
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

        protected void uploadImage(object sender, EventArgs e)
        {
            string Name = Request.Form["Name"];
            string query4 = "Insert into dbo.Image_Header(Name,Date) Values('"+Name+"','"+ DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")+"')";
            string query2 = "select * from dbo.Image_Header order by Id DESC";
            int Id = 0;
            int count = 0;
            Boolean CHECK = false;

            foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
            {
                string filename = Path.GetFileName(postedFile.FileName);
                string contentType = postedFile.ContentType;
                count++;
                using (Stream fs = postedFile.InputStream)
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        try
                        {
                             string base64String = "";
                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                            base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                            string[] subs = contentType.Split('/');

                            if (base64String.Length != 0 && filename.Length !=0  && subs[1].Equals("jpeg"))
                            {
                                if(count == 1)
                                {
                                    updateDataBase(query4);
                                    Id = getRecordId(query2);
                                }
                                
                                CHECK = true;
                                string query3 = "Insert into dbo.Image_Details(record_id,Image) Values (" + Id + ",'"+base64String+"');";
                                updateDataBase(query3);
                            }
                            else
                            {
                                CHECK = false;
                            }                            
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            CHECK = false;
                        }
                    }
                }
            }
            if (CHECK)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(),"alert","alert('Image saved sucessfully! Image Id :"+Id+"');window.location ='index.aspx';",true);
            }
            else
            {
                Label2.Text = "Please Check Again Name and Image Uploaded! Re Upload";
            }
        }

        public Boolean updateDataBase(string query)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.InsertCommand = new SqlCommand(query, con);
                adapter.InsertCommand.ExecuteNonQuery();

                cmd.Dispose();
                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Label1.Text = "Database Connection Issue";
                return false;
            }
        }
       
        public int getRecordId(string query)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Con"].ConnectionString);
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    int id = (int)sdr["Id"];
                    return id;

                }
                //Close the connection
                cmd.Dispose();
                con.Close();
                return -1;
            }
            catch (Exception ex)
            {
                Label1.Text = "Database Connection Issue";
                Console.WriteLine(ex);
                return -1;
            }
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
           String Id = (sender as LinkButton).CommandArgument;
           Response.Redirect("info.aspx?ID=" + Id);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Wait !Loading');window.location ='info.aspx?ID="+Id+"';", true);

        }
    }
}


