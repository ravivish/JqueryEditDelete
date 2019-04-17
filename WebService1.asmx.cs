using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Newtonsoft.Json;

namespace JqueryEditDelete
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string country()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_country", con);
            cmd.CommandType = CommandType.StoredProcedure;
            string p = "";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                p = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            return p;
        }

        [WebMethod]
        public void Save(string name, int age, string address, int country)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_emp_all", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action","insert");
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@c_name", country);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        [WebMethod]
        public string show()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_emp_all", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "show");
            string p = "";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                p = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            return p;
        }
        [WebMethod]
        public void delete(int empid)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_emp_all", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "delete");
            cmd.Parameters.AddWithValue("@empid", empid);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        [WebMethod]
        public string edit(int empid)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_emp_all", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "edit");
            cmd.Parameters.AddWithValue("@empid", empid);
            string p = "";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            if (ds.Tables[0].Rows.Count > 0)
            {
                p = JsonConvert.SerializeObject(ds.Tables[0]);
            }
            return p;
        }
        [WebMethod]
        public void update(string name, int age, string address, int country,int Empid)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_emp_all", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "update");
            cmd.Parameters.AddWithValue("@empid", Empid);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@c_name", country);
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }
}
