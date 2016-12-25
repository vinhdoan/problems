using System;
using System.Collections;

using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


using System.Data.OleDb;
using System.Configuration;


namespace Demo_ConnectDataBase
{
    public partial class _Default : System.Web.UI.Page
    {
        String strConnect = ConfigurationManager.ConnectionStrings["QLHS"].ToString();
        
       

        protected void Page_Load(object sender, EventArgs e)
        {
            OleDbConnection cnn = new OleDbConnection();
            String strConnect = ConfigurationManager.ConnectionStrings["QLHS"].ToString();
            
            cnn.ConnectionString = strConnect;
            
            
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;

            cmd.CommandText = "SELECT COUNT(*) FROM HocSinh";
            cmd.CommandType = CommandType.Text;

            cnn.Open();
            lblTongSo.Text = ((int)cmd.ExecuteScalar()).ToString();
            cnn.Close();            
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            OleDbConnection cnn = new OleDbConnection();
            String strConnect = ConfigurationManager.ConnectionStrings["QLHS"].ToString();
            cnn.ConnectionString = strConnect;
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = cnn;

            cmd.CommandText = "INSERT INTO HocSinh(ID, HoTen, DTB, Lop) VALUES(?,?,?,?)";
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("id",OleDbType.Integer);
            cmd.Parameters.Add("HoTen", OleDbType.VarChar);
            cmd.Parameters.Add("DTB", OleDbType.Double);
            cmd.Parameters.Add("Lop", OleDbType.Integer);

            cmd.Parameters["id"].Value = int.Parse(txtMSHS.Text);
            cmd.Parameters["HoTen"].Value = txtHoTen.Text;
            cmd.Parameters["DTB"].Value = double.Parse(txtDiem.Text);
            cmd.Parameters["Lop"].Value = 1;

            cnn.Open();
            lblTongSo.Text = cmd.ExecuteNonQuery().ToString();
            cnn.Close();
        }
    }
}