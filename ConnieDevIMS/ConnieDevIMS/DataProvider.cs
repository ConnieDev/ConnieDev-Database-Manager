using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnieDevIMS
{
    public class DataProvider
    {
        static SqlConnection con;
        static SqlCommand cmd;
        static SqlDataReader rdr;

        public void ConnectTo(String cs)
        {
            con = new SqlConnection(cs);
            try
            {
                con.Open();
               
            }
            catch (Exception)
            {
                
            }
        }

        public DataTable ReadAllData()
        {
            DataTable dataSet = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * From Projects", con);
            dataAdapter.Fill(dataSet);
            return dataSet;
        }

        public DataTable GetData()
        {
            DataTable dataSet = new DataTable();
            SqlDataAdapter dataAdapter = new SqlDataAdapter("Select * From Projects", con);
            dataAdapter.Fill(dataSet);
            return dataSet;
        }

        public void CloseConnection()
        {
            con.Close();
        }

        public void DeleteData(String ID)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Delete From Projects Where ID = " + ID;
            cmd.ExecuteNonQuery();
        }

        public String[] ReadLocalData()
        {
            String[] data = new String[3];
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * From Connections";
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                data[0] = rdr["DataSource"].ToString();
                data[1] = rdr["InitialCatalog"].ToString();
                data[2] = rdr["UserID"].ToString();
            }

            rdr.Close();
            return data;
        }

        public String[] ReadData(String ID)
        {
            String[] data = new String[4];
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "Select * From Projects Where ID = " + ID;
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                data[0] = rdr["Title"].ToString();
                data[1] = rdr["ShortDes"].ToString();
                data[2] = rdr["LongDes"].ToString();
                data[3] = rdr["ImagePath"].ToString();
            }

            rdr.Close();
            return data;
        }

        public void InsertLocalData(String[] data)
        {
            cmd = new SqlCommand();

            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Connections (DataSource, InitialCatalog, UserID) VALUES (@DataSource,@InitialCatalog,@UserID);";
            cmd.Parameters.AddWithValue("@DataSource", data[0]);
            cmd.Parameters.AddWithValue("@InitialCatalog", data[1]);
            cmd.Parameters.AddWithValue("@UserID", data[2]);
            cmd.ExecuteNonQuery();
        }

        public void InsertData(String[] data)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Projects (Title, ShortDes, LongDes, ImagePath) VALUES (@Title,@ShortDes,@LongDes,@ImagePath);";
            cmd.Parameters.AddWithValue("@Title", data[0]);
            cmd.Parameters.AddWithValue("@ShortDes", data[1]);
            cmd.Parameters.AddWithValue("@LongDes", data[2]);
            cmd.Parameters.AddWithValue("@ImagePath", data[3]);
            cmd.ExecuteNonQuery();
        }

        public void UpdateData(String[] data, String ID)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Projects " +
                "SET Title = @Title, ShortDes = @ShortDes, LongDes = @LongDes, ImagePath = @ImagePath " +
                "WHERE ID = @ID";
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@Title", data[0]);
            cmd.Parameters.AddWithValue("@ShortDes", data[1]);
            cmd.Parameters.AddWithValue("@LongDes", data[2]);
            cmd.Parameters.AddWithValue("@ImagePath", data[3]);
            cmd.ExecuteNonQuery();
        }
    }
}
